using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public Transform objectToShake; // Assign any object you want to shake (camera, gun, etc.)
    public float gunShakeIntensity = .6f;
    public float boomShakeIntensity = 0.3f;
    public float shakeDuration = 0.5f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;


    private Coroutine currentShakeRoutine;

    public Auto script1;
    public revolver script2;
    public bolt_action script3;
    public m4 script5;
    public EnemyProjectileScript script4;

    public bool gunshake = false;
    public bool boomshake = false;
    private bool isShaking = false;

    void Start()
    {
        // Save the original position and rotation
        if (objectToShake == null)
        {
            objectToShake = transform; // Default to the object this script is attached to
        }

        originalPosition = objectToShake.localPosition;
        originalRotation = objectToShake.localRotation;
    }

    void Update()
    {
        // Check for gunshake and boomshake from external scripts
        bool gunshakeActive = script1.gunshake || script2.gunshake || script3.gunshake || script5.gunshake;
        bool boomshakeActive = script4.boomshake;

        // Start shaking if one of the bools is true
        if (gunshakeActive && currentShakeRoutine == null)
        {
            currentShakeRoutine = StartCoroutine(Shake(gunShakeIntensity, shakeDuration));
        }
        else if (boomshakeActive && currentShakeRoutine == null)
        {
            currentShakeRoutine = StartCoroutine(Shake(boomShakeIntensity, shakeDuration));
        }

        // Stop shaking if neither shake condition is active
        if (!gunshakeActive && !boomshakeActive && currentShakeRoutine != null)
        {
            StopCoroutine(currentShakeRoutine);
            currentShakeRoutine = null;
            ResetShake();
        }

        
    }

    IEnumerator Shake(float intensity, float duration)
    {
        isShaking = true;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            // Apply random offset and slight rotation to the object
            Vector3 randomOffset = Random.insideUnitSphere * intensity;
            Quaternion randomRotation = Quaternion.Euler(
                Random.Range(-intensity * 60f, intensity * 60f),
                Random.Range(-intensity * 60f, intensity * 60f),
                0
            );

            objectToShake.localPosition = originalPosition + randomOffset;
            objectToShake.localRotation = originalRotation * randomRotation;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ResetShake();
        currentShakeRoutine = null;
    }

    private void ResetShake()
    {
        // Reset the object to its original position and rotation
        objectToShake.localPosition = originalPosition;
        objectToShake.localRotation = originalRotation;
    }

    internal void TriggerShake(float v)
    {
        throw new System.NotImplementedException();
    }
}

