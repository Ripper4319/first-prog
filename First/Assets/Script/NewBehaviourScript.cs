using NUnit;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


public class NewBehaviourScript : MonoBehaviour
{
    Rigidbody theRB;
    public Camera playercam;

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
    public gamemanager gamemanager;



    [Header("Player Stats")]
    public int maxHealth = 8;
    public int Health = 8;
    public int healthRestore = 1;
    public int explosiondamage = 4;

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

    public float fallDamageThreshold = -10f;  
    public float fallDamageMultiplier = 2f;  

    private bool isGrounded;
    private Vector3 lastVelocity;

    private CameraShake cameraShake;

    public gamemanager isnotalive;

    public int lightAmmo = 0;
    public int heavyAmmo = 0;



    void Start()
    {
        theRB = GetComponent<Rigidbody>();
        cameraShake = FindObjectOfType<CameraShake>();

        isnotalive = gamemanager.GetComponent<gamemanager>();

        playercam = Camera.main;
        camhold = transform.GetChild(0);

        camRotation = Vector2.zero;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;

        gunNormalPosition = gunTransform.localPosition;

        cameraShake = FindObjectOfType<CameraShake>();
    }

    void Update()
    {
        if (Health <= 0)
        {
            gamemanager.End();
        }

        lastVelocity = theRB.velocity;

        if (isGrounded && lastVelocity.y < fallDamageThreshold)
        {
            ApplyFallDamage(lastVelocity.y);

        }

        playercam.transform.position = camhold.position;

        camRotation.x += Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.timeScale;
        camRotation.y += Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.timeScale;

        camRotation.y = Mathf.Clamp(camRotation.y, -camRotationLimit, camRotationLimit); playercam.transform.position = camhold.position;

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


        
    }

    public void AddlightAmmo(int amount)
    {
        lightAmmo += amount;
        Debug.Log("Added " + amount + " Type1 Ammo. Total: " + lightAmmo);
    }

    public void AddheavyAmmo(int amount)
    {
        heavyAmmo += amount;
        Debug.Log("Added " + amount + " Type2 Ammo. Total: " + heavyAmmo);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("enemy3"))
        {
            Health--;
        }

        if (collision.gameObject.CompareTag("Ground") || collision.contacts[0].normal.y > 0.9f)
        {
            isGrounded = true;
           // Mathf.Lerp
        }

        if (collision.gameObject.CompareTag("weapon") && (collision.gameObject.GetComponent<WeaponPickup>() != null && collision.gameObject.GetComponent<WeaponPickup>().canpickup))
        {

            collision.gameObject.transform.SetPositionAndRotation(weaponslot.position, weaponslot.rotation);
            collision.gameObject.transform.SetParent(weaponslot);
            


        }
        if (collision.gameObject.CompareTag("healthpickup") && Health < maxHealth)
        {
            Destroy(collision.gameObject);
            Health++;
        }
        if (collision.gameObject.CompareTag("shot"))
        {
            Destroy(collision.gameObject);
            Health--;
        }
        if (collision.gameObject.CompareTag("SHOTBIG"))
        {
            Destroy(collision.gameObject);
            Health--;
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

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void ApplyFallDamage(float fallSpeed)
    {
        int damage = Mathf.FloorToInt((fallDamageThreshold - fallSpeed) * fallDamageMultiplier);

        if (damage > 0)
        {
            Health -= damage;
        }
    }

}












