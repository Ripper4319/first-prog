using NUnit;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine.Events;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{

    public NewBehaviourScript playerdata;

    public Image healthbar;

    public GameObject quit;
    private bool QuitActive;


    void Start()
    {
        playerdata = GameObject.Find("Player").GetComponent<NewBehaviourScript>();

        quit.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        healthbar.fillAmount = Mathf.Clamp((float)playerdata.Health / (float)playerdata.maxHealth, 0, 1);
    }
    public void ToggleBool()
    {
        quit.SetActive(false);
    }

}
