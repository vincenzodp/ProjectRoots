using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent()]
public class PurchaseRootPanelManager : MonoBehaviour
{
    [SerializeField] Transform canvas;

    [SerializeField] TextMeshProUGUI TopLeftLabel;
    [SerializeField] TextMeshProUGUI TopRightLabel;
    [SerializeField] TextMeshProUGUI MidLeftLabel;
    [SerializeField] TextMeshProUGUI MidRightLabel;
    [SerializeField] TextMeshProUGUI BottomLeftLabel;
    [SerializeField] TextMeshProUGUI BottomRightLabel;
    [SerializeField] Button ConfirmButton;
    [SerializeField] Button CancelButton;

    [SerializeField] bool usePerSecondsSuffix;

    public delegate void OnPurchaseConfirmedHandler(TreeRootNode requestingNode, Vector3 confirmButtonPosition);
    public event OnPurchaseConfirmedHandler OnPurchaseConfirmed;

    public delegate void OnPurchaseCancelledHandler();
    public event OnPurchaseCancelledHandler OnPurchaseCancelled;

    TreeRootNode requestingNode;

    void Start()
    {
        Hide();
        ConfirmButton.onClick.AddListener(ConfirmPurchase);
        CancelButton.onClick.AddListener(CancelPurchase);
    }

    void ConfirmPurchase()
    {
        OnPurchaseConfirmed?.Invoke(requestingNode, ConfirmButton.transform.position);
    }
    void CancelPurchase()
    {
        OnPurchaseCancelled?.Invoke();
        Hide();
    }

    void OnDestroy()
    {
        ConfirmButton.onClick.RemoveListener(ConfirmPurchase);
        CancelButton.onClick.RemoveListener(CancelPurchase);
    }

    public void Hide()
    {
        requestingNode = null;
        canvas.gameObject.SetActive(false);
    }
    public void Show(TreeRootNode requestingNode, Vector3 position, int buyCost, float earningValue, TreeRootNode.EarningType earningType, float increaseMaxValue)
    {
        if (this.requestingNode == requestingNode)
            // Avoid to reopen the panel on same node
            return;

        this.requestingNode = requestingNode;
        transform.position = position;

        // Note: use underscore to display a minus
        TopLeftLabel.text = "Cost";
        TopRightLabel.text = $"_{buyCost.ToString("N0")}";
        if (increaseMaxValue > 0)
        {
            MidLeftLabel.text = "Max";
            MidRightLabel.text = $"+{increaseMaxValue.ToString("N0")}";
            BottomLeftLabel.text = "Grow";
            BottomRightLabel.text = GetEarningBonusText(earningValue, earningType);
            BottomLeftLabel.gameObject.SetActive(true);
            BottomRightLabel.gameObject.SetActive(true);
        }
        else
        {
            MidLeftLabel.text = "Grow";
            MidRightLabel.text = GetEarningBonusText(earningValue, earningType);
            BottomLeftLabel.text = "";
            BottomRightLabel.text = "";
            BottomLeftLabel.gameObject.SetActive(false);
            BottomRightLabel.gameObject.SetActive(false);
        }
        canvas.gameObject.SetActive(true);
    }

    string GetEarningBonusText(float earningValue, TreeRootNode.EarningType earningType)
    {
        var formattedValue = earningValue.ToString("N0");
        if (earningType == TreeRootNode.EarningType.Percentage)
            return $"+{formattedValue}%";
        else
        {
            var suffix = usePerSecondsSuffix ? "/s" : "";
            return $"+{formattedValue}{suffix}";
        }
    }
}
