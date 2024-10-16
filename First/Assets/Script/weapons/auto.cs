using NUnit;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEditor.Experimental;
using System.Runtime.CompilerServices;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using UnityEngine.UIElements.Experimental;

public class Auto : MonoBehaviour
{
    public Camera playercam;

    private Rigidbody theRB;

    private Animator rpkAnimator;

    public TextMeshProUGUI numberText;

    Vector2 camRotation;

    public bool isAiming = false;
    public float normalFOV = 60f;
    public float zoomFOV = 30f;
    public Transform gunTransform;
    public Transform firePoint;
    public Vector3 gunADSPosition;
    public Vector3 gunNormalPosition;
    public bool gunshake;

    [Header("Weapon Stats")]
    public GameObject shot;
    public GameObject casing;
    public int weaponid = 0;
    public int firemode = 0; 
    public float shotspeed = 100f; 
    public float casingspeed = 3f;
    public float firerate = 0.1f; 
    public float clipsize = 200f; 
    public float currentclip = 200;
    public float maxclip = 200f; 
    public float reloadamt = 45f; 
    public float bulletlifespan = 5f; 
    public bool canfire = true;
    public Transform weaponslot;
    public NewBehaviourScript newBehaviourScript;
    public GameObject muzzleFlashPrefab;

    public NewBehaviourScript playerAmmo;

    public Camera direction;

    void Start()
    {
        

        camRotation = Vector2.zero;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;


        gunNormalPosition = gunTransform.localPosition;

        
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(1)) StartADS();
        if (Input.GetMouseButtonUp(1)) StopADS();

       
        if (Input.GetMouseButton(0) && canfire && playerAmmo.lightAmmo > 0 && weaponid >= 0)
        {
            FireWeapon();
        }

        
        numberText.text = " " + currentclip;
        
    }

    private void StartADS() => isAiming = true;
    private void StopADS() => isAiming = false;


    private void FireWeapon()
    {

        if (Time.timeScale == 1)
        {
            GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
            GameObject projectile = Instantiate(shot, weaponslot.position, weaponslot.rotation * Quaternion.Euler(90, 0, 0));
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce(playercam.transform.forward * shotspeed, ForceMode.Impulse);
            }
            else
            {

            }

            GameObject casing1 = Instantiate(casing, weaponslot.position, weaponslot.rotation * Quaternion.Euler(90, 0, 0));
            Rigidbody rb1 = casing1.GetComponent<Rigidbody>();
            rb1.AddForce(playercam.transform.right * casingspeed, ForceMode.Impulse);

            Destroy(casing1, 1f);

            currentclip--;
            canfire = false;

            Destroy(projectile, 2f);
            StartCoroutine(CooldownFire());
            Destroy(muzzleFlash, 0.1f);

            gunshake = true;


            StartCoroutine(camshake());
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





