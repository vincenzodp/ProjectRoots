using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceBar : MonoBehaviour
{
    [SerializeField] Slider slider;  // slider.value holds the amount of resource currently collected and owned by the player.

    [SerializeField] float resourceInterval = 1f; // how much time passes between generating new resources.
    [SerializeField] int resourceIncrement = 1; // amount of resources generated every time the timer reaches 0.
    [SerializeField] TextMeshProUGUI resourceText;
    [SerializeField] ParticleSystem fireworks;

    int maximumResourceAllowed;

    float resourceTimer;

    

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 0;
        resourceTimer = resourceInterval;
        maximumResourceAllowed = (int) slider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        if(resourceTimer <= 0)
        {
            IncreaseResource(resourceIncrement);
            resourceTimer = resourceInterval;
        }
        else
        {
            resourceTimer -= Time.deltaTime; 
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            IncreaseResource(10);
        }

        if(Input.GetKeyDown(KeyCode.F) && slider.value >= 10)
        {
            DecreaseResource(10);
            
            fireworks.Play();
        }

        
    }


    public void IncreaseResource(int amount)
    {
        if(amount > 0)
        {
            slider.value += amount;

            if(slider.value > maximumResourceAllowed)
            {
                slider.value = maximumResourceAllowed;
            }
            resourceText.text = "$" + slider.value.ToString();

        }
    }

    public void DecreaseResource(int amount)
    {
        if(amount > 0)
        {
            slider.value -= amount;
            
            if(slider.value < 0)
            {
                slider.value = 0;
            }

            resourceText.text = "$" + slider.value.ToString();
        }
    }

    public int GetResource()
    {
        return (int) slider.value;
    }
}
