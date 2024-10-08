using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public enum AmmoType { Type1, Type2 }
    public AmmoType ammoType;

    public int ammoAmount = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NewBehaviourScript playerAmmo = other.GetComponent<NewBehaviourScript>();
            if (playerAmmo != null)
            {
                if (ammoType == AmmoType.Type1)
                {
                    playerAmmo.AddlightAmmo(ammoAmount);
                }
                else if (ammoType == AmmoType.Type2)
                {
                    playerAmmo.AddheavyAmmo(ammoAmount);
                }

                Destroy(gameObject);
            }
        }
    }
}
