using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbarian : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        startSpeed = 6f;
        startHealth = 300;
        speed = startSpeed;
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