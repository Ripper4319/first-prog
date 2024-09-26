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

    [Header("Weapon Stats")]
    public GameObject shot;
    public int weaponid = 0;
    public int firemode = 0; 
    public float shotspeed = 100f; 
    public float firerate = 0.1f; 
    public float clipsize = 200f; 
    public float currentclip = 200;
    public float maxclip = 200f; 
    public float reloadamt = 45f; 
    public float bulletlifespan = 5f; 
    public bool canfire = true;
    public Transform weaponslot;
    public NewBehaviourScript newBehaviourScript;

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

       
        if (Input.GetMouseButton(0) && canfire && currentclip > 0 && weaponid >= 0)
        {
            FireWeapon();
        }

        
        numberText.text = "Ammo: " + currentclip;
        
    }

    private void StartADS() => isAiming = true;
    private void StopADS() => isAiming = false;


    private void FireWeapon()
    {
        GameObject projectile = Instantiate(shot,weaponslot.position, weaponslot.rotation * Quaternion.Euler(90, 0, 0));
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(playercam.transform.forward * shotspeed, ForceMode.Impulse);

        currentclip--;

        canfire = false;

        Destroy(projectile, 2f);
        StartCoroutine
            (CooldownFire());

    }
    

    private IEnumerator CooldownFire()
    {
        yield return new WaitForSeconds(firerate);
        canfire = true;
    }
}





