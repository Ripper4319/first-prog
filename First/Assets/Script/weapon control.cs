using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject[] weapons; // Array to hold references to the weapon GameObjects
    public Transform weaponslot;  // Reference to the transform where weapons are assigned

    private int currentWeaponIndex = -1;

    void Start()
    {
        
    }

    void Update()
    {
        // Check for weapon switch inputs (1, 2, 3, 4)
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchWeapon(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SwitchWeapon(3);
    }

    private void SwitchWeapon(int weaponIndex)
    {
        // Check if the selected index is valid and different from the current weapon
        if (weaponIndex >= 0 && weaponIndex < weapons.Length && weaponIndex != currentWeaponIndex)
        {
            // Deactivate the current weapon
            if (currentWeaponIndex >= 0)
            {
                weapons[currentWeaponIndex].SetActive(false);
            }

            // Activate the new weapon and set it as the current weapon
            weapons[weaponIndex].SetActive(true);
            weapons[weaponIndex].transform.SetPositionAndRotation(weaponslot.position, weaponslot.rotation);
            weapons[weaponIndex].transform.SetParent(weaponslot);
            currentWeaponIndex = weaponIndex;

            Debug.Log($"Switched to weapon: {weapons[weaponIndex].name}");
        }
    }
}

