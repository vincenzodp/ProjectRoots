using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Canvas canvas;
    public Slider slider;
    public float health;
    public Gradient gradient;
    public Image fill;

    public void Awake()
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
        canvas.enabled = false;
        slider.enabled = false;
    }

    public void SetHealthBarHealth(float currenHealth)
    {
        health = currenHealth;
        canvas.enabled = true;
        slider.enabled = true;
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
