using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public enum AmmoType { Type1, Type2 }
    public AmmoType ammoType;

    public NewBehaviourScript bud;

    public int ammoAmount = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NewBehaviourScript playerAmmo = other.GetComponent<NewBehaviourScript>();
            if (playerAmmo != null)
            {
                bud.revAmmo = 28;
                bud.M4Ammo = 60;
                bud.BoltAmmo = 60;
                bud.LMGAmmo = 60;

                Destroy(gameObject);
            }
        }
    }
}
