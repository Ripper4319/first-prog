using NUnit;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEditor.Experimental;
using System.Runtime.CompilerServices;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using UnityEngine.UIElements.Experimental;

public class BasicEnemyController : MonoBehaviour
{
    //public playercontroller;


    public int health = 3;
    public int maxHealth = 3;
    public Transform player;
    public float detectionRange = 10f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 20f;
    public float fireRate = 2f;
    private float nextFireTime = 0f;
    public GameObject muzzleFlashPrefab;
    public bool gunshake;
    public bool hit = false;

    void Update()
    {
        if (health <= 0)
            Destroy(gameObject);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            transform.LookAt(player);

            if (Time.time >= nextFireTime)
            {
                ShootAtPlayer();
                nextFireTime = Time.time + fireRate;
            }
        }

       

    }

    private void ShootAtPlayer()
    {
        GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0, 90, 0));
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = (player.position - firePoint.position).normalized * projectileSpeed;

        Destroy(projectile, 2f);
        Destroy(muzzleFlash, 0.1f);
        gunshake = true;
        StartCoroutine(camshake());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "shot")
        {
            Destroy(collision.gameObject);
            health--;
            hit = true;
        }
        if (collision.gameObject.tag == "SHOTBIG")
        {
            Destroy(collision.gameObject);
            health -= 999;
            hit = true;
        }
    }

    public void togglehit()
    {
        hit = false;
    }

    private IEnumerator camshake()
    {
        yield return new WaitForSeconds(.2f);
        gunshake = false;
    }
}

