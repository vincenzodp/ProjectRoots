using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LauncherPurchaseButton : MonoBehaviour
{
    public event Action<float, float, float, float> OnHovered;
    public event Action<LauncherData> OnClick;
    public event Action OnExit;


    [SerializeField] private LauncherData _launcherData;

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(OnBtnClicked);
    }

    private void OnBtnClicked()
    {
        OnClick?.Invoke(_launcherData);
    }

    private void OnMouseOver()
    {
        OnHovered?.Invoke(_launcherData.Price, _launcherData.Damage, _launcherData.FireRate, _launcherData.ShootRange);
    }

    private void OnMouseExit()
    {
        OnExit?.Invoke();
    }
}
