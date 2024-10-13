using UnityEngine;

public class Crouch : MonoBehaviour
{
    public float crouchSpeed = 2f;
    public float normalHeight = 2.0f;
    public float crouchHeight = 1.0f;
    private bool crouching = false;

    public CapsuleCollider capsuleCollider;
    public Rigidbody rb;

    public LayerMask elevator;
    public float groundCheckDistance = 0.2f;

    private bool isGrounded = false;
    private bool onElevator = false;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GroundCheck();

        if ((isGrounded || onElevator) && Input.GetKeyDown(KeyCode.C))
        {
            crouching = !crouching;
        }

        AdjustCrouch();
    }

    void AdjustCrouch()
    {
        if (crouching)
        {
            capsuleCollider.height = Mathf.MoveTowards(capsuleCollider.height, crouchHeight, crouchSpeed * Time.deltaTime);
            capsuleCollider.center = new Vector3(0, crouchHeight / 2, 0);
        }
        else
        {
            capsuleCollider.height = Mathf.MoveTowards(capsuleCollider.height, normalHeight, crouchSpeed * Time.deltaTime);
            capsuleCollider.center = new Vector3(0, normalHeight / 2, 0);
        }
    }

    void GroundCheck()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, elevator))
        {
            isGrounded = true;

            if (hit.collider.CompareTag("Elevator"))
            {
                onElevator = true;
            }
            else
            {
                onElevator = false;
            }
        }
        else
        {
            isGrounded = false;
            onElevator = false;
        }
    }
}


