using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseKnight : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        startSpeed = 10f;
        startHealth = 100;
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
