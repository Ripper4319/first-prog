using TMPro;
using UnityEngine;

public class DisplayAndControl : MonoBehaviour
{
    public TextMeshProUGUI tmpText1;
    public TextMeshProUGUI tmpText2;
    public TextMeshProUGUI tmpText3;
    public TextMeshProUGUI tmpText4;

    public NewBehaviourScript dataStorage;

    public bool hasFlower1;
    public bool hasFlower2;
    public bool hasFlower3;
    public bool hasFlower4;
    public bool hasFlower5;

    public gamemanager gamemanager;

    public bool hasKey1;
    public bool hasKey2;
    public bool hasKey3;

    void Update()
    {
        if (gamemanager.inventoryOpen)
        {
            DisplayIntegers();
        }
    }

    public void DisplayIntegers()
    {
        tmpText1.text = " " + dataStorage.heavyAmmo;
        tmpText2.text = " " + dataStorage.lightAmmo;
        tmpText3.text = " " + dataStorage.healthpacks;
        tmpText4.text = " " + dataStorage.armour;
    }

    public void OpenDesLA(GameObject DesLA)
    {
        DesLA.SetActive(true);
    }

    public void OpenDesHa(GameObject DesHa)
    {
        DesHa.SetActive(true);
    }

    public void OpenDesS(GameObject DesS)
    {
        DesS.SetActive(true);
    }

    public void OpenDesA(GameObject DesA)
    {
        DesA.SetActive(true);
    }
}

