using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    public Image crosshair;
    public Transform gunslot;
    public Camera mainCamera;
    public float interactDistance = 5f;
    public LayerMask interactableitems;
    public Text objectname;
    public float maxRaycastDistance = 100;

    void Update()
    {
        Ray ray = new Ray(gunslot.position, -gunslot.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxRaycastDistance, interactableLayer))
        {
            Vector3 screenPoint = mainCamera.WorldToScreenPoint(hit.point);
            crosshair.transform.position = screenPoint;

            if (hit.distance <= interactDistance)
            {
                objectname.text = hit.collider.gameObject.name;
            }
        }
        else
        {
            crosshair.transform.position = new Vector3(Screen.width / 2, Screen.height / 2);
            objectname.text = "";
        }
    }
}

