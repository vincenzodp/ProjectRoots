using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[Singleton]
[DisallowMultipleComponent]
public class EnergyRefiller : MonoBehaviour
{
    public static EnergyRefiller Instance { get; private set; }

    [SerializeField] float MaxSize = 500f;
    [SerializeField] float BaseEarningPerSecond = 20f;
    [SerializeField] float InitialValue = 0f;

    public float MaxValue { get => MaxSize; set { MaxSize = value; } }

    public delegate void OnValueBelowZeroOnceHandler();
    public event OnValueBelowZeroOnceHandler OnValueBelowZeroOnce;
    bool isValueBelowZeroNotified = false;
    bool StopIncreasing = false;

    public float FixedEarnedValuePerSecond { get; set; } = 0f;
    public float EarnedValueIncreasePercentage { get; set; } = 0f;
    public float EarnedValueTemporaryBonusPercentage { get; set; } = 0f;
    public float CalculatedEarnedValuePerSecond
    {
        get => BaseEarningPerSecond + FixedEarnedValuePerSecond * (1 + EarnedValueIncreasePercentage) * (1 + EarnedValueTemporaryBonusPercentage);
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
        set
        {
            currentValue = Mathf.Min(value, MaxSize);
            if (currentValue < 0 && !isValueBelowZeroNotified)
            {
                isValueBelowZeroNotified = true;
                StopIncreasing = true;
                OnValueBelowZeroOnce?.Invoke();
            }
        }
    }

    public bool CanConsumeValue(float amount) => Value >= amount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Update()
    {
        var valueToIncrease = CalculatedEarnedValuePerSecond * Time.deltaTime;
        if (StopIncreasing == false) {
            Value += valueToIncrease;
        }
        else
        {
            Value = 0;
        }
    }
}
