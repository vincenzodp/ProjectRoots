using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent()]
public class TreeRootManager : MonoBehaviour
{
    public List<TreeRootNode> ChildrenNodes { get; private set; } = new List<TreeRootNode>();
    public Material NextToBuyMaterial;
    public Material NextToBuyHoverMaterial;
    public GameObject FloatingPurchaseFeedbackText;
    public GameObject NotEnoughEnergyFeedbackText;
    public LayerMask RootsLayer;
    public PurchaseRootPanelManager PurchasePanel;


    void Start()
    {
        ChildrenNodes.ForEach(node => node.SetStatus(TreeRootNode.Status.ToBuy));
        PurchasePanel.OnPurchaseConfirmed += PurchasePanel_OnConfirmClick;
        PurchasePanel.OnPurchaseCancelled += PurchasePanel_OnCancelClick;
    }

    private void PurchasePanel_OnConfirmClick(TreeRootNode requestingNode, Vector3 confirmButtonPosition)
    {
        if (!EnergyRefiller.Instance.CanConsumeValue(requestingNode.buyCost))
        {
            CreateFloatingNotEnoughEnergyFeedbackText(confirmButtonPosition);
            return;
        }

        PurchasePanel.Hide();
        requestingNode.SetStatus(TreeRootNode.Status.Bought);
        CreateFloatingPurchaseFeedbackText(confirmButtonPosition, requestingNode.buyCost);

        var energyRefiller = EnergyRefiller.Instance;
        // Remove Cost
        energyRefiller.Value -= requestingNode.buyCost;

        // Apply bonus
        if (requestingNode.earningType == TreeRootNode.EarningType.FixedValue)
            energyRefiller.CalculatedEarnedValuePerSecond += requestingNode.earningValue;
        else
        {
            var percent = requestingNode.earningValue / 100;
            energyRefiller.CalculatedEarnedValuePerSecond += energyRefiller.CalculatedEarnedValuePerSecond * percent;
        }

        energyRefiller.MaxValue += requestingNode.maxSizeIncrease;

        // Emit event on top
        GameManager.Instance.NewRootNodeBought(requestingNode);
    }

    private void PurchasePanel_OnCancelClick()
    {
        Debug.Log("PanelCancel");
    }


    public void DisplayPurchasePanel(TreeRootNode requestingNode, int buyCost, float earningValue, TreeRootNode.EarningType earningType, float maxSizeIncrease)
    {
        var clickPosition = GetClickPointOnRoots();
        PurchasePanel.Show(requestingNode, clickPosition, buyCost, earningValue, earningType, maxSizeIncrease);
    }

    Vector3 GetClickPointOnRoots()
    {
        var cameraMain = Camera.main;
        var cameraHitRay = cameraMain.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(cameraHitRay, out hitInfo, 1000f, RootsLayer))
            return hitInfo.point;
        else
            return cameraMain.transform.position;
    }

    void CreateFloatingPurchaseFeedbackText(Vector3 position, int cost)
    {
        var newFloatingText = Instantiate(FloatingPurchaseFeedbackText, position, FloatingPurchaseFeedbackText.transform.rotation);
        var floatingTextManager = newFloatingText.GetComponentInChildren<FloatingPurchaseFeedbackManager>();
        floatingTextManager.DisplayCost(cost);
    }

    void CreateFloatingNotEnoughEnergyFeedbackText(Vector3 position)
    {
        Instantiate(NotEnoughEnergyFeedbackText, position, NotEnoughEnergyFeedbackText.transform.rotation);
    }
}
