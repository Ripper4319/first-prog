using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody theRB;
    Camera playercam;

    Vector2 camRotation;

    public Transform weaponslot;

    public bool sprintmode = false;

    public GameObject inv;
    private bool inventoryOpen;

    [Header("Player Stats")]
    public int maxHealth = 5;
    public int Health = 5;
    public int healthRestore = 1;

    [Header("weapon stats")]
    public GameObject shot;
    public int weaponid = -1;
    public int firemode = 0;
    public float shotspeed = 15f;
    public float firerate = 0;
    public float clipsize = 0;
    public float currentclip = 0;
    public float maxclip = 0;
    public float maxammo = 0;
    public float currentammo = 0;
    public float reloadamt = 0;
    public float bulletlifespan = 0; 
    public bool canfire = true;


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
        camRotation.x += Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.timeScale;
        camRotation.y += Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.timeScale;

        camRotation.y = Mathf.Clamp(camRotation.y, -camRotationLimit, camRotationLimit);

        playercam.transform.localRotation = Quaternion.AngleAxis(camRotation.y, Vector3.left);
        transform.localRotation = Quaternion.AngleAxis(camRotation.x, Vector3.up);

        if (Input.GetMouseButton(0) && canfire && currentclip > 0 && weaponid >= 0)
        {

            GameObject s = Instantiate(shot, weaponslot.position, weaponslot.rotation);
            s.GetComponent<Rigidbody>().AddForce(playercam.transform.forward * shotspeed);
            Destroy(s);
                
            canfire = false;
            currentclip --;
            StartCoroutine("cooldownfire");

        }
       
          

        if (Input.GetKeyDown(KeyCode.R))
            reloadclip();

        Vector3 temp = theRB.velocity;


        float VerticalMove = Input.GetAxisRaw("Vertical") * Time.timeScale;
        float HorizontalMove = Input.GetAxisRaw("Horizontal") * Time.timeScale;

        if (!SprintToggleOption)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                sprintmode = true;

            if (Input.GetKey(KeyCode.LeftShift))
                sprintmode = false;
        }

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

        temp.z = HorizontalMove * speed;
        temp.x = VerticalMove * speed;

        if (sprintmode)
            temp.x *= sprintMultiplier;

        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, -transform.up, groundDetectDistance))
            temp.y = jumpHeight;

        theRB.velocity = (temp.x * transform.forward) + (temp.z * transform.right) + (temp.y * transform.up);

        if (Input.GetKeyDown(KeyCode.I))
            Inventory();
    }


    private void Inventory()
    {
        if (inventoryOpen)
        {
            inv.SetActive(false);
            inventoryOpen = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
        else
        {
            inv.SetActive(true);
            inventoryOpen = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "weapon")
        {
            other.gameObject.transform.SetPositionAndRotation(weaponslot.position, weaponslot.rotation);

            other.gameObject.transform.SetParent(weaponslot);
        }

        switch (other.gameObject.name)
        {
            case "weapon1":
                bulletlifespan = 3;
                weaponid = 0;
                firemode = 0;
                firerate = 0.25f;
                clipsize = 20;
                currentclip = 20;
                maxclip = 20;
                maxammo = 160;
                currentammo = 40;
                reloadamt = 20;
                shotspeed = 10000;
                break;

            default: break;

            
        }



    }





    private void OnCollisionEnter(Collision collision)
    {
        if ((Health < maxHealth) && collision.gameObject.tag == "healthpickup")
        {
            Health += healthRestore;

            if (Health > maxHealth)
                Health = maxHealth;

            Destroy(collision.gameObject);

        }

        if ((currentammo < maxammo) && collision.gameObject.tag == "ammopickup")
        {
            currentammo += reloadamt;

            if (currentammo > maxammo)
                currentammo = maxammo;

            Destroy(collision.gameObject);
        }


        if ((Health < maxHealth) && collision.gameObject.tag == "healthpickup")
        {
            Health += healthRestore;

            if (Health > maxHealth)
                Health = maxHealth;

            Destroy(collision.gameObject);



        }
    }


    public void reloadclip()
    {
        currentclip = 0;
        if (currentclip >= clipsize)
            return;


        else
        {
            float reloadcount = clipsize - currentclip;

            if (currentammo < reloadcount)
            {
                currentclip += currentammo;

                currentammo = 0;
                return;

            }

            else
            {
                currentclip += reloadcount;

                currentammo -= reloadcount;

                return;

            }
        }



        




    }

    IEnumerator cooldownfire()
    {
        yield return new WaitForSeconds(firerate);
        canfire = true;
    }
   
    

    
}








