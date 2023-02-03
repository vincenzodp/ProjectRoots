using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent()]
public class PurchaseRootPanelManager : MonoBehaviour
{
    public Transform canvas;
    public TextMeshProUGUI costTextMesh;
    public TextMeshProUGUI earningTextMesh;
    public Button confirmButton;
    public Button cancelButton;

    public delegate void OnPurchaseConfirmedHandler(TreeRootNode requestingNode, Vector3 confirmButtonPosition);
    public event OnPurchaseConfirmedHandler OnPurchaseConfirmed;

    public delegate void OnPurchaseCancelledHandler();
    public event OnPurchaseCancelledHandler OnPurchaseCancelled;

    TreeRootNode requestingNode;

    void Start()
    {
        Hide();
        confirmButton.onClick.AddListener(ConfirmPurchase);
        cancelButton.onClick.AddListener(CancelPurchase);
    }

    void ConfirmPurchase()
    {
        OnPurchaseConfirmed?.Invoke(requestingNode, confirmButton.transform.position);
        Hide();
    }
    void CancelPurchase()
    {
        OnPurchaseCancelled?.Invoke();
        Hide();
    }

    void OnDestroy()
    {
        confirmButton.onClick.RemoveListener(ConfirmPurchase);
        cancelButton.onClick.RemoveListener(CancelPurchase);
    }

    public void Hide()
    {
        requestingNode = null;
        canvas.gameObject.SetActive(false);
    }
    public void Show(TreeRootNode requestingNode, Vector3 position, int buyCost, float earningValue, TreeRootNode.EarningType earningType)
    {
        if (this.requestingNode == requestingNode)
            // Avoid to reopen the panel on same node
            return;

        this.requestingNode = requestingNode;
        transform.position = position;

        // Note: use underscore to display a minus
        costTextMesh.text = $"_{buyCost.ToString("N0")}";
        earningTextMesh.text = GetEarningBonusText(earningValue, earningType);
        canvas.gameObject.SetActive(true);
    }

    string GetEarningBonusText(float earningValue, TreeRootNode.EarningType earningType)
    {
        var formattedValue = earningValue.ToString("N0");
        if (earningType == TreeRootNode.EarningType.Percentage)
            return $"+{formattedValue}%";
        else
            return $"+{formattedValue}!";
    }
}
