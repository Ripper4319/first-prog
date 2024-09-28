using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour
{
    public bool collidedwithplayer = false;
    public bool shotted = false;
    public GameObject explosionPrefab;  
    public float explosionForce = 19f; 
    public float explosionRadius = 19f;
    public int health = 3;
    public GameObject explosionbase;
    private Animator cloneAnimator;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collidedwithplayer = true;

            droneexplode();
            
        }

        if (collision.gameObject.CompareTag("shot"))
        {
            
            health--;
            shotted = true;
            droneexplode();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("SHOTBIG"))
        {
            
            health -= 999;
            shotted = true;
            droneexplode();
            Destroy(collision.gameObject);
        }
    }

    private void Start()
    {
        
    }

    void ApplyShockwave(Vector3 explosionPosition, float radius, float force)
    {
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(force, explosionPosition, radius);
            }
        }
    }
    public void TakeDamage()
    {
        health -= 1;

        if (health <= 0)
        {
            droneexplode();
        }
    }

    void droneexplode()
    {
        GameObject explosionEffect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Instantiate(explosionbase, transform.position, Quaternion.identity);
        PlayClonedAnimation();

        ApplyShockwave(transform.position, explosionRadius, explosionForce);

        Destroy(explosionEffect, 0.5f);

        Destroy(gameObject);

        Destroy(animator);
    }

    private void PlayClonedAnimation()
    {
        if (clonedClip != null)
        {
            cloneAnimator.SetTrigger("StartAnimation");
        }
        else
        {
            Debug.LogWarning("Cloned animation clip is null.");
        }
    }

}






