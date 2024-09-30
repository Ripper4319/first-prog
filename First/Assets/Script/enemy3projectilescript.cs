using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class EnemyProjectileScript : MonoBehaviour
{
    public bool collidedwithplayer = false;
    public bool shotted = false;
    public GameObject explosionPrefab;
    public float explosionForce = 19f;
    public float explosionRadius = 19f;
    public int health = 3;
    private float explosionradius = 100;
    private float explosionforce = 2000;
    public bool boomshake;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

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
        // Any initialization code
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
        var surroundingObjects = Physics.OverlapSphere(transform.position, explosionradius);

        foreach (var obj in surroundingObjects)
        {
            var rb = obj.GetComponent<Rigidbody>();
            if (rb == null) continue;

            rb.AddExplosionForce(explosionforce, transform.position, explosionradius, 1);

            boomshake = true;
        }

        // Instantiate the explosion prefab
        GameObject explosionEffect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Fetch the particle system from the instantiated explosion
        ParticleSystem explosionParticles = explosionEffect.GetComponent<ParticleSystem>();

        if (explosionParticles != null)
        {
            // Destroy the explosion instance after the particles have finished playing
            Destroy(explosionEffect, explosionParticles.main.duration + explosionParticles.main.startLifetime.constantMax);
        }
        else
        {
            // Fallback if there's no particle system found, destroy immediately after a short time
            Destroy(explosionEffect, 0.5f);
        }

        // Apply shockwave force to nearby objects
        ApplyShockwave(transform.position, explosionRadius, explosionForce);

        // Destroy the current game object (the enemy or projectile)
        Destroy(gameObject);
    }
}







