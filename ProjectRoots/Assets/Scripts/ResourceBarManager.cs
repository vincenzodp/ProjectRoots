using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBarManager : MonoBehaviour
{
    [SerializeField] Slider Slider;
    [SerializeField] TextMeshProUGUI ValueText;
    [SerializeField] TextMeshProUGUI EarningText;

    private void Start()
    {
        Slider.minValue = 0f;
        Slider.maxValue = EnergyRefiller.Instance.MaxValue;
    }

    void Update()
    {
        var currentValue = EnergyRefiller.Instance.Value;
        var maxValue = EnergyRefiller.Instance.MaxValue;
        Slider.value = currentValue;
        ValueText.text = $"{Mathf.Floor(currentValue).ToString("N0")} / {maxValue.ToString("N0")}";
        EarningText.text = $"+{EnergyRefiller.Instance.CalculatedEarnedValuePerSecond.ToString("N1")}/s";

        if (Slider.maxValue != maxValue)
            Slider.maxValue = maxValue;
    }
}
