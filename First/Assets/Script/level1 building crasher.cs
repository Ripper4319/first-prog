using UnityEngine;

public class CollisionExplosionRotator : MonoBehaviour
{
    public GameObject explosionprefab;
    public GameObject rotater;
    public float rotationspeed = 100f;
    public int explosions = 6;
    public Transform[] explosionspawnpoints;
    private bool shouldrotate = false;

    void Update()
    {
        if (shouldrotate && rotater != null)
        {
            RotateObject();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            for (int i = 0; i < explosions; i++)
            {
                Vector3 spawnPosition = explosionspawnpoints != null && explosionspawnpoints.Length > 0
                    ? explosionspawnpoints[i % explosionspawnpoints.Length].position
                    : transform.position + Random.insideUnitSphere * 2f;

                Instantiate(explosionprefab, spawnPosition, Quaternion.identity);
            }

            shouldrotate = true;
        }
    }

    void RotateObject()
    {
        rotater.transform.Rotate(Vector3.up * rotationspeed * Time.deltaTime);
    }
}

