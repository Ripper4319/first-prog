using UnityEngine;
using UnityEngine.AI;

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

        //agent.destination = player.transform.position;

        if (health <= 0) ;
            //Destroy(GameObject);
    }

    private void ShootAtPlayer()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = (player.position - firePoint.position).normalized * projectileSpeed;

        Destroy(projectile, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "shot")
        {
            Destroy(collision.gameObject);
            health--;
        }
    }
}

