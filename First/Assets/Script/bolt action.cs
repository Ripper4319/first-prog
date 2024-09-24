using NUnit;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine.Events;
public class WeaponScript : MonoBehaviour
{
    public GameObject shot;
    public int weaponid = -1;
    public int firemode = 0;
    public float shotspeed = 865f; 
    public float firerate = 1.5f;
    public float clipsize = 5f;   
    public float currentclip = 5f;
    public float maxclip = 5f;
    public float maxammo = 50f;    
    public float currentammo = 20f; 
    public float reloadamt = 5f;   
    public float bulletlifespan = 10f; 
    public bool canfire = true;
    public Transform weaponslot;

    private Animator sniper;
    private Camera playercam;
    public TextMeshProUGUI numberText;

    void Start()
    {
        sniper = GetComponent<Animator>();
        playercam = transform.GetChild(0).GetComponent<Camera>();
    }

    void Update()
    {
        numberText.text = currentclip.ToString();

        if (Input.GetMouseButton(0) && canfire && currentclip > 0 && weaponid >= 0)
        {
            FireWeapon();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadClip();
        }
    }

    private void FireWeapon()
    {
        GameObject s = Instantiate(shot, weaponslot.position, weaponslot.rotation);
        s.GetComponent<Rigidbody>().AddForce(playercam.transform.forward * shotspeed);
        Destroy(s, bulletlifespan);

        canfire = false;
        currentclip--;
        StartCoroutine(CooldownFire());

        Debug.Log("triggering shotsniper");
        sniper.SetTrigger("shotsniper");
    }

    private void ReloadClip()
    {
        currentclip = 0;
        if (currentclip >= clipsize)
            return;

        sniper.SetTrigger("reload");

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "weapon")
        {
            other.gameObject.transform.SetPositionAndRotation(weaponslot.position, weaponslot.rotation);
            other.gameObject.transform.SetParent(weaponslot);

            switch (other.gameObject.name)
            {
                case "weapon1":
                    bulletlifespan = 10; 
                    weaponid = 0;
                    firemode = 0;
                    firerate = 1.5f;  
                    clipsize = 5;
                    currentclip = 5;
                    maxclip = 5;
                    maxammo = 50;
                    currentammo = 20;
                    reloadamt = 5;
                    shotspeed = 865f; 
                    break;

                default: break;
            }
        }
    }

    IEnumerator CooldownFire()
    {
        yield return new WaitForSeconds(firerate);
        canfire = true;
    }
}

