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

    public Image healthbar;

    public GameObject quit;
    private bool QuitActive;

    public GameObject inv;
    private bool inventoryOpen;

    public GameObject set;
    private bool settingsOpen;

    public GameObject HUD;
    private bool HUDActive;


    public GameObject Resume;
    private bool ResumeOpen;


    public bool EndGame = false;

    void Start()
    {
        set.SetActive(false);
        inv.SetActive(false);
        HUD.SetActive(true);
        Resume.SetActive(false);


        if (SceneManager.GetActiveScene().buildIndex > 0)
        { 
            playerdata = GameObject.Find("player").GetComponent<NewBehaviourScript>();

            quit.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            healthbar.fillAmount = Mathf.Clamp((float)playerdata.Health / (float)playerdata.maxHealth, 0, 1);
        }

        if (Input.GetKeyDown(KeyCode.I) && !EndGame)
            Inventory();
        if (Input.GetKeyDown(KeyCode.Escape) && !EndGame)
            Settings();

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

    public void RestartLevel()
    {
        Time.timeScale = 1;
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResumeGame()
    {

    }

    public void ToggleResumeBool()
    {
        Resume.SetActive(false);
    }

    private void Settings()
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

    private void Inventory()
    {
        if (inventoryOpen)
        {
            inv.SetActive(false);
            inventoryOpen = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            HUD.SetActive(true);
        }
        else
        {
            HUD.SetActive(false);
            inv.SetActive(true);
            inventoryOpen = true;
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
        settingsOpen = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }
}
