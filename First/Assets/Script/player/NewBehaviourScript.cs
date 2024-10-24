
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class NewBehaviourScript : MonoBehaviour
{
    Rigidbody theRB;
    public Camera playercam;

    Transform camhold;

    public TextMeshProUGUI numberText;

    Vector2 camRotation;

    public bool isAiming = false;
    public float normalFOV = 60f;
    public float zoomFOV = 30f;
    public Transform gunTransform;
    public Vector3 gunADSPosition;
    public Vector3 gunNormalPosition;
    public Transform weaponslot;
    public gamemanager gamemanager;
    public float recoil = 0;



    [Header("Player Stats")]
    public int maxHealth = 8;
    public int Health = 8;
    public int healthRestore = 1;
    public int explosiondamage = 4;

    public float speed = 10.0f;
    public float sprintMultiplier = 2.5f;
    public float jumpHeight = 5.0f;
    public float groundDetectDistance = 1.5f;
    public bool sprintmode = false;

    public float mouseSensitivity = 2.0f;
    public float xsensitivity = 2.0f;
    public float ysensitivity = 2.0f;
    public float camRotationLimit = 90f;
    public bool recoilapplied = false;


    public float fallDamageThreshold = -10f;  
    public float fallDamageMultiplier = 2f;  

    private bool isGrounded;
    private Vector3 lastVelocity;


    public gamemanager isnotalive;

    [Header("leaning")]
    public float leanAmount = 100f;
    public float leanSpeed = 5f;
    private float targetLean = 30f;
    private float currentLean = 0f;


    [Header("Ammo")]
    public int revAmmo = 28;
    public int M4Ammo = 90;
    public int BoltAmmo = 20;
    public int LMGAmmo = 200;
    public bool levelevent = false;
    public int healthpacks;
    public int armour;



    void Start()
    {
        theRB = GetComponent<Rigidbody>();

        isnotalive = gamemanager.GetComponent<gamemanager>();


        playercam = Camera.main;
        camhold = transform.GetChild(0);

        camRotation = Vector2.zero;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;

        gunNormalPosition = gunTransform.localPosition;


    }

    void Update()
    {
        if (Health <= 0)
        {
            gamemanager.End();
        }

        lastVelocity = theRB.velocity;

        if (isGrounded && lastVelocity.y < fallDamageThreshold && !levelevent)
        {
            ApplyFallDamage(lastVelocity.y);

        }

        
        camRotation.x += Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.timeScale;
        camRotation.y += Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.timeScale;

        playercam.transform.rotation = Quaternion.Euler(-camRotation.y, camRotation.x, 0);

        camRotation.y = Mathf.Clamp(camRotation.y, -camRotationLimit, camRotationLimit);

        transform.rotation = Quaternion.Euler(0, camRotation.x, 0);

        playercam.transform.rotation = Quaternion.Euler(-camRotation.y, camRotation.x, 0);

        playercam.transform.position = camhold.position;

        if (Input.GetMouseButtonDown(1))
        {
            StartADS();
        }

        if (Input.GetMouseButtonUp(1))
        {
            StopADS();
        }


        if (isAiming)
        {
            playercam.fieldOfView = Mathf.Lerp(playercam.fieldOfView, zoomFOV, 0.1f);
            gunTransform.localPosition = Vector3.Lerp(gunTransform.localPosition, gunADSPosition, 0.1f);
        }
        else
        {
            playercam.fieldOfView = Mathf.Lerp(playercam.fieldOfView, normalFOV, 0.1f);
            gunTransform.localPosition = Vector3.Lerp(gunTransform.localPosition, gunNormalPosition, 0.1f);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            targetLean = leanAmount;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            targetLean = -leanAmount;
        }
        else
        {
            targetLean = 0f;
        }

        currentLean = Mathf.Lerp(currentLean, targetLean, Time.deltaTime * leanSpeed);
        playercam.transform.localRotation = Quaternion.Euler(playercam.transform.localRotation.eulerAngles.x, playercam.transform.localRotation.eulerAngles.y, currentLean);


        Vector3 temp = theRB.velocity;
        float VerticalMove = Input.GetAxisRaw("Vertical") * Time.timeScale;
        float HorizontalMove = Input.GetAxisRaw("Horizontal") * Time.timeScale;


       
            if (Input.GetKeyDown(KeyCode.LeftShift) && VerticalMove > 0)
                sprintmode = true;

            if (VerticalMove <= 0)
                sprintmode = false;
        

        if (!sprintmode)
            temp.x = VerticalMove * speed;

        if (sprintmode)
            temp.x = VerticalMove * speed * sprintMultiplier;

        temp.z = HorizontalMove * speed;
        temp.x = VerticalMove * speed;

        if (sprintmode)
            temp.x *= sprintMultiplier;
        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, -transform.up, groundDetectDistance))
            temp.y = jumpHeight;
        theRB.velocity = (temp.x * transform.forward) + (temp.z * transform.right) + (temp.y * transform.up);

    }



    public void DecreaseAmmo(int weaponID, int amount)
    {
        switch (weaponID)
        {
            case 0:
                revAmmo = Mathf.Max(0, revAmmo - amount);
                break;
            case 1:
                M4Ammo = Mathf.Max(0, M4Ammo - amount);
                break;
            case 2:
                BoltAmmo = Mathf.Max(0, BoltAmmo - amount);
                break;
        }
    }

    public int GetCurrentAmmo(int weaponID)
    {
        return weaponID switch
        {
            0 => revAmmo,      // M1911
            1 => M4Ammo,      // M4
            2 => BoltAmmo,     // FN SCAR
            3 => LMGAmmo,      // HK416
            _ => 0
        };
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("enemy3"))
        {
            Health--;
        }

        if (collision.gameObject.CompareTag("Ground") || collision.contacts[0].normal.y > 0.9f)
        {
            isGrounded = true;
           // Mathf.Lerp
        }

        if (collision.gameObject.CompareTag("weapon") && (collision.gameObject.GetComponent<WeaponPickup>() != null && collision.gameObject.GetComponent<WeaponPickup>().canpickup))
        {

            collision.gameObject.transform.SetPositionAndRotation(weaponslot.position, weaponslot.rotation);
            collision.gameObject.transform.SetParent(weaponslot);
            


        }
        if (collision.gameObject.CompareTag("healthpickup") && Health < maxHealth)
        {
            Destroy(collision.gameObject);
            Health++;
        }
        if (collision.gameObject.CompareTag("shot"))
        {
            Destroy(collision.gameObject);
            Health--;
        }
        if (collision.gameObject.CompareTag("SHOTBIG"))
        {
            Destroy(collision.gameObject);
            Health--;
        }


    }

    private void StartADS()
    {
        isAiming = true;
    }

    private void StopADS()
    {
        isAiming = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void ApplyFallDamage(float fallSpeed)
    {
        int damage = Mathf.FloorToInt((fallDamageThreshold - fallSpeed) * fallDamageMultiplier);

        if (damage > 0)
        {
            Health -= damage;
        }
    }




}












