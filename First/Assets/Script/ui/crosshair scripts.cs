using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrosshairController : MonoBehaviour
{

   
    public Image crosshair;
    public Camera cam;
    public Transform cam1;
    public float interactDistance = 5f;

    private LayerMask mask;

    [SerializeField]
    private float maxRaycastDistance = 100;

    void start()
    {
        //h
    }
   
    

    void Update()
    {
        Ray ray = new Ray(cam1.position, cam.transform.forward);
        RaycastHit hitinfo;

        if (Physics.Raycast(ray, out hitinfo, maxRaycastDistance, mask))
        {

            if (hitinfo.collider.GetComponent<interactable>()! = null )
            {
                //huh
            }
        }
       
    }
}

