using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComboTier", menuName = "ScriptableObjects/ComboTierSO")]
public class comboTier : ScriptableObject
{
    public Sprite letterGradeSprite;
    public string letterGradeText;
    public Color comboMeterColor;
    public float pointsRequiredToNext;
    public float pointMultiplier;
    public comboTier nextTier;
    public comboTier prevTier;
    public float cameraZoomAmount;
    public float bloomIntensity;
    public float chromaticAbberationIntensity;
    public float vignetteIntensity;
    public FMODUnity.EventReference comboSound;
    public FMODUnity.EventReference dastardlyComboSound;
}
