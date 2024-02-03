using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class comboManager : MonoBehaviour
{
    public static comboManager CM_Instance;

    [SerializeField] CinemachineVirtualCamera playerCamera;
    [SerializeField] comboTier startingTier;
    float currentComboPoints = 0;
    comboTier currentTier;
    public float currentMultiplier;
    [SerializeField] GameObject comboUI;
    [SerializeField] Slider comboMeter;
    [SerializeField] TMP_Text tierText;
    [SerializeField] TMP_Text multiplierText;
    [SerializeField] Image comboMeterBackground;
    [SerializeField] private float comboDecayPercentagePerSecond;
    [SerializeField] float transitionDuration;
    [SerializeField] FMODUnity.EventReference derankSound;

    private Coroutine effectCoroutine = null;

    [SerializeField] private Volume PV;
    private Bloom bloom;
    private ChromaticAberration ca;
    private Vignette vig;
    private bool startSound;

    private void Awake()
    {
        if (CM_Instance != null && CM_Instance != this)
        {
            Destroy(this);
        }
        else
        {
            CM_Instance = this;
        }

        PV.profile.TryGet<Bloom>(out bloom);
        PV.profile.TryGet<ChromaticAberration>(out ca);
        PV.profile.TryGet<Vignette>(out vig);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTier = startingTier;
        currentMultiplier = currentTier.pointMultiplier;
        startSound = false;
        UpdateUIElements();
    }

    // Update is called once per frame
    void Update()
    {
        //If you have no combo going on
        if(currentTier.prevTier == null && currentComboPoints <= 0)
        {
            currentComboPoints = 0;
            comboUI.SetActive(false);
            return;
        }


        comboUI.SetActive(true);
        //If we can make it to the next tier, upgrade

        if (currentComboPoints >= currentTier.pointsRequiredToNext && currentTier.nextTier != null)
        {
            currentComboPoints -= currentTier.pointsRequiredToNext;
            currentTier = currentTier.nextTier;
            currentMultiplier = currentTier.pointMultiplier;
            if (startSound)
            {
                FMODUnity.RuntimeManager.PlayOneShot(currentTier.comboSound);
            }
            UpdateUIElements();

        }

        //Otherwise, if we have to downgrade, downgrade.
        else if(currentComboPoints <= 0 && currentTier.prevTier != null)
        {
            currentTier = currentTier.prevTier;
            currentMultiplier = currentTier.pointMultiplier;
            currentComboPoints += currentTier.pointsRequiredToNext;
            FMODUnity.RuntimeManager.PlayOneShot(derankSound);
            UpdateUIElements();
        }



        //Decay the combo meter and update it
        currentComboPoints -= comboDecayPercentagePerSecond * currentTier.pointsRequiredToNext * Time.deltaTime;
        comboMeter.value = Mathf.Clamp(currentComboPoints / currentTier.pointsRequiredToNext,0,1);
    }

    public void GainComboPoints(float points)
    {
        currentComboPoints += points;
    }

    private void UpdateUIElements()
    {
        multiplierText.text = "Multiplier - " + currentTier.pointMultiplier.ToString() + "x";
        tierText.text = currentTier.letterGradeText;
        comboMeterBackground.color = currentTier.comboMeterColor;
        multiplierText.color = currentTier.comboMeterColor;
        tierText.color = currentTier.comboMeterColor;
        startSound = true; 
        HandleEffects();
        
    }

    private void HandleEffects()
    {
        if(effectCoroutine != null)
        {
            StopCoroutine(effectCoroutine);
        }
        effectCoroutine = StartCoroutine(UpdateCameraZoomAndBloods());
    }

    private IEnumerator UpdateCameraZoomAndBloods()
    {
        float timeElapsed = 0;
        float caOrigin = (float)ca.intensity;
        float bloomOrigin = (float)bloom.intensity;
        float vigOrigin = (float)vig.intensity;
        float cameraSizeOrigin = playerCamera.m_Lens.OrthographicSize;
        while(timeElapsed <= transitionDuration)
        {
            timeElapsed += Time.deltaTime;
            bloom.intensity.Override(Mathf.Lerp(bloomOrigin, currentTier.bloomIntensity, timeElapsed/transitionDuration));
            ca.intensity.Override(Mathf.Lerp(caOrigin, currentTier.chromaticAbberationIntensity, timeElapsed / transitionDuration));
            vig.intensity.Override(Mathf.Lerp(vigOrigin, currentTier.vignetteIntensity, timeElapsed / transitionDuration));
            playerCamera.m_Lens.OrthographicSize = Mathf.Lerp(cameraSizeOrigin, currentTier.cameraZoomAmount, timeElapsed / transitionDuration);
            yield return null;
        }
        yield return null;
    }
}
