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

        weaponController = FindFirstObjectByType<WeaponControl>();
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
        if (collision.gameObject.CompareTag("Player") && canpickup)  // Assuming the player has the tag "Player"
        {
            if (weaponController != null)
            {
                weaponController.weaponUnlocked[weaponIndex] = true;  // Unlock the weapon in WeaponController
                weaponController.SwitchWeapon(weaponIndex);
                Destroy(gameObject);  // Destroy the pickup after collecting
            }
            else
            {
                Debug.LogError("WeaponController is not assigned!");
            }
        }
    }

}


