using UnityEngine;
using System.Collections;

public class grenadeprojectile : MonoBehaviour
{
    public float delay = 3f;

    float countdown;

    bool hasexploded;

    public GameObject explosionPrefab;
    public float strikeTimer = 3f;
    public float explosionRadius = 19f;
    public float explosionForce = 19f;
    public bool boomshake;
    private float explosionradius = 100;
    private float explosionforce = 2000;

    public Grenade Grenade;

    void Start()
    {
        countdown = delay;

        //Grenade = GameObject.Find("grenade").GetComponent<Grenade>();
    }

    void Update()
    {

        if(Grenade != null && Grenade.Grenadetriggered)
        {
            Grenadelogic();
        }
       
    }

    public void Grenadelogic()
    {
        StartCoroutine("BoomShake");
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


    private IEnumerator BoomShake()
    {
        yield return new WaitForSeconds(strikeTimer);
        Explode();
        hasexploded = true;

        boomshake = false;
        Destroy(gameObject);
    }


}

