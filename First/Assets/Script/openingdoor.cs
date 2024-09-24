using System.Collections;
using UnityEngine;

public class ElevatorDoor : MonoBehaviour
{
    public float doorOpenTime = 3f;  
    public Vector3 openPosition;     
    public Vector3 closedPosition; 
    public float doorSpeed = 2f;    

    public void Start()
    {
        
        transform.localPosition = closedPosition;
    }

    public IEnumerator OpenDoorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        
        StartCoroutine(MoveDoor(openPosition));

        
        yield return new WaitForSeconds(doorOpenTime);

        
        StartCoroutine(MoveDoor(closedPosition));
    }

    private IEnumerator MoveDoor(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.localPosition, targetPosition) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, doorSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
