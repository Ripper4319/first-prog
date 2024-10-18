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


    private WeaponControl weaponController;


    void Start()
    {

    }

    private void Update()
    {
        if (pickuprate >= 0)
            pickuprate -= Time.deltaTime;
        else
            canpickup = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && canpickup) 
        {
            if (weaponController != null)
            {
                weaponController.weaponUnlocked[weaponIndex] = true;  
                weaponController.SwitchWeapon(weaponIndex);
                Destroy(gameObject);  
            }
        }
    }

    
        


}


