using UnityEngine;
using System.Collections;

public class grenadeprojectile : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float explosionRadius = 19f;
    public float explosionForce = 19f;
    public bool boomshake;

    void Start()
    {
        
        StartCoroutine(ExplodeAfterDelay());
    }

    private IEnumerator ExplodeAfterDelay()
    {
       
        yield return new WaitForSeconds(5f);

        Explode();
    }

    void OnCollisionEnter(Collision collision)
    {
        
        Explode();
    }

    private void Explode()
    {
        boomshake = true;
        StartCoroutine(BoomShake());

        GameObject explosionEffect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        
        ParticleSystem explosionParticles = explosionEffect.GetComponent<ParticleSystem>();
        if (explosionParticles != null)
        {
            Destroy(explosionEffect, explosionParticles.main.duration + explosionParticles.main.startLifetime.constantMax);
        }
        else
        {
            Destroy(explosionEffect, 0.5f);
        }

        
        ApplyShockwave(transform.position, explosionRadius, explosionForce);

        
        Destroy(gameObject);
    }

    private void ApplyShockwave(Vector3 explosionPosition, float radius, float force)
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

    private IEnumerator BoomShake
        ()
    {
        yield return new WaitForSeconds(1f);
        boomshake = false;
    }
}

