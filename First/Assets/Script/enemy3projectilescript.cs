using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyProjectileScript : MonoBehaviour
{
    public bool collidedwithplayer = false;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the projectile has collided with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            collidedwithplayer = true;
            Destroy(gameObject);  // Destroy this object if it hits the player
        }
    }
}




