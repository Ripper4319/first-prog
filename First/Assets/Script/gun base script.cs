
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float fireRate;
    public int ammoCount;

    public virtual void Fire()
    {
        // Basic fire functionality common to all guns
        Debug.Log("Firing a basic gun.");
    }
}

