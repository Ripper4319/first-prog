using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playbutton : MonoBehaviour
{
    public GameObject Main;
    private bool MainOpen = true;

    public void ToggleBool()
    {
        MainOpen = !MainOpen;
        Debug.Log("mybool in now:" + MainOpen);
    }
}
   

