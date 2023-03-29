using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] GameObject creditsPanel;

    public void StartButtonPressed()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void CreditsButtonPressed()
    {
        creditsPanel.SetActive(true);
    }

    public void CloseCreditsPanel()
    {
        creditsPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
    
}
