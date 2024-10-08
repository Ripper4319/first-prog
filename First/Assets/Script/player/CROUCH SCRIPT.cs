using UnityEngine;

public class Crouch : MonoBehaviour
{
    public float crouchSpeed = 2f;
    public float normalHeight = 2.0f;
    public float crouchHeight = 1.0f;
    private bool crouching = false;

    public CapsuleCollider capsuleCollider;
    public Rigidbody rb;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
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
}

