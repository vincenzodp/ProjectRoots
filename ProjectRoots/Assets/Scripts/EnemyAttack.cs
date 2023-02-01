using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour
{
    Slider slider;
    Animator animator;
    private Transform target;
    private Enemy enemy;
    public float flashTime;
    Color originalColor;
    public MeshRenderer renderer;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = GameObject.FindGameObjectWithTag("Tree").transform;
        animator = GetComponent<Animator>();
        flashTime = 0.5f;
        renderer = target.gameObject.GetComponent<MeshRenderer>();
        originalColor = target.gameObject.GetComponent<MeshRenderer>().material.color;
        slider = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
    }

  
    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) <= 2.5f)
        {
            animator.SetTrigger("Attack");
        }
    }
    void AttackEvents()
    {
        slider.value -= 1;
        renderer.material.color = Color.red;
        Invoke("ResetColor", flashTime);
        //Debug.Log("Colpito");
    }
    void ResetColor()
    {
        renderer.material.color = originalColor;
    }
}
