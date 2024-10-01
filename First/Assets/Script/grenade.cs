using TMPro;
using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour
{
    public float throwForce = 3f;
    public float fuseTime = 3f;
    public GameObject explosionPrefab;
    public GameObject grenadeprojectile; 
    public Transform weaponslot;
    public int currentgrenades = 3;
    public int maxgrenades = 4;
    public bool isthrowing = false;
    public float throwrate = 2f;
    public TextMeshProUGUI numberText;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentgrenades > 0)
        {
            StartCoroutine(ThrowGrenade());
        }

        numberText.text = "" + currentgrenades + " / " + maxgrenades;

        if (currentgrenades <= 0)
        {
            Destroy(gameObject); 
        }
    }

    private IEnumerator ThrowGrenade()
    {
        if (!isthrowing && currentgrenades > 0)
        {
            isthrowing = true;
            currentgrenades--;

            
            if (grenadeprojectile != null)
            {
                
                GameObject grenade = Instantiate(grenadeprojectile, weaponslot.position, weaponslot.rotation);
                Rigidbody rb = grenade.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.AddForce(weaponslot.forward * throwForce, ForceMode.Impulse);
                }
            }
            else
            {
                Debug.LogError("Grenade projectile is missing!");
            }

            yield return new WaitForSeconds(throwrate);

            isthrowing = false;
        }
    }
}



