using NUnit;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine.Events;

public class bolt_action : MonoBehaviour
{

    public Camera playercam;

    private Rigidbody theRB;


    public TextMeshProUGUI numberText;

    Vector2 camRotation;

    public bool isAiming = false;
    public float normalFOV = 60f;
    public float zoomFOV = 30f;
    public Transform gunTransform;
    public Transform firePoint;
    public Vector3 gunADSPosition;
    public Vector3 gunNormalPosition;


    [Header("Weapon Stats")]
    public GameObject shot;
    public GameObject casing;
    public int weaponid = 1;
    public int firemode = 0;
    public float shotspeed = 100f;
    public float firerate = 3.5f;
    public int clipsize = 5;
    public float currentclip = 5;
    public float maxclip = 5f;
    public float maxammo = 20f;
    public float currentammo = 10;
    public float reloadamt = 45f;
    public float bulletlifespan = 5f;
    public bool canfire = true;

    public Camera direction;

    public Transform weaponslot;


    


    private void Update()
    {
        if (Input.GetMouseButton(0) &&canfire && currentclip > 0 && weaponid >= 0)
        {
            FireWeapon();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
             ReloadClip();
        }

        numberText.text = "Ammo: " + currentclip;
    }

    void Start()
    {
        camRotation = Vector2.zero;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;


        gunNormalPosition = gunTransform.localPosition;



    }


    public void FireWeapon()
    {
        GameObject projectile = Instantiate(shot, weaponslot.position, weaponslot.rotation * Quaternion.Euler(90, 0, 0));
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(playercam.transform.forward * shotspeed, ForceMode.Impulse);

        currentclip--;

        canfire = false;

        Destroy(projectile, 2f);
        StartCoroutine
            (CooldownFire());

        GunAction();

    }

    public void GunAction()
    {
        GameObject projectile = Instantiate(casing, weaponslot.position, weaponslot.rotation * Quaternion.Euler(90, 0, 0));
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(playercam.transform.forward * shotspeed, ForceMode.Impulse);


    }

    public void ReloadClip()
    {
        if (currentclip >= clipsize) return;

       

        int reloadCount = (int)(clipsize - currentclip);

        if (currentammo < reloadCount)
        {
            currentclip += currentammo;
            currentammo = 0;
        }
        else
        {
            currentclip += reloadCount;
            currentammo -= reloadCount;
        }

    }

    private IEnumerator CooldownFire()
    {
        yield return new WaitForSeconds(firerate);
        canfire = true;
    }
}


