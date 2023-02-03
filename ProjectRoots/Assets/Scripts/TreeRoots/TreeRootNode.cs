using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent()]
[RequireComponent(typeof(MeshRenderer))]
public class TreeRootNode : MonoBehaviour
{
    public EarningType earningType;
    public float earningValue;
    public int buyCost;

    public enum EarningType
    {
        Percentage,
        FixedValue
    }

    public TreeRootNode ParentNode { get; private set; } = null;
    public List<TreeRootNode> ChildrenNodes { get; private set; } = new List<TreeRootNode>();

    TreeRootManager manager;
    Material[] baseMaterials;
    MeshRenderer meshRenderer;


    Status currentStatus;

    public enum Status
    {
        ToBuy,
        Bought,
        Invisible
    }

    private void Awake()
    {
        SetupParentChildrenRelationships();
        meshRenderer = GetComponent<MeshRenderer>();
        baseMaterials = meshRenderer.materials;

        SetStatus(Status.Invisible);
    }

    private void OnMouseDown()
    {
        if (currentStatus == Status.ToBuy)
            manager.DisplayPurchasePanel(this, buyCost, earningValue, earningType);
    }

    private void OnMouseEnter()
    {
        if (currentStatus == Status.ToBuy)
            meshRenderer.materials = baseMaterials.Select(m => manager.NextToBuyHoverMaterial).ToArray();
    }
    private void OnMouseExit()
    {
        if (currentStatus == Status.ToBuy)
            meshRenderer.materials = baseMaterials.Select(m => manager.NextToBuyMaterial).ToArray();
    }

    public void SetStatus(Status status)
    {
        currentStatus = status;
        switch (status)
        {
            case Status.ToBuy:
                meshRenderer.materials = baseMaterials.Select(m => manager.NextToBuyMaterial).ToArray();
                gameObject.SetActive(true);
                break;
            case Status.Bought:
                meshRenderer.materials = baseMaterials;
                gameObject.SetActive(true);
                ChildrenNodes.ForEach(node => node.SetStatus(Status.ToBuy));
                break;
            case Status.Invisible:
                gameObject.SetActive(false);
                break;
        }
    }

    void SetupParentChildrenRelationships()
    {
        var transformParent = transform.parent;
        ParentNode = transformParent.GetComponentInParent<TreeRootNode>(true);

        if (ParentNode == null)
        {
            // Register on Manager
            var manager = transformParent.GetComponentInParent<TreeRootManager>();
            if (manager == null)
                throw new MissingComponentException("TreeRootManager is missing");
            manager.ChildrenNodes.Add(this);
        }
        else
        {
            // Register on upper node
            ParentNode.ChildrenNodes.Add(this);
        }

        manager = GetComponentInParent<TreeRootManager>();
    }
}
