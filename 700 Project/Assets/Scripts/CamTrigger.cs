using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    private int index;
    private CameraController camController; 

    public void SetIndex(int newIndex)
    {
        index = newIndex; 
    }

    public void SetCamController(CameraController controller)
    {
        camController = controller;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            camController.SetCurrentCam(index);
    }
}
