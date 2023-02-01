using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[DisallowMultipleComponent()]
public class TreeRootNode : MonoBehaviour
{
    public float EarnedValue;
    public float BuyCost;

    public TreeRootNode ParentNode { get; private set; } = null;
    public List<TreeRootNode> ChildrenNodes { get; private set; } = new List<TreeRootNode>();

    private void Awake()
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
    }

    void Start()
    {
    }

    void Update()
    {

    }

    private void OnMouseDown()
    {

    }
}
