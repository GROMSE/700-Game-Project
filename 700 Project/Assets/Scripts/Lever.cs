using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{

    public GameObject door;
    public GameObject leverHandle;
    public Vector3 doorStart;
    public Vector3 doorEnd;
    public Quaternion leverStart;
    public Quaternion leverEnd;

    private bool moveDoor;
    private bool doorOpened; 
    private bool canPullLever;

    private void Start()
    {
        door.transform.localPosition = doorStart;

    }

    void Update()
    {
        if (canPullLever && Input.GetKeyDown(KeyCode.F))
            moveDoor = true; 

        if (moveDoor && !doorOpened)
        {
            moveDoor = false;
            door.transform.localPosition = doorEnd;
            leverHandle.transform.localRotation = leverEnd;
            doorOpened = true;

        }
 

        else if(moveDoor && doorOpened)
        {
            moveDoor = false;
            door.transform.localPosition = doorStart;
            leverHandle.transform.localRotation = leverStart;
            doorOpened = false; 
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            canPullLever = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            canPullLever = false;
    }
}
