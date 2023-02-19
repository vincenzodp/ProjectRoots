using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyParentOnAnimationEvent : MonoBehaviour
{
    public void DestroyParent()
    {
        var parent = transform.parent.gameObject;
        Destroy(parent);

    }
}
