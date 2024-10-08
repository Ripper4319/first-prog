using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrosshairController : MonoBehaviour
{

   
    public Image crosshair;
    public Camera cam;
    public Transform cam1;
    public float interactDistance = 5f;

    public bool hasChanged = false;

    [SerializeField]
    private float maxRaycastDistance = 100;
    [SerializeField]
    private LayerMask mask;

    public playerui playerUI;

    private void Start()
    {

    }

    void Update()
    {
        Ray ray = new Ray(cam1.position, cam.transform.forward);
        RaycastHit hitinfo;

        if (Physics.Raycast(ray, out hitinfo, maxRaycastDistance, mask))
        {
            Debug.Log("Test");

            if (hitinfo.collider.GetComponent<interactable>() != null)
            {
                playerUI.UpdateText(hitinfo.collider.GetComponent<interactable>().promptmessage);
            }
        }
        else
        {
            playerUI.UpdateText(string.Empty);
        }
       
    }
}

