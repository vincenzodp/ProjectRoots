using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    [SerializeField] IntroUIManager intro;

    public void OnAnimationDone()
    {
        intro.GrowTree();
        Destroy(this.gameObject);
    }
}
