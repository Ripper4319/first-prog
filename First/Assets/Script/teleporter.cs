using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Light directionalLight;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = new Vector3(0, 2, 11000);
            DeactivateDirectionalLight();
            ToggleFog(false);
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
}
