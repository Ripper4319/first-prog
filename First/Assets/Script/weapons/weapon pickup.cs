using UnityEngine;
using NUnit;
using System.Collections;
using UnityEditor.Experimental;
using System.Runtime.CompilerServices;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;

public class WeaponPickup : MonoBehaviour
{
    public int weaponIndex;

    public bool canpickup = false;
    public float pickuprate = 2f;


    public Firearm weaponController;


    void Start()
    {
    }

    private void Update()
    {
        /*
        if (pickuprate >= 0)
            pickuprate -= Time.deltaTime;
        else
            canpickup = true;
    */
        canpickup = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && canpickup) 
        {
            UnlockWeapon(weaponIndex);
        }
    }

    public void UnlockWeapon(int weaponIndex)
    {
        if (weaponIndex >= 0 && weaponIndex < weaponController.weaponUnlocked.Length)
        {
            weaponController.UnlockWeapon(weaponIndex);
            weaponController.SetupWeapon(weaponIndex);
        }
    }



}


