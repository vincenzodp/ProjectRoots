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
        Slider.maxValue = EnergyRefiller.Instance.MaxSize;
    }

    void Update()
    {
        var currentValue = EnergyRefiller.Instance.Value;
        Slider.value = currentValue;
        ValueText.text = Mathf.Floor(currentValue).ToString("N0");
        EarningText.text = $"+{EnergyRefiller.Instance.CalculatedEarnedValuePerSecond.ToString("N1")}/s";
    }
}
