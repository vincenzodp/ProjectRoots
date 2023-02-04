using System.Collections;
using UnityEngine;

public class EnergyRefiller : MonoBehaviour
{
    [SerializeField] float MaxSize = 1500f;
    [SerializeField] float InitialValue = 0f;

    public delegate void OnValueBelowZeroOnceHandler();
    public event OnValueBelowZeroOnceHandler OnValueBelowZeroOnce;
    bool isValueBelowZeroNotified = false;

    public float FixedEarnedValuePerSecond { get; set; } = 1f;
    public float EarnedValueIncreasePercentage { get; set; } = 0f;
    public float EarnedValueTemporaryBonusPercentage { get; set; } = 0f;
    public float CalculatedEarnedValuePerSecond
    {
        get => FixedEarnedValuePerSecond * (1 + EarnedValueIncreasePercentage) * (1 + EarnedValueTemporaryBonusPercentage);
    }


    public void SetTemporaryIncreaseBonus(float bonusPercentage, float secondsLength)
    {
        EarnedValueTemporaryBonusPercentage = bonusPercentage;
        StartCoroutine(ResetBonusAfterSeconds(secondsLength));
    }
    IEnumerator ResetBonusAfterSeconds(float secondsLength)
    {
        yield return new WaitForSeconds(secondsLength);
        EarnedValueTemporaryBonusPercentage = 0f;
    }


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

    void Update()
    {
        var valueToIncrease = CalculatedEarnedValuePerSecond * Time.deltaTime;
        Value += valueToIncrease;
    }
}
