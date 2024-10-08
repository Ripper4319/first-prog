using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Light directionalLight;
    public Vector3 teleportPosition = new Vector3(0, 2, 11000);
    public ColorChangeBriefly fadeinout;
    public bool isone = true;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Fade());

            Debug.Log("huh");

            other.transform.position = teleportPosition;
            
            if (isone)
            {
                DeactivateDirectionalLight();
                ToggleFog(false);
            }
            
        }
    }

    public void DeactivateDirectionalLight()
    {
        if (directionalLight != null)
        {
            directionalLight.enabled = false;
        }
    }

    public void ToggleFog(bool isEnabled)
    {
        RenderSettings.fog = isEnabled;
    }

    public IEnumerator Fade()
    {
       
        yield return StartCoroutine(fadeinout.FadeIn());
    }
}
