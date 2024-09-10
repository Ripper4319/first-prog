using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody theRB;
    Camera playercam;

    Vector2 camRotation;

    public bool sprintmode = false;

    [Header("Movement Settings")]
    public float speed = 10.0f;
    public float sprintMultiplier = 2.5f;
    public float jumpHeight = 5.0f;
    public float groundDetectDistance = 1.5f;
   

    [Header("User Settings")]
    public bool SprintToggleOption = false;
    public float mouseSensitivity = 2.0f;
    public float xsensitivity = 2.0f;
    public float ysensitivity = 2.0f;
    public float camRotationLimit = 90f;
    
    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody>();
        playercam = transform.GetChild(0).GetComponent<Camera>();

        camRotation = Vector2.zero;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        camRotation.x += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        camRotation.y += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        camRotation.y = Mathf.Clamp(camRotation.y, -camRotationLimit, camRotationLimit);

        playercam.transform.localRotation = Quaternion.AngleAxis(camRotation.y, Vector3.left);
        transform.localRotation = Quaternion.AngleAxis(camRotation.x, Vector3.up);

        theRB = GetComponent<Rigidbody>();

        Vector3 temp = theRB.velocity;
        if (!SprintToggleOption)
        { 
         if (Input.GetKey(KeyCode.LeftShift))
            sprintmode = true;

         if (Input.GetKey(KeyCode.LeftShift))
            sprintmode = false;
        }

        float VerticalMove = Input.GetAxisRaw("Vertical");
        float HorizontalMove = Input.GetAxisRaw("Horizontal");

        if (SprintToggleOption)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && VerticalMove > 0)
                sprintmode = true;

            if (VerticalMove <= 0)
                sprintmode = false;
        }

        if (!sprintmode)
            temp.x = VerticalMove * speed;

        if (sprintmode)
            temp.x = VerticalMove * speed * sprintMultiplier;

        temp.z = VerticalMove * speed;

        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, -transform.up, groundDetectDistance))
            temp.y = jumpHeight;

        theRB.velocity = (temp.x * transform.forward) + (temp.z * transform.right) + (temp.y * transform.up);
    }
}
