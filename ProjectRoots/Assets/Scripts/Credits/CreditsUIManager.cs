using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsUIManager : MonoBehaviour
{
    [SerializeField] Button QuitButton;

    void Awake()
    {
        QuitButton.onClick.AddListener(OnResumeClick);
    }

    void OnResumeClick()
    {
        GameManager.Instance.ResumeGame();
    }

}
