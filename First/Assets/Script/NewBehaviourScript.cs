using NUnit;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine.Events;



public class NewBehaviourScript : MonoBehaviour
{
    Rigidbody theRB;
    Camera playercam;

    Transform camhold;

    public TextMeshProUGUI numberText;

    Vector2 camRotation;

    public bool isAiming = false;
    public float normalFOV = 60f;
    public float zoomFOV = 30f;
    public Transform gunTransform;
    public Vector3 gunADSPosition;
    public Vector3 gunNormalPosition;
    public Transform weaponslot;

    public GameObject inv;
    private bool inventoryOpen;

    public GameObject set;
    private bool settingsOpen;

    public GameObject HUD;
    private bool HUDActive;

    public GameObject Main;
    private bool MainOpen;



    [Header("Player Stats")]
    public int maxHealth = 5;
    public int Health = 5;
    public int healthRestore = 1;

    public float speed = 10.0f;
    public float sprintMultiplier = 2.5f;
    public float jumpHeight = 5.0f;
    public float groundDetectDistance = 1.5f;
    public bool sprintmode = false;

    public bool SprintToggleOption = false;
    public float mouseSensitivity = 2.0f;
    public float xsensitivity = 2.0f;
    public float ysensitivity = 2.0f;
    public float camRotationLimit = 90f;

    
    private Gun autoGun;        
    private bolt_action boltActionGun;

    void Start()
    {
        theRB = GetComponent<Rigidbody>();
        playercam = Camera.main;
        camhold = transform.GetChild(0);

        camRotation = Vector2.zero;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;

       

        set.SetActive(false);
        inv.SetActive(false);
        HUD.SetActive(false);
        Main.SetActive(false);

        gunNormalPosition = gunTransform.localPosition;
    }

    void Update()
    {
        playercam.transform.position = camhold.position;

        camRotation.x += Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.timeScale;
        camRotation.y += Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.timeScale;

        camRotation.y = Mathf.Clamp(camRotation.y, -camRotationLimit, camRotationLimit);


       
        playercam.transform.rotation = Quaternion.Euler(-camRotation.y, camRotation.x, 0);
        transform.localRotation = Quaternion.AngleAxis(camRotation.x, Vector3.up);

        if (Input.GetMouseButtonDown(1))
        {
            StartADS();
        }

        if (Input.GetMouseButtonUp(1))
        {
            StopADS();
        }

        if (isAiming)
        {
            playercam.fieldOfView = Mathf.Lerp(playercam.fieldOfView, zoomFOV, 0.1f);
            gunTransform.localPosition = Vector3.Lerp(gunTransform.localPosition, gunADSPosition, 0.1f);
        }
        else
        {
            playercam.fieldOfView = Mathf.Lerp(playercam.fieldOfView, normalFOV, 0.1f);
            gunTransform.localPosition = Vector3.Lerp(gunTransform.localPosition, gunNormalPosition, 0.1f);
        }



        Vector3 temp = theRB.velocity;
        float VerticalMove = Input.GetAxisRaw("Vertical") * Time.timeScale;
        float HorizontalMove = Input.GetAxisRaw("Horizontal") * Time.timeScale;

        if (!SprintToggleOption)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                sprintmode = true;

            if (Input.GetKey(KeyCode.LeftShift))
                sprintmode = false;
        }

        if (SprintToggleOption)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && VerticalMove > 0)
                sprintmode = true;

            if (VerticalMove <= 0)
                sprintmode = false;
        }

        if (!sprintmode)
            temp.x = VerticalMove * speed;

        if (sprintmode)
            temp.x = VerticalMove * speed * sprintMultiplier;

        temp.z = HorizontalMove * speed;
        temp.x = VerticalMove * speed;

        if (sprintmode)
            temp.x *= sprintMultiplier;
        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, -transform.up, groundDetectDistance))
            temp.y = jumpHeight;
        theRB.velocity = (temp.x * transform.forward) + (temp.z * transform.right) + (temp.y * transform.up);
        if (Input.GetKeyDown(KeyCode.I))
            Inventory();
        if (Input.GetKeyDown(KeyCode.Escape))
            Settings();

    }




    private void StartADS()
    {
        isAiming = true;
    }

    private void StopADS()
    {
        isAiming = false;
    }

    


    

    private void MainMenu()
    {
        if (MainOpen)
        {
            Main.SetActive(false);
            MainOpen = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            HUD.SetActive(true);
        }
        else
        {
            HUD.SetActive(false);
            Main.SetActive(true);
            MainOpen = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }

    public void ToggleBool()
    {
        Main.SetActive(false);
        Debug.Log("mybool in now:" + MainOpen);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("weapon"))
        {
            
            other.gameObject.transform.SetPositionAndRotation(weaponslot.position, weaponslot.rotation);
            other.gameObject.transform.SetParent(weaponslot);

            
            
        }
    }

}
    










