using System.Collections;
using UnityEngine;

public class auto : MonoBehaviour
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

    // Setup automatic weapon parameters
    public void SetupWeapon()
    {
        bulletlifespan = 5; // Example values for auto gun
        weaponid = 1;
        firemode = 1;
        firerate = 0.1f;
        clipsize = 30;
        currentclip = 30;
        maxclip = 5;
        maxammo = 150;
        currentammo = 120;
        reloadamt = 30;
        shotspeed = 900f;

        Debug.Log("Automatic weapon equipped!");
    }

    // Additional methods for firing, reloading, etc.
    public void FireWeapon()
    {
        // Automatic fire logic here
    }

    public void ReloadWeapon()
    {
        // Reload logic here
    }
}



