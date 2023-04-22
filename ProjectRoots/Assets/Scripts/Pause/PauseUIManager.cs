using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUIManager : MonoBehaviour
{
    [SerializeField] Button ResumeButton;

    void Awake()
    {
        ResumeButton.onClick.AddListener(OnResumeClick);
    }

    void OnResumeClick()
    {
        GameManager.Instance.ResumeGame();
    }

    void OnQuitClick()
    {
        GameManager.Instance.ResumeGame();
    }

}
