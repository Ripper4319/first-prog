using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject[] weapons; 
    public Transform weaponslot;  

    private int currentWeaponIndex = -1;

    void Start()
    {
        
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchWeapon(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SwitchWeapon(3);
    }

    private void SwitchWeapon(int weaponIndex)
    {
        
        if (weaponIndex >= 0 && weaponIndex < weapons.Length && weaponIndex != currentWeaponIndex)
        {
            
            if (currentWeaponIndex >= 0)
            {
                weapons[currentWeaponIndex].SetActive(false);
            }

            
            weapons[weaponIndex].SetActive(true);
            weapons[weaponIndex].transform.SetPositionAndRotation(weaponslot.position, weaponslot.rotation);
            weapons[weaponIndex].transform.SetParent(weaponslot);
            currentWeaponIndex = weaponIndex;

            Debug.Log($"Switched to weapon: {weapons[weaponIndex].name}");
        }
    }
}

