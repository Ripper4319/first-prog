using TMPro;
using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour
{
    public float throwForce = .2f;
    public float fuseTime = 3f;
    public GameObject explosionPrefab;
    public GameObject grenadeprojectile; 
    public Transform weaponslot;
    public Camera playercam;
    public int currentgrenades = 3;
    public int maxgrenades = 4;
    public bool isthrowing = false;
    public float throwrate = 2f;
    public bool canthrow = true;
    public TextMeshProUGUI numberText;

    public bool Grenadetriggered = false;

    void Update()
    {
        if (Input.GetMouseButton(0) && currentgrenades > 0 && !isthrowing)
        {
            Grenadetriggered = true;
            ThrowGrenade();
            isthrowing = true;
        }

        numberText.text = "" + currentgrenades + " / " + maxgrenades;

        if (currentgrenades <= 0)
        {
            Deactivateboomboom();
        }

    }

    private void ThrowGrenade()
    {

        GameObject projectile = Instantiate(grenadeprojectile, weaponslot.position, weaponslot.rotation * Quaternion.Euler(90, 0, 0));
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(playercam.transform.forward * throwForce, ForceMode.Impulse);
            projectile.GetComponent<grenadeprojectile>().Grenadelogic();
        }
        else
        {

        }

        currentgrenades--;

        StartCoroutine(CooldownThrow());
    }

    private IEnumerator CooldownThrow()
    {
        yield return new WaitForSeconds(throwrate);
        isthrowing = false;
    }

    public void Deactivateboomboom()
    {
        gameObject.SetActive(false);
    }
}



