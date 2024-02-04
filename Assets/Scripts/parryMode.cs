using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parryMode : MonoBehaviour
{
    public bool isParrying;

    [SerializeField] GameObject parryVisual;

    [SerializeField] healthBar parryBar;
    [SerializeField] float maxTime;

    [SerializeField] Animator ani;

    private float currentTime;

    private List<projectileMove> parryObjects;

    // Audio Stuff
    [SerializeField] FMODUnity.EventReference ShieldWorkieSFX;
    [SerializeField] FMODUnity.EventReference ShieldNoWorkieSFX;

    // Start is called before the first frame update
    void Start()
    {
        parryVisual.SetActive(false);
        Time.timeScale = 10.0f;
        isParrying = false;
        parryObjects = new List<projectileMove>();
        parryBar.sliderMax(maxTime);

        currentTime = maxTime;
    }

    void parryOn()
    {
        parryVisual.SetActive(true);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("shieldEmpty", 0);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("ShieldOff", 0);
        FMODUnity.RuntimeManager.PlayOneShot(ShieldWorkieSFX);
        isParrying = true;
        ani.SetBool("isParrying", true);
    }

    void parryOff()
    {
        parryVisual.SetActive(false);
        Time.timeScale = 10f;
        isParrying = false;
        ani.SetBool("isParrying", false);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("ShieldOff", 1);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SlowDown", 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Handle Parry
        if (Input.GetKeyDown(KeyCode.Space) && !parryBar.isRecovering)
        {
            parryOn();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && parryBar.isRecovering)
        {
            FMODUnity.RuntimeManager.PlayOneShot(ShieldNoWorkieSFX);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            parryOff();
        }

        // Do Parry Mechanics
        if (parryObjects.Count>0)
        {
            if (isParrying)
            {
                Time.timeScale = 1f;
                parryObjects[0].ActivateParry();
                FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SlowDown", 1);
            }
            else
            {
                Time.timeScale = 10f;
                parryObjects[0].deactivateParry();
            }
            
        }

        // Drain healthBar
        if(isParrying)
        {
            if (parryObjects.Count > 0)
            {
                currentTime -= Time.deltaTime;
            }
            else
            {
                currentTime -= Time.deltaTime/3f;
            }

            //if it hits bottom, go to recovery
            if (currentTime <=0)
            {
                FMODUnity.RuntimeManager.StudioSystem.setParameterByName("shieldEmpty", 1);
                parryBar.isRecovering = true;
                parryOff();
            }

            currentTime = Mathf.Max(0f, currentTime);

        }
        else
        {
            currentTime += Time.deltaTime/20;

            //Stop Recovering
            if (parryBar.isRecovering && currentTime >= maxTime/2)
            {
                parryBar.isRecovering = false;
            }
            currentTime = Mathf.Min(maxTime, currentTime);
            
        }
        parryBar.setSlider(currentTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        projectileMove pm = other.GetComponent<projectileMove>();
        if (pm !=null && !parryObjects.Contains(pm))
        {
            parryObjects.Add(pm);
            // Do something when an object enters the trigger
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        projectileMove pm = collision.GetComponent<projectileMove>();
        if (pm != null && parryObjects.Contains(pm) && pm.hasBeenParryed)
        {
            parryObjects.Remove(pm);
            // Remove things that have been parried
        }
    }

    public void removeObject(projectileMove pm)
    {
        if (pm != null && parryObjects.Contains(pm))
        {
            parryObjects.Remove(pm);
            // Remove things that have been parried
        }
        StartCoroutine(deleteAfterDelay(pm));
    }
    private IEnumerator deleteAfterDelay(projectileMove pm)
    {
        yield return new WaitForSeconds(.01f);
        if (pm !=null && pm.gameObject!=null)
        {
            Destroy(pm.gameObject);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        projectileMove pm = other.GetComponent<projectileMove>();
        if (pm != null && parryObjects.Contains(pm))
        {
            parryObjects.Remove(pm);
            // Do something when an object exits the trigger
        }
    }

}
