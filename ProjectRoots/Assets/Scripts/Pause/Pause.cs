using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PurchasePanel;

    private void Start()
    {
        PurchasePanel = GameObject.Find("PurchaseRootPanel");
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
        PurchasePanel.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        GameObject[] roots = GameObject.FindGameObjectsWithTag("Root");
        foreach (GameObject root in roots)
        {
            root.GetComponent<MeshCollider>().enabled = false;
        }
    }

    private void Resume()
    {
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
