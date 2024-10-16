using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTeleporter : MonoBehaviour
{
    public Vector3 TeleportPosition = new Vector3(0, 1, 0);
    public ColorChangeBriefly Fadeinout;
    public NewBehaviourScript player;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = TeleportPosition;

            player.Health--;
        }
    }

    public void ToggleFog(bool isEnabled)
    {
        RenderSettings.fog = isEnabled;
    }

    public IEnumerator Fade()
    {

        yield return StartCoroutine(Fadeinout.FadeIn());
    }
}
