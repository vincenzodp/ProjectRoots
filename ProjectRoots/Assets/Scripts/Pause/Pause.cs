using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject PauseUI;

    public static bool GameIsPaused = false;
    public GameObject PurchasePanel;
    public GameObject PausePanel;

    private void Start()
    {
        PurchasePanel = GameObject.Find("PurchaseRootPanel");
        PausePanel = Instantiate(PauseUI, Vector3.zero, PauseUI.transform.rotation);
        PausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }

            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        PausePanel.SetActive(true);
        PurchasePanel.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        GameObject[] roots = GameObject.FindGameObjectsWithTag("Root");
        foreach (GameObject root in roots)
        {
            root.GetComponent<MeshCollider>().enabled = false;
        }
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        PurchasePanel.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        GameObject[] roots = GameObject.FindGameObjectsWithTag("Root");
        foreach (GameObject root in roots)
        {
            root.GetComponent<MeshCollider>().enabled = true;
        }
    }
}
