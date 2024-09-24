using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    public GameObject shot;
    public int weaponid = -1;
    public int firemode = 0;
    public float shotspeed = 15f;
    public float firerate = 0.25f;
    public float clipsize = 20;
    public float currentclip = 20;
    public float maxclip = 20;
    public float maxammo = 160;
    public float currentammo = 40;
    public float reloadamt = 20;
    public float bulletlifespan = 3f;
    public bool canfire = true;
    

    private Animator auto;

    void Start()
    {
        auto = GetComponent<Animator>();
    }

    public void Initialize()
    {
        
        switch (weaponid)
        {
            case 0: 
                shotspeed = 4500;
                break;
           
            default:
                break;
        }
    }

    public void FireWeapon(Vector3 position, Quaternion rotation)
    {
        GameObject s = Instantiate(shot, position, rotation);
        s.GetComponent<Rigidbody>().AddForce(rotation * Vector3.forward * shotspeed);
        Destroy(s, bulletlifespan);

        canfire = false;
        currentclip--;
        StartCoroutine(CooldownFire());

       auto.SetTrigger("shotsniper");
    }

    public void ReloadClip()
    {
        auto.SetTrigger("reload");

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


    public void SetupWeapon()
    {
        switch (gameObject.name)
        {
            case "weapon1":
                bulletlifespan = 10f;
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

            default:
                break;
        }
    }

    private IEnumerator CooldownFire()
    {
        yield return new WaitForSeconds(firerate);
        canfire = true;
    }
}

