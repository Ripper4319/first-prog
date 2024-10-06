using System.Collections;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public float[] floorHeights; 
    public float speed = 3f;
    private int targetFloor = 0;
    private bool moving = false;
    [SerializeField]
    public KeyCode activationKey = KeyCode.E; 
    public ElevatorDoor elevatorDoor;

    public Transform player;
    public float activationRange = 1f;


    void Update()
    {
        if (!moving && Vector3.Distance(player.position, transform.position) <= activationRange)
        {
            if (Input.GetKeyDown(activationKey))
            {
                GoToFloor(targetFloor);
                moving = true;
            }
        }
    }

    public void GoToFloor(int floorIndex)
    {
        if (floorIndex >= 0 && floorIndex < floorHeights.Length && !moving)
        {
            targetFloor = floorIndex;
            StartCoroutine(MoveToPosition(floorHeights[floorIndex]));
            //
        }
    }

    IEnumerator MoveToPosition(float targetY)
    {
        moving = true;

       
        while (Mathf.Abs(transform.position.y - targetY) > 0.01f)
        {
            Vector3 newPosition = new Vector3(transform.position.x, Mathf.MoveTowards(transform.position.y, targetY, speed * Time.deltaTime), transform.position.z);
            transform.position = newPosition;
            yield return null;
        }

        moving = false;

        StartCoroutine(elevatorDoor.OpenDoorAfterDelay(2f)); 
    }
}


