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

    private Rigidbody theRB;
    Camera playercam;

    public TextMeshProUGUI numberText;

    Vector2 camRotation;

    public int bulletlifespan;
    public int weaponid;
    public int firemode;
    public float firerate;
    public int clipsize;
    public int currentclip;
    public int maxclip;
    public int maxammo;
    public int currentammo;
    public int reloadamt;
    public float shotspeed;

    public GameObject shot;
    public bool canfire = true;
    public Transform weaponslot;

    public bool isAiming = false;
    public float normalFOV = 60f;
    public float zoomFOV = 30f;
    public Transform gunTransform;
    public Vector3 gunADSPosition;
    public Vector3 gunNormalPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "weapon")
        {
            other.gameObject.transform.SetPositionAndRotation(weaponslot.position, weaponslot.rotation);

            other.gameObject.transform.SetParent(weaponslot);

            switch (other.gameObject.name)
            {
                case "weapon2":

                    weaponid = 1;
                    shotspeed = 10000;
                    firemode = 0;
                    firerate = 0.25f;
                    currentclip = 20;
                    clipsize = 20;
                    maxammo = 400;
                    currentammo = 200;
                    reloadamt = 20;
                    bulletlifespan = 1;
                    break;

                default:
                    break;
            }
        }
    }


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
    }

    void Start()
    {
        theRB = GetComponent<Rigidbody>();
        playercam = transform.GetChild(0).GetComponent<Camera>();

        camRotation = Vector2.zero;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;

        gunNormalPosition = gunTransform.localPosition;


        
    }

    public void FireWeapon()
    {
        GameObject s = Instantiate(shot, weaponslot.position, weaponslot.rotation);
        s.GetComponent<Rigidbody>().AddForce(playercam.transform.forward * shotspeed);
        Destroy(s,bulletlifespan);

        canfire = false;
        currentclip--;
        StartCoroutine("cooldownfire");


        StartCoroutine(CooldownFire());
    }

    public void ReloadClip()
    {
        if (currentclip >= clipsize) return;

       

        int reloadCount = clipsize - currentclip;

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


