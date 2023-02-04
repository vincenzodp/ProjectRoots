using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVictory : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

    }

    public void TriggerVictory()
    {
        animator.SetTrigger("Victory");
    }
}
