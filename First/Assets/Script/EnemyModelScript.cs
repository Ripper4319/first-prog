using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModelScript : MonoBehaviour
{
    public BasicEnemyController3 enemyController;
    public int health = 3; 
    public bool isEnemyDead = false; 

    void Start()
    {
        
        if (enemyController == null)
        {
            enemyController = GetComponentInParent<BasicEnemyController3>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "shot")
        {
            Destroy(collision.gameObject);
            health--;
        }
        if (collision.gameObject.tag == "SHOTBIG")
        {
            Destroy(collision.gameObject);
            health -= 999;
        }
    }

    public void TakeDamage()
    {
        health -= 1; 

        if (health <= 0)
        {
            isEnemyDead = true; 
            Destroy(gameObject);
            enemyController?.DestroyEnemy();
        }
    }
}

