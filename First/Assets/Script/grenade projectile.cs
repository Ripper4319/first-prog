using UnityEngine;
using System.Collections;

public class grenadeprojectile : MonoBehaviour
{
    public float delay = 3f;

    float countdown;

    bool hasexploded;

    public GameObject explosionPrefab;
    public float explosionRadius = 19f;
    public float explosionForce = 19f;
    public bool boomshake;
    private float explosionradius = 100;
    private float explosionforce = 2000;

    private Grenade Grenade;

    void Start()
    {
        countdown = delay;

        Grenade = GetComponent<Grenade>();
    }

    void Update()
    {

        if(Grenade != null && Grenade.Grenadetriggered)
        {
            Grenadelogic();
        }
       
    }

    void Grenadelogic()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasexploded)
        {
            Explode();
            hasexploded = true;
        }
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

    void Explode()
    {
        var surroundingObjects = Physics.OverlapSphere(transform.position, explosionradius);

        boomshake = true;
        StartCoroutine(BoomShake());

        foreach (var obj in surroundingObjects)
        {
            var rb = obj.GetComponent<Rigidbody>();
            if (rb == null) continue;

            rb.AddExplosionForce(explosionforce, transform.position, explosionRadius, 1);


        }

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


    private IEnumerator BoomShake
        ()
    {
        yield return new WaitForSeconds(1f);
        boomshake = false;
    }


}

