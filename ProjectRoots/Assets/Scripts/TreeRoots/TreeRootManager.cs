using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent()]
public class TreeRootManager : MonoBehaviour
{
    public List<TreeRootNode> ChildrenNodes { get; private set; } = new List<TreeRootNode>();

    void Start()
    {
        DisableAllDescendantNodes(ChildrenNodes);
    }

    static void DisableAllDescendantNodes(List<TreeRootNode> nodes)
    {
        foreach(var node in nodes)
        {
            node.gameObject.SetActive(false);
            DisableAllDescendantNodes(node.ChildrenNodes);
        }
    }
}
