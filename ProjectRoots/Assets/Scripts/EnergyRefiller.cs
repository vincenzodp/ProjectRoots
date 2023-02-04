using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRefiller : MonoBehaviour
{
    [SerializeField] float MaxSize = 1500f;
    [SerializeField] float WaitSecondsBeforeRefill = 0.2f;
    [SerializeField] float RefillScale = 1f;
    [SerializeField] float InitialValue = 0f;

    public delegate void OnValueBelowZeroOnceHandler();
    public event OnValueBelowZeroOnceHandler OnValueBelowZeroOnce;
    bool isValueBelowZeroNotified = false;

    public float EarnedValuePerTick { get; private set; } = 1f;

    float currentValue;
    public float Value
    {
        get => currentValue;
        private set
        {
            currentValue = Mathf.Min(value, MaxSize);
            if (currentValue < 0 && !isValueBelowZeroNotified)
            {
                isValueBelowZeroNotified = true;
                OnValueBelowZeroOnce?.Invoke();
            }
        }
    }

    public bool CanConsumeValue(float amount) => Value >= amount;

    // Update is called once per frame
    void Start()
    {
        Value = InitialValue;
        StartCoroutine(RefillLoop());
    }

    private IEnumerator RefillLoop()
    {
        WaitForSeconds waitTime = new WaitForSeconds(WaitSecondsBeforeRefill);
        while (true)
        {
            Value += EarnedValuePerTick * RefillScale;
            yield return waitTime;
        }
    }
}
