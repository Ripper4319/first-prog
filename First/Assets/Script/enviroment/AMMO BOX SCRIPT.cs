using System.Collections;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{

    [SerializeField] public AudioSource pickup;

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
                bud.LMGAmmo = 60;

                StartCoroutine(Play());
            }
        }
    }

    public IEnumerator Play()
    {
        pickup.Play();
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }
}
