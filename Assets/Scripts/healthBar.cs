using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void setSlider(float value)
    {
        slider.value = value;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    // Used to add to the value instead of setting
    public void increaseValue(float valueToAdd)
    {
        slider.value += valueToAdd;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void sliderMax(float value)
    {
        slider.maxValue = value;
        slider.value = value;

        fill.color = gradient.Evaluate(1f);
    }

    public float getValue()
    {
        return slider.value;
    }
}
