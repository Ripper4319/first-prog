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


    void Start()
    {
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

}
