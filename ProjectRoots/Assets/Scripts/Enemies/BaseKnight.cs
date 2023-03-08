using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseKnight : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        //startSpeed = 10f;
        //startHealth = 100;
        //speed = startSpeed;
        //health = startHealth;
    }

    protected override void Attack()
    {
        base.Attack();
    }
}
