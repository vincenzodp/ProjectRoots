using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField] float turretLifetime = 10;
    [SerializeField] int defenseTurretCost = 30;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public float GetLifetime()
    {
        return turretLifetime;
    }

    public int GetCost()
    {
        return defenseTurretCost;
    }
}
