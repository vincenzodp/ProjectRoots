using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretPruchaseButton : MonoBehaviour
{
    public event Action<Vector3, int> OnPurchaseButtonClicked;

    public int Index => _index;

    [SerializeField] private int _index;


    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        OnPurchaseButtonClicked?.Invoke(transform.position, _index);
    }
}
