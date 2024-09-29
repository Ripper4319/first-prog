using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    public Image crosshair; 
    public Transform gunslot; 
    public Camera mainCamera; 

    public float maxRaycastDistance = 100f; 

    void Update()
    {
        
        Ray ray = new Ray(gunslot.position, -gunslot.up);
        RaycastHit hit;

        
        if (Physics.Raycast(ray, out hit, maxRaycastDistance))
        {
            
            Vector3 screenPoint = mainCamera.WorldToScreenPoint(hit.point);

            
            crosshair.transform.position = screenPoint;
        }
        else
        {
            
            crosshair.transform.position = new Vector3(Screen.width / 2, Screen.height / 2);
        }
    }
}

