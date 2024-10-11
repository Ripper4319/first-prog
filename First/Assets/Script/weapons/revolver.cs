using NUnit;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine.Events;
using Unity.VisualScripting;

public class revolver : MonoBehaviour
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
    public GameObject muzzleFlashPrefab;
    public bool gunshake;
    public NewBehaviourScript recoilcontroller;
    public float weaponrecoil = 30;


    [Header("Weapon Stats")]
    public GameObject shot;
    public GameObject casing;
    public int weaponid = 1;
    public int firemode = 0;
    public float shotspeed = 100f;
    public float casingspeed = 3f;
    public float firerate = .7f;
    public int clipsize = 5;
    public float currentclip = 5;
    private int shotsfired = 0;
    public float maxclip = 5f;
    public float maxammo = 20f;
    public float currentammo = 10;
    public float reloadamt = 45f;
    public float bulletlifespan = 5f;
    public bool canfire = true;


    public Camera direction;

    public Transform weaponslot;


    public NewBehaviourScript playerAmmo;


    //huh2

    private void Update()
    {
        if (Input.GetMouseButton(0) && canfire && currentclip > 0 && weaponid >= 0)
        {
            FireWeapon();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadClip();
        }

        numberText.text = " " + currentclip + " / " + playerAmmo.heavyAmmo;

        if (recoilcontroller.recoilapplied)
        {
            recoilcontroller.recoil = 0;
            recoilcontroller.recoilapplied = false;
        }
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
        GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);


        GameObject projectile = Instantiate(shot, weaponslot.position, weaponslot.rotation * Quaternion.Euler(90, 0, 0));
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(playercam.transform.forward * shotspeed, ForceMode.Impulse);

        currentclip--;
        shotsfired++;

        canfire = false;

        Destroy(projectile, 2f);
        StartCoroutine
            (CooldownFire());
        Destroy(muzzleFlash, 0.1f);

        gunshake = true;

        StartCoroutine
            (camshake());



    }


    
    public void GunAction()
    {
        
        GameObject casing1 = Instantiate(casing, weaponslot.position, weaponslot.rotation * Quaternion.Euler(90, 0, 0));

        
        Rigidbody rb = casing1.GetComponent<Rigidbody>();

        
        Vector3 randomForce = new Vector3(
            Random.Range(1f, 2f),  //x
            Random.Range(0.5f, 1.5f),  //y
            Random.Range(-0.5f, 0.5f)   //z
        ) * casingspeed;

        rb.AddForce(randomForce, ForceMode.Impulse);

        
        Destroy(casing1, 1f);

        shotsfired--;
    }


    public void ReloadClip()
    {
        if (currentclip >= clipsize) return;

        int reloadCount = (int)(clipsize - currentclip);

        if (playerAmmo.heavyAmmo < reloadCount)
        {
            currentclip += playerAmmo.heavyAmmo;
            playerAmmo.heavyAmmo = 0;
        }
        else
        {
            currentclip += reloadCount;
            playerAmmo.heavyAmmo -= reloadCount;
        }

        
        StartCoroutine(EjectCasings());

    }

    private IEnumerator EjectCasings()
    {
        for (int i = 0; i < shotsfired; i++)
        {
            GunAction();
            yield return new WaitForSeconds(0.1f);  
        }
    }


    private IEnumerator CooldownFire()
    {
        yield return new WaitForSeconds(firerate);
        canfire = true;
    }

    private IEnumerator camshake()
    {
        yield return new WaitForSeconds(.2f);
        gunshake = false;
    }
}
