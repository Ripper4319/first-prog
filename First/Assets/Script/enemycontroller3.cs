using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyController3 : MonoBehaviour
{
    public NewBehaviourScript player;
    public NavMeshAgent agent;
    public Transform enemy3;
    public GameObject enemy3projectile1; // This will be the 'copy' or 'projectile' of the enemy.
    public GameObject enemyModel; // Reference to the model that follows the player

    public float detectionRange = 2f;
    public Transform player1;

    [Header("Enemy Stats")]
    public int health = 3;
    public int maxHealth = 5;
    public int damageGiven = 1;
    public int damageRecieved = 3;
    public float pushBackForce = 10000;
    public float enemyspeed = 19;
    public bool diving = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<NewBehaviourScript>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.destination = player.transform.position;

        if (health <= 0)
            DestroyEnemy();

        float distanceToPlayer = Vector3.Distance(transform.position, player1.position);
        if (distanceToPlayer <= detectionRange && !diving)
        {
            diving = true;
            ActivateDive();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "shot")
        {
            health -= damageRecieved;
            Destroy(collision.gameObject);
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }


    public void ActivateDive()
    {
        GameObject diveCopy = Instantiate(enemy3projectile1, enemy3.position, enemy3.rotation);
        Rigidbody rb = diveCopy.GetComponent<Rigidbody>();
        rb.AddForce(enemy3.transform.forward * enemyspeed, ForceMode.Impulse);

        DestroyEnemy(); // Call method to destroy the enemy and associated model
    }

    
}





