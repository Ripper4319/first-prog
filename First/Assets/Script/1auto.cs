using NUnit;
using TMPro;
using UnityEngine;
using System.Collections;

public class RPKWeapon : MonoBehaviour
{
    private Rigidbody theRB;
    Camera playercam;

    private Animator rpkAnimator;

    public TextMeshProUGUI numberText;

    Vector2 camRotation;

    public bool isAiming = false;
    public float normalFOV = 60f;
    public float zoomFOV = 30f;
    public Transform gunTransform;
    public Vector3 gunADSPosition;
    public Vector3 gunNormalPosition;

    [Header("Weapon Stats")]
    public GameObject shot;
    public int weaponid = -1;
    public int firemode = 0; // 0: semi-auto, 1: full-auto
    public float shotspeed = 700f; // Muzzle velocity in m/s
    public float semiAutoFireRate = 0.1f; // 600 RPM (0.1 seconds between shots)
    public float fullAutoFireRate = 0.05f; // 1200 RPM (0.05 seconds between shots)
    public float firerate; // Variable fire rate based on mode
    public float clipsize = 45f; // Standard magazine capacity
    public float currentclip = 0;
    public float maxclip = 45f;
    public float maxammo = 150f; // Total ammo including reserves
    public float currentammo = 0;
    public float reloadamt = 45f; // Amount reloaded per action
    public float bulletlifespan = 5f; // Time before bullet is destroyed
    public bool canfire = true;
    public Transform weaponslot;

    void Start()
    {
        theRB = GetComponent<Rigidbody>();
        playercam = transform.GetChild(0).GetComponent<Camera>();

        camRotation = Vector2.zero;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;

        rpkAnimator = GetComponent<Animator>();

        gunNormalPosition = gunTransform.localPosition;

        // Set initial fire rate to semi-auto
        firerate = semiAutoFireRate;
    }

    void Update()
    {
        // Aiming logic
        if (Input.GetMouseButtonDown(1)) StartADS();
        if (Input.GetMouseButtonUp(1)) StopADS();

        // Fire mode switching
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            ToggleFireMode();
        }

        // Firing logic
        if (Input.GetMouseButton(0) && canfire && currentclip > 0 && weaponid >= 0)
        {
            FireWeapon();
        }

        // Reload logic
        if (Input.GetKeyDown(KeyCode.R)) ReloadClip();

        // Update UI
        numberText.text = currentclip.ToString();
    }

    private void StartADS() => isAiming = true;
    private void StopADS() => isAiming = false;

    private void ToggleFireMode()
    {
        if (firemode == 0) // If currently semi-auto
        {
            firemode = 1; // Switch to full-auto
            firerate = fullAutoFireRate;
        }
        else // If currently full-auto
        {
            firemode = 0; // Switch to semi-auto
            firerate = semiAutoFireRate;
        }

        Debug.Log("Fire mode changed to: " + (firemode == 0 ? "Semi-Auto" : "Full-Auto"));
    }

    private void FireWeapon()
    {
        GameObject s = Instantiate(shot, weaponslot.position, weaponslot.rotation);
        s.GetComponent<Rigidbody>().AddForce(playercam.transform.forward * shotspeed);
        Destroy(s, bulletlifespan);

        canfire = false;
        currentclip--;

        // Use the current firerate for cooldown
        StartCoroutine(CooldownFire());

        rpkAnimator.SetTrigger("Fire");
    }

    private void ReloadClip()
    {
        if (currentclip >= clipsize) return;

        rpkAnimator.SetTrigger("Reload");

        float reloadCount = clipsize - currentclip;

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





