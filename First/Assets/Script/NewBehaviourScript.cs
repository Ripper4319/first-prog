using NUnit;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody theRB;
    Camera playercam;

    private Animator sniper;

    public TextMeshProUGUI numberText;

    Vector2 camRotation;

    public bool isAiming = false;
    public float normalFOV = 60f;
    public float zoomFOV = 30f;
    public Transform gunTransform;
    public Vector3 gunADSPosition;
    public Vector3 gunNormalPosition;

    public GameObject inv;
    private bool inventoryOpen;

    public GameObject set;
    private bool settingsOpen;



    [Header("Player Stats")]
    public int maxHealth = 5;
    public int Health = 5;
    public int healthRestore = 1;

    [Header("Weapon Stats")]
    public GameObject shot;
    public int weaponid = -1;
    public int firemode = 0;
    public float shotspeed = 15f;
    public float firerate = 0;
    public float clipsize = 0;
    public float currentclip = 0;
    public float maxclip = 0;
    public float maxammo = 0;
    public float currentammo = 0;
    public float reloadamt = 0;
    public float bulletlifespan = 0;
    public bool canfire = true;
    public Transform weaponslot;

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

    void Start()
    {
        theRB = GetComponent<Rigidbody>();
        playercam = transform.GetChild(0).GetComponent<Camera>();

        camRotation = Vector2.zero;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        sniper = GetComponent<Animator>();

        set.SetActive(false);
        inv.SetActive(false);

        gunNormalPosition = gunTransform.localPosition;
    }

    void Update()
    {
        camRotation.x += Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.timeScale;
        camRotation.y += Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.timeScale;

        camRotation.y = Mathf.Clamp(camRotation.y, -camRotationLimit, camRotationLimit);

        playercam.transform.localRotation = Quaternion.AngleAxis(camRotation.y, Vector3.left);
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

        if (Input.GetMouseButton(0) && canfire && currentclip > 0 && weaponid >= 0)
        {
            FireWeapon();
        }

        if (Input.GetKeyDown(KeyCode.R))
            reloadclip();

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

        numberText.text = currentclip.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "weapon")
        {
            other.gameObject.transform.SetPositionAndRotation(weaponslot.position, weaponslot.rotation);

            other.gameObject.transform.SetParent(weaponslot);

            switch (other.gameObject.name)
            {
                case "weapon1":

                    bulletlifespan = 3;
                    weaponid = 0;
                    firemode = 0;
                    firerate = 0.25f;
                    clipsize = 20;
                    currentclip = 20;
                    maxclip = 20;
                    maxammo = 160;
                    currentammo = 40;
                    reloadamt = 20;
                    shotspeed = 4500;
                    break;

                default: break;


            }
        }





    }

    private void StartADS()
    {
        isAiming = true;
    }

    private void StopADS()
    {
        isAiming = false;
    }

    private void FireWeapon()
    {
        GameObject s = Instantiate(shot, weaponslot.position, weaponslot.rotation);
        s.GetComponent<Rigidbody>().AddForce(playercam.transform.forward * shotspeed);
        Destroy(s, bulletlifespan);

        canfire = false;
        currentclip--;
        StartCoroutine("cooldownfire");

        Debug.Log("triggering shotsniper");
        sniper.SetTrigger("shotsniper");
    }


    private void reloadclip()
    {
        currentclip = 0;
        if (currentclip >= clipsize)
            return;

        else
        {
            sniper.SetTrigger("reload");

            float reloadcount = clipsize - currentclip;

            if (currentammo < reloadcount)
            {
                currentclip += currentammo;

                currentammo = 0;
                return;
            }
            else
            {
                currentclip += reloadcount;

                currentammo -= reloadcount;

                return;
            }
        }
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
        }
        else
        {
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
        }
        else
        {
            inv.SetActive(true);
            inventoryOpen = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }


    IEnumerator cooldownfire()
    {
        yield return new WaitForSeconds(firerate);
        canfire = true;
    }
}









