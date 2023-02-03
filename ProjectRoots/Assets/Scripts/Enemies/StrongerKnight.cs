using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongerKnight : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        startSpeed = 8f;
        startHealth = 150;
        speed = startSpeed;
        health = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Attack()
    {
        base.Attack();
    }
}
