using NUnit;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Firearm : MonoBehaviour
{
    [Header("Weapon Stats")]
    public int weaponID;
    public float shotVel;
    public int fireMode;
    public float fireRate;
    public int currentClip;
    public int clipSize;
    public int maxAmmo;
    public int currentAmmo;
    public int reloadAmt;
    public float bulletLifespan;
    public float casingspeed;

    [Header("Weapon Library")]
    public bool useWeapon0 = true; // Nagant Revolver
    public bool useWeapon1 = true; // RPK
    public bool CanFire = true;
    public Transform camera;
    public TextMeshProUGUI textMeshPro; 

    [Header("Weapon Objects")]
    public GameObject shot;
    public GameObject muzzleFlashPrefab;
    public GameObject[] casingPrefabs;
    public bool gunshake;
    public NewBehaviourScript playerAmmo;
    public Transform gunTransform;

    [Header("Weapon Models")]
    public GameObject[] weapons;
    public GameObject[] weaponModels;
    public Transform weaponslot;
    public GameObject[] weaponpickups;
    private int currentWeaponIndex = -1;
    public bool[] weaponUnlocked;
    public Animator weapon1A;

    [Header("Shake")]
    public float gunShakeIntensity = 2f;
    public float shakeDuration = 0.5f;

    [Header("Weapon Locational Data")]
    public GameObject[] bulletInstantiators;
    public GameObject[] casingInstantiators;

    private void Start()
    {
    }

    void Update()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons.Length > i && weaponUnlocked[i])
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    SwitchWeapon(i);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        if (Input.GetMouseButtonDown(0) && currentAmmo > 0)
        {
            if (currentClip > 0)
            {

                Debug.Log("shot");

                Fire();
            }
            else
            {
                Reload();
            }
        }

        textMeshPro.text = $" {currentClip}";
    }

    public void SetupWeapon(int id)
    {
        switch (id)
        {
            case 0 when useWeapon0: // Nagant Revolver
                weaponID = 0;
                shotVel = 400f; // Adjust shot velocity
                fireMode = 0; // Semi-auto
                fireRate = .5f; // Shots per second
                currentClip = 7; 
                clipSize = 7;
                maxAmmo = 30; // Total ammo
                currentAmmo = 30;
                reloadAmt = 7;
                bulletLifespan = 2.0f; // Bullet lifespan
                break;

            case 1 when useWeapon1: // RPK
                weaponID = 1;
                shotVel = 800f; // Adjust shot velocity
                fireMode = 1; // Full-auto
                fireRate = 0.1f; // Shots per second
                currentClip = 30; // Standard clip size
                clipSize = 30;
                maxAmmo = 120; // Total ammo
                currentAmmo = 120;
                reloadAmt = 30; // Reload 30 at once
                bulletLifespan = 2.0f; // Bullet lifespan
                break;

            default:
                break;
        }
    }

    public void Fire()
    {

        if (Time.timeScale == 1)
        {
            if (weaponID == 0)
            {
                weapon1A.SetBool("isfiring", true);
            }

            GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, gunTransform.position, gunTransform.rotation);
            gunshake = true;
            StartCoroutine(camshake());

            GameObject bulletInstantiator = bulletInstantiators[weaponID];
            GameObject projectile = Instantiate(shot, bulletInstantiator.transform.position, bulletInstantiator.transform.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(camera.transform.forward * shotVel, ForceMode.Impulse);
            }

            currentClip--;
            CanFire = false;

            Destroy(projectile, bulletLifespan);
            Destroy(muzzleFlash, 0.1f);

            StartCoroutine(CooldownFire());
            StartCoroutine(GunAction());
        }
    }

    public void SwitchWeapon(int weaponIndex)
    {
        if (weaponIndex >= 0 && weaponIndex < weapons.Length && weaponUnlocked[weaponIndex])
        {
            if (currentWeaponIndex >= 0)
            {
                weapons[currentWeaponIndex].SetActive(false);
                weaponModels[currentWeaponIndex].SetActive(false);
            }

            weapons[weaponIndex].SetActive(true);
            weaponModels[weaponIndex].SetActive(true);
            currentWeaponIndex = weaponIndex;

            SetupWeapon(weaponIndex);
        }
        else
        {
            Debug.LogError("Cannot switch to weapon: either the index is invalid or the weapon is locked.");
        }
    }

    public void Reload()
    {
        if (currentClip >= clipSize) return;

        int reloadCount = clipSize - currentClip;
        int availableAmmo = playerAmmo.GetCurrentAmmo(weaponID);

        if (availableAmmo < reloadCount)
        {
            currentClip += availableAmmo;
            playerAmmo.DecreaseAmmo(weaponID, availableAmmo);
        }
        else
        {
            currentClip += reloadCount;
            playerAmmo.DecreaseAmmo(weaponID, reloadCount);
        }
    }

    private IEnumerator CooldownFire()
    {
        yield return new WaitForSeconds(fireRate);
        CanFire = true;
        weapon1A.SetBool("Isfiring", false);
    }

    IEnumerator GunAction()
    {
        yield return new WaitForSeconds(0.01f);
    }

    private IEnumerator camshake()
    {
        yield return new WaitForSeconds(.2f);
        gunshake = false;
    }
}

