using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public Transform objectToShake;
    public float gunShakeIntensity = 2f;
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
    public BasicEnemyController script6;

    public bool gunshake = false;
    public bool boomshake = false;
    private bool isShaking = false;

    void Start()
    {
        
        if (objectToShake == null)
        {
            objectToShake = transform; 
        }

        originalPosition = objectToShake.localPosition;
        originalRotation = objectToShake.localRotation;
    }

    void Update()
    {
        
        bool gunshakeActive = script1.gunshake || script2.gunshake || script3.gunshake || script5.gunshake || script6.gunshake;
        bool boomshakeActive = script4.boomshake;

        
        if (gunshakeActive && currentShakeRoutine == null)
        {
            currentShakeRoutine = StartCoroutine(Shake(gunShakeIntensity, shakeDuration));
        }
        else if (boomshakeActive && currentShakeRoutine == null)
        {
            currentShakeRoutine = StartCoroutine(Shake(boomShakeIntensity, shakeDuration));
        }

        
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
        
        objectToShake.localPosition = originalPosition;
        objectToShake.localRotation = originalRotation;
    }

    internal void TriggerShake(float v)
    {
        throw new System.NotImplementedException();
    }
}

