using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "launchers/Data")]
public class LauncherData : ScriptableObject
{
    public float Price = 50;

    public float FireRate = 0.7f;

    public float Damage = 200;
    
    public float ShootRange = 5;
}
