using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public int weaponIndex;

    public float pickuprate = 2f;
    private float pickupCooldown; // Cooldown timer
    public Firearm weaponController;

    void Start()
    {
        pickupCooldown = 0f; // Initialize cooldown timer
    }

    private void Update()
    {
        if (pickupCooldown > 0)
        {
            pickupCooldown -= Time.deltaTime; // Reduce the cooldown
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && pickupCooldown <= 0)
        {
            if (weaponController != null)
            {
                weaponController.weaponUnlocked[weaponIndex] = true;
                weaponController.SwitchWeapon(weaponIndex);
                Destroy(gameObject);

                pickupCooldown = pickuprate; 
            }
            else
            {
                Debug.LogError("WeaponController is not assigned!");
            }
        }
    }
}



