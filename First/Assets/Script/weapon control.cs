using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public GameObject[] weapons; // Array of weapons
    public Transform weaponslot;  // Where weapons are equipped

    public GameObject[] weaponpickups;


    private int currentWeaponIndex = -1; // Tracks currently equipped weapon
    public bool[] weaponUnlocked; // Tracks which weapons are unlocked

    void Start()
    {

        weaponUnlocked = new bool[weapons.Length];


        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
            weaponUnlocked[i] = false;
        }
    }

    void Update()
    {
        if (weapons.Length > 0 && weaponUnlocked[0] && Input.GetKeyDown(KeyCode.Alpha1))
            SwitchWeapon(0);
        if (weapons.Length > 1 && weaponUnlocked[1] && Input.GetKeyDown(KeyCode.Alpha2))
            SwitchWeapon(1);
        if (weapons.Length > 2 && weaponUnlocked[2] && Input.GetKeyDown(KeyCode.Alpha3))
            SwitchWeapon(2);
        if (weapons.Length > 3 && weaponUnlocked[3] && Input.GetKeyDown(KeyCode.Alpha4))
            SwitchWeapon(3);
    }

    public void SwitchWeapon(int weaponIndex)
    {

        if (weaponIndex >= 0 && weaponIndex < weapons.Length)
        {
            if (weaponIndex != currentWeaponIndex)
            {
                if (currentWeaponIndex >= 0)
                {
                    weapons[currentWeaponIndex].SetActive(false);
                }


                weapons[weaponIndex].SetActive(true);
                weapons[weaponIndex].transform.SetPositionAndRotation(weaponslot.position, weaponslot.rotation);
                weapons[weaponIndex].transform.SetParent(weaponslot); 

                if (weapons[weaponIndex].GetComponent<Auto>() != null)
                    weapons[weaponIndex].GetComponent<Auto>().canfire = true;

                if (weapons[weaponIndex].GetComponent<bolt_action>() != null)
                    weapons[weaponIndex].GetComponent<bolt_action>().canfire = true;

                if (weapons[weaponIndex].GetComponent<revolver>() != null)
                    weapons[weaponIndex].GetComponent<revolver>().canfire = true;


                currentWeaponIndex = weaponIndex;

                EnableWeapon(weaponIndex);

                Debug.Log($"Switched to weapon: {weapons[weaponIndex].name}");
            }
            else
            {
                weapons[currentWeaponIndex].SetActive(false);


                weapons[weaponIndex].SetActive(false);
                weapons[weaponIndex].transform.SetParent(null); // Attach weapon to weapon slot

                weaponUnlocked[weaponIndex] = false;

                Instantiate(weaponpickups[weaponIndex], weaponslot.position, weaponslot.rotation);

                currentWeaponIndex = -1;
            }
        }
    }

    public void EnableWeapon(int weaponIndex)
    {

        if (weaponIndex >= 0 && weaponIndex < weapons.Length)
        {
            weaponUnlocked[weaponIndex] = true;
            Debug.Log($"Weapon {weaponIndex} unlocked!");
        }
    }
}

