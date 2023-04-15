using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherPurchaseButton : MonoBehaviour
{
    public event Action<float, float, float, float> OnHovered;

    public event Action OnExit;


    [SerializeField] private LauncherData _launcherData;

    private void OnMouseOver()
    {
        OnHovered?.Invoke(_launcherData.Price, _launcherData.Damage, _launcherData.FireRate, _launcherData.ShootRange);
    }

    private void OnMouseExit()
    {
        OnExit?.Invoke();
    }
}
