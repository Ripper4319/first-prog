using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyController2 : MonoBehaviour //moving enemy base
{
    public NewBehaviourScript player;
    public NavMeshAgent agent;

    [Header("Enemy Stats")]
    public int health = 3;
    public int maxHealth = 5;
    public int damageGiven = 1;
    public int damageRecieved = 1;
    public float pushBackForce = 10000;
    public bool hit = false;
    public Transform player1;
    public float detectionRange = 10f;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<NewBehaviourScript>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player1.position);
        if (distanceToPlayer <= detectionRange)
        {
            animator.SetBool("isactive", true);

            agent.destination = player.transform.position;

            if (health <= 0)
                Destroy(gameObject);
        }
          
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
        if (collision.gameObject.tag == "player")
        {
            player.Health--;
        }
    }

    public void togglehit()
    {
        hit = false;
    }
}