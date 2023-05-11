using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LauncherPurchaseManager : MonoBehaviour
{
    [Header("Purchase Panel")]
    [SerializeField] private Transform _purchasePanel;
    [SerializeField] private Vector3 _purchasePanelShowScale;
    [SerializeField] private float _panelTimeToShow;

    [Header("Info Panel")]
    [SerializeField] private CanvasGroup _infoPanelCanvasGroup;
    [SerializeField] private TMP_Text _launcherCostText;
    [SerializeField] private TMP_Text _launcherDamageText;
    [SerializeField] private TMP_Text _launcherFireRateText;
    [SerializeField] private TMP_Text _launcherShootRangeText;

    [Header("Slot Purchase Buttons")]
    [SerializeField] private List<LauncherSlotPurchaseButton> _leftPurchaseButtons;
    [SerializeField] private List<LauncherSlotPurchaseButton> _rightPurchaseButtons;
    [SerializeField] private Vector3 _purchaseButtonsShowScale;
    [SerializeField] private float _buttonsTimeToShow;

    [Header("Purchase Buttons")]
    [SerializeField] private List<LauncherPurchaseButton> _launcherPurchaseButtons;
    [SerializeField] private Button _cancelBtn;

    private int _enabledSlotsIndex = 0;

    private Vector3 offset;

    #region MONOBEHAVIOUR_METHODS

    private void Awake()
    {
        offset = _purchasePanel.position - _infoPanelCanvasGroup.transform.position;

        HideInfoPanel();

        _purchasePanel.localScale = Vector3.zero;

        foreach (LauncherSlotPurchaseButton tpb in _leftPurchaseButtons)
        {
            tpb.transform.localScale = Vector3.zero;
        }

        foreach (LauncherSlotPurchaseButton tpb in _rightPurchaseButtons)
        {
            tpb.transform.localScale = Vector3.zero;
        }

        foreach (LauncherPurchaseButton lpb in _launcherPurchaseButtons)
        {
            lpb.OnHovered += LauncherPurchaseButton_OnHovered;
            lpb.OnExit += LauncherPurchaseButton_OnExit;
        }

        _cancelBtn.onClick.AddListener(OnCancelBtnClicked);
    }

    private void LauncherPurchaseButton_OnExit()
    {
        HideInfoPanel();
    }

    private void HideInfoPanel()
    {
        _infoPanelCanvasGroup.alpha = 0;
        _infoPanelCanvasGroup.interactable = false;
        _infoPanelCanvasGroup.blocksRaycasts = false;
    }

    private void LauncherPurchaseButton_OnHovered(float price, float damage, float fireRate, float shootRange)
    {
        ShowInfoPanel();

        _launcherCostText.text = price.ToString();
        _launcherDamageText.text = damage.ToString();
        _launcherFireRateText.text = fireRate.ToString();
        _launcherCostText.text = shootRange.ToString();
    }

    private void ShowInfoPanel()
    {
        _infoPanelCanvasGroup.alpha = 1;
        _infoPanelCanvasGroup.interactable = true;
        _infoPanelCanvasGroup.blocksRaycasts = true;
    }

    private void Start()
    {
        EnableNextPurchaseSlots(); // Test only
    }

    private void OnEnable()
    {
        foreach (LauncherSlotPurchaseButton tpb in _leftPurchaseButtons)
        {
            tpb.OnPurchaseButtonClicked += TurretPurchaseButton_OnLeftPurchaseButtonClicked;
        }

        foreach (LauncherSlotPurchaseButton tpb in _rightPurchaseButtons)
        {
            tpb.OnPurchaseButtonClicked += TurretPurchaseButton_OnRightPurchaseButtonClicked;
        }
    }

    private void OnDisable()
    {
        foreach (LauncherSlotPurchaseButton tpb in _leftPurchaseButtons)
        {
            tpb.OnPurchaseButtonClicked -= TurretPurchaseButton_OnLeftPurchaseButtonClicked;
        }

        foreach (LauncherSlotPurchaseButton tpb in _rightPurchaseButtons)
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
        foreach (LauncherSlotPurchaseButton tpb in _leftPurchaseButtons)
        {
            if(tpb.Index == _enabledSlotsIndex)
            {
                StartCoroutine(ScaleToShow(tpb.transform, _purchaseButtonsShowScale, _buttonsTimeToShow));
                break;
            }
        }

        foreach (LauncherSlotPurchaseButton tpb in _rightPurchaseButtons)
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
        foreach (LauncherPurchaseButton lpb in _launcherPurchaseButtons)
        {
            lpb.gameObject.SetActive(true);
        }

        _purchasePanel.position = at;
        _infoPanelCanvasGroup.transform.position = at - offset;
        StartCoroutine(ScaleToShow(_purchasePanel, _purchasePanelShowScale, _panelTimeToShow));
    }

    private void HidePurchasePanel()
    {
        foreach (LauncherPurchaseButton lpb in _launcherPurchaseButtons)
        {
            lpb.gameObject.SetActive(false);
        }

        StartCoroutine(ScaleToShow(_purchasePanel, Vector3.zero, _panelTimeToShow));
    }

    #endregion

}
