using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretPurchaseManager : MonoBehaviour
{
    [Header("Purchase Panel")]
    [SerializeField] private Transform _purchasePanel;
    [SerializeField] private Vector3 _purchasePanelShowScale;
    [SerializeField] private float _panelTimeToShow;

    [Header("Slot Purchase Buttons")]
    [SerializeField] private List<TurretPruchaseButton> _leftPurchaseButtons;
    [SerializeField] private List<TurretPruchaseButton> _rightPurchaseButtons;
    [SerializeField] private Vector3 _purchaseButtonsShowScale;
    [SerializeField] private float _buttonsTimeToShow;

    [Header("Purchase Buttons")]
    [SerializeField] private Button _cancelBtn;

    private int _enabledSlotsIndex = 0;



    #region MONOBEHAVIOUR_METHODS

    private void Awake()
    {
        _purchasePanel.localScale = Vector3.zero;

        foreach (TurretPruchaseButton tpb in _leftPurchaseButtons)
        {
            tpb.transform.localScale = Vector3.zero;
        }

        foreach (TurretPruchaseButton tpb in _rightPurchaseButtons)
        {
            tpb.transform.localScale = Vector3.zero;
        }

        _cancelBtn.onClick.AddListener(OnCancelBtnClicked);
    }

    private void Start()
    {
        EnableNextPurchaseSlots(); // Test only
    }

    private void OnEnable()
    {
        foreach (TurretPruchaseButton tpb in _leftPurchaseButtons)
        {
            tpb.OnPurchaseButtonClicked += TurretPurchaseButton_OnLeftPurchaseButtonClicked;
        }

        foreach (TurretPruchaseButton tpb in _rightPurchaseButtons)
        {
            tpb.OnPurchaseButtonClicked += TurretPurchaseButton_OnRightPurchaseButtonClicked;
        }
    }

    private void OnDisable()
    {
        foreach (TurretPruchaseButton tpb in _leftPurchaseButtons)
        {
            tpb.OnPurchaseButtonClicked -= TurretPurchaseButton_OnLeftPurchaseButtonClicked;
        }

        foreach (TurretPruchaseButton tpb in _rightPurchaseButtons)
        {
            tpb.OnPurchaseButtonClicked -= TurretPurchaseButton_OnRightPurchaseButtonClicked;
        }
    }

    #endregion

    #region EVENTS_HANDLERS

    private void TurretPurchaseButton_OnLeftPurchaseButtonClicked(Vector3 btnPos, int btnIndex)
    {
        ShowPurchasePanel(btnPos);
    }

    private void TurretPurchaseButton_OnRightPurchaseButtonClicked(Vector3 btnPos, int btnIndex)
    {
        ShowPurchasePanel(btnPos);
    }

    private void OnCancelBtnClicked()
    {
        HidePurchasePanel();
    }

    #endregion

    #region COROUTINES
    private IEnumerator ScaleToShow(Transform transform, Vector3 finalScale, float timeToEnd)
    {
        Vector3 initialScale = transform.localScale;

        float elapsedTime = 0;
        while (elapsedTime < timeToEnd)
        {
            Vector3 newScale = Vector3.Slerp(initialScale, finalScale, elapsedTime / timeToEnd);
            transform.localScale = newScale;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = finalScale;

        if(transform.GetComponent<CanvasGroup>() != null)
            transform.GetComponent<CanvasGroup>().interactable = true;
    }

    #endregion

    #region PRUCHASE_BUTTONS

    public void EnableNextPurchaseSlots()
    {
        _enabledSlotsIndex++;
        foreach (TurretPruchaseButton tpb in _leftPurchaseButtons)
        {
            if(tpb.Index == _enabledSlotsIndex)
            {
                StartCoroutine(ScaleToShow(tpb.transform, _purchaseButtonsShowScale, _buttonsTimeToShow));
                break;
            }
        }

        foreach (TurretPruchaseButton tpb in _rightPurchaseButtons)
        {
            if (tpb.Index == _enabledSlotsIndex)
            {
                StartCoroutine(ScaleToShow(tpb.transform, _purchaseButtonsShowScale, _buttonsTimeToShow));
                break;
            }
        }
    }

    #endregion
    
    #region PURCHASE_PANEL

    private void ShowPurchasePanel(Vector3 at)
    {
        _purchasePanel.position = at;
        StartCoroutine(ScaleToShow(_purchasePanel, _purchasePanelShowScale, _panelTimeToShow));
    }

    private void HidePurchasePanel()
    {
        StartCoroutine(ScaleToShow(_purchasePanel, Vector3.zero, _panelTimeToShow));
    }

    #endregion

}
