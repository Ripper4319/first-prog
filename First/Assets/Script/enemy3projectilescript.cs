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
    private Animator explosion;
    private bool isexploding;

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
        isexploding = false;
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

        
        Animator explosionAnimator = explosionEffect.GetComponent<Animator>();
        if (explosionAnimator != null)
        {
            explosionAnimator.SetBool("isexploding", true);
        }
        else
        {
            Debug.LogWarning("No Animator found on explosion effect prefab.");
        }

        
        ParticleSystem explosionParticles = explosionEffect.GetComponent<ParticleSystem>();

        
        if (explosionParticles != null)
        {
            
            Destroy(explosionEffect, explosionParticles.main.duration + explosionParticles.main.startLifetime.constantMax);
        }
        else
        {
           
            Destroy(explosionEffect, .05f);
        }

        
        ApplyShockwave(transform.position, explosionRadius, explosionForce);

        
        isexploding = false;

        
        Destroy(gameObject);
    }



    private void PlayClonedAnimation(GameObject explosionBaseInstance)
    {
        Animator cloneAnimator = explosionBaseInstance.GetComponent<Animator>(); 

        if (cloneAnimator != null)
        {
            
            cloneAnimator.SetTrigger("explode");
        }
        else
        {
            Debug.LogWarning("No Animator found on explosion base instance.");
        }
    }


}






