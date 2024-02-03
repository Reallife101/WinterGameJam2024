using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Gradient gradient;
    public Image fillGood;
    public Image fillBad;

    public bool isRecovering;

    private float maxVal = 1f;

    public void setSlider(float value)
    {
        fillGood.fillAmount = value / maxVal;
        fillBad.fillAmount = value / maxVal;

        if (isRecovering)
        {
            fillBad.gameObject.SetActive(true);
            fillGood.gameObject.SetActive(false);
        }
        else
        {
            fillBad.gameObject.SetActive(false);
            fillGood.gameObject.SetActive(true);
        }
        
    }


    // Used to add to the value instead of setting
    public void increaseValue(float valueToAdd)
    {
        fillGood.fillAmount += valueToAdd;
    }

    public void sliderMax(float value)
    {
        maxVal = value;
        fillGood.fillAmount = 1f;
        fillBad.fillAmount = 1f;
        fillBad.gameObject.SetActive(false);
        fillGood.gameObject.SetActive(true);
    }

    public float getValue()
    {
        return fillGood.fillAmount;
    }
}
