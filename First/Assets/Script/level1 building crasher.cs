using UnityEngine;

public class CollisionExplosionRotator : MonoBehaviour
{
    public GameObject explosionprefab;
    public GameObject rotater;
    public float rotationspeed = .0005f;
    public int explosions = 6;
    public Transform[] explosionspawnpoints;
    private bool shouldrotate = false;
    private float currentRotationZ = 0f;
    private float targetRotationZ = 90f;

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
        if (currentRotationZ < targetRotationZ)
        {
            float rotationStep = rotationspeed * Time.deltaTime;
            rotater.transform.Rotate(Vector3.forward * rotationStep);
            currentRotationZ += rotationStep;

            if (currentRotationZ >= targetRotationZ)
            {
                rotater.transform.rotation = Quaternion.Euler(0, 0, targetRotationZ);
                currentRotationZ = targetRotationZ;
                shouldrotate = false; // Stop rotating once target is reached
            }
        }
    }
}


