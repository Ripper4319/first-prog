using NUnit;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour
{

    public NewBehaviourScript playerdata;

    public GameObject player;

    public Image healthbar;

    public GameObject quit;
    private bool QuitActive;

    public GameObject set;
    private bool settingsOpen;

    public GameObject end;

    public GameObject HUD;
    private bool HUDActive;


    public GameObject Resume;
    private bool ResumeOpen;


    public bool EndGame = false;

    void Start()
    {
        set.SetActive(false);
        HUD.SetActive(true);
        end.SetActive(false);
     
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            healthbar.fillAmount = Mathf.Clamp((float)playerdata.Health / (float)playerdata.maxHealth, 0, 1);

            if (Input.GetKeyDown(KeyCode.Escape) && !EndGame)
                Settings();
        }

        

    }
  
    public void Quitgame()
    {
        Application.Quit();
    }

    public void LoadLevel(int sceneID)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneID);

    }

    public void MainLevel(int sceneID)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = 0;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void RestartLevel()
    {

        Time.timeScale = 1;
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    


    public void Settings()
    {
        if (settingsOpen)
        {
            set.SetActive(false);
            settingsOpen = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            HUD.SetActive(true);
        }
        else
        {
            HUD.SetActive(false);
            set.SetActive(true);
            settingsOpen = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }


    public void End()
    {
        EndGame = true;
        HUD.SetActive(false);
        set.SetActive(false);
        end.SetActive(true);
        settingsOpen = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;

        StartCoroutine(RestartGame());
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
    public IEnumerator RestartGame()
    {

        yield return new WaitForSeconds(2);

        Application.Quit();
    }
}
