using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent()]
public class TreeRootManager : MonoBehaviour
{
    public float Earning { get; set; }

    public List<TreeRootNode> ChildrenNodes { get; private set; } = new List<TreeRootNode>();
    public Material NextToBuyMaterial;
    public Material NextToBuyHoverMaterial;
    public GameObject FloatingText;
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
        Debug.Log("TODO: Add cost verification here", this);
        // TODO: add cost verification here
        requestingNode.SetStatus(TreeRootNode.Status.Bought);
        CreatePurchaseFeedbackText(confirmButtonPosition, requestingNode.buyCost);
    }

    private void PurchasePanel_OnCancelClick()
    {
        Debug.Log("PanelCancel");
    }


    public void DisplayPurchasePanel(TreeRootNode requestingNode, int buyCost, float earningValue, TreeRootNode.EarningType earningType)
    {
        var clickPosition = GetClickPointOnRoots();
        PurchasePanel.Show(requestingNode, clickPosition, buyCost, earningValue, earningType);
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

    void CreatePurchaseFeedbackText(Vector3 position, int cost)
    {
        var newFloatingText = Instantiate(FloatingText, position, FloatingText.transform.rotation);
        var floatingTextManager = newFloatingText.GetComponentInChildren<FloatingPurchaseFeedbackManager>();
        floatingTextManager.DisplayCost(cost);
    }
}
