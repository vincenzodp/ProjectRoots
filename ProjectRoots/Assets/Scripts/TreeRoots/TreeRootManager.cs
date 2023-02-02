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
    public LayerMask cameraClickLayerDetection;

    void Start()
    {
        ChildrenNodes.ForEach(node => node.SetStatus(TreeRootNode.Status.ToBuy));
    }
}
