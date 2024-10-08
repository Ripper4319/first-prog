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

public class turret_gun: MonoBehaviour
{
    public Camera playercam;

    private Rigidbody theRB;



    public Transform firePoint;
    public bool gunshake;

    [Header("Weapon Stats")]
    public GameObject shot;
    public GameObject casing;
    public float shotspeed = 100f;
    public float casingspeed = 3f;
    public float firerate = 0.1f;
    public float reloadamt = 45f;
    public float bulletlifespan = 5f;
    public bool canfire = true;
    public Transform weaponslot;
    public NewBehaviourScript newBehaviourScript;
    public GameObject muzzleFlashPrefab;


    public Camera direction;

    void Start()
    {


       

    }

    void Update()
    {

        


    }


    private void FireWeapon()
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

        canfire = false;

        Destroy(projectile, 2f);
        StartCoroutine(CooldownFire());
        Destroy(muzzleFlash, 0.1f);

        gunshake = true;


        StartCoroutine(camshake());
    }


    public void GunAction()
    {
        GameObject casing1 = Instantiate(casing, weaponslot.position, weaponslot.rotation * Quaternion.Euler(90, 0, 0));
        Rigidbody rb = casing1.GetComponent<Rigidbody>();
        rb.AddForce(playercam.transform.right * casingspeed, ForceMode.Impulse);

        Destroy(casing1, 1f);
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
