using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Canvas canvas;
    public Slider slider;
    public float health;

    public void Awake()
    {
        canvas.enabled = false;
        slider.enabled = false;
    }

    public void Update()
    {
        health = GetComponent<Enemy>().getHealth();
    }

    public void SetHealthBarMaxHealth()
    {
        slider.maxValue = health;
        slider.value = health;
        //fill.color = gradient.Evaluate(1f);
    }

    public void SetHealthBarHealth()
    {
        canvas.enabled = true;
        slider.enabled = true;
        slider.value = health;
        //fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
