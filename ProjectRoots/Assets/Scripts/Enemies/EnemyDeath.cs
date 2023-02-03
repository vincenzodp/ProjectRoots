using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    Animator animator;
    private Transform target;

    void Start()
    {   
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Projectile")?.transform;
    }

    public void TriggerDeath()
    {
        animator.SetTrigger("Death");
        //gameObject.tag = "Untagged";
    }
}
