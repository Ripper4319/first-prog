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

    // Setup bolt action weapon parameters
    public void SetupWeapon()
    {
        bulletlifespan = 10; // Example values for bolt action gun
        weaponid = 2;
        firemode = 0;
        firerate = 1.5f;
        clipsize = 5;
        currentclip = 5;
        maxclip = 5;
        maxammo = 50;
        currentammo = 20;
        reloadamt = 5;
        shotspeed = 865f;

        Debug.Log("Bolt action weapon equipped!");
    }

    // Additional methods for firing, reloading, etc.
    public void FireWeapon()
    {
        // Bolt-action fire logic here
    }

    public void ReloadWeapon()
    {
        // Reload logic here
    }
}


