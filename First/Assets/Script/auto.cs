using NUnit;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEditor.Experimental;

public class RPKWeapon : MonoBehaviour
{
    Camera playercam;

    private Rigidbody theRB;

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
    public int firemode = 0; 
    public float shotspeed = 700f; 
    public float semiAutoFireRate = 0.1f; 
    public float fullAutoFireRate = 0.05f; 
    public float firerate; 
    public float clipsize = 45f; 
    public float currentclip = 0;
    public float maxclip = 45f;
    public float maxammo = 150f; 
    public float currentammo = 0;
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

        rpkAnimator = GetComponent<Animator>();

        gunNormalPosition = gunTransform.localPosition;

        
        firerate = semiAutoFireRate;
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(1)) StartADS();
        if (Input.GetMouseButtonUp(1)) StopADS();

       
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            ToggleFireMode();
        }

       
        if (Input.GetMouseButton(0) && canfire && currentclip > 0 && weaponid >= 0)
        {
            FireWeapon();
        }

        
        if (Input.GetKeyDown(KeyCode.R)) ReloadClip();

        
        numberText.text = currentclip.ToString();
    }

    private void StartADS() => isAiming = true;
    private void StopADS() => isAiming = false;

    private void ToggleFireMode()
    {
        if (firemode == 0)
        {
            firemode = 1;
            firerate = fullAutoFireRate;
        }
        else 
        {
            firemode = 0; 
            firerate = semiAutoFireRate;
        }

        Debug.Log("Fire mode changed to: " + (firemode == 0 ? "Semi-Auto" : "Full-Auto"));
    }

    private void FireWeapon()
    {
        direction = newBehaviourScript.playercam;
        GameObject s = Instantiate(shot, weaponslot.position, weaponslot.rotation);
        s.GetComponent<Rigidbody>().AddForce(direction.transform.forward * shotspeed);
        Destroy(s, bulletlifespan);

        canfire = false;
        currentclip--;

        
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





