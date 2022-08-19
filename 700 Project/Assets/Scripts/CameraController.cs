using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool staticCamerasEnabled;

    public GameObject[] staticCams; 
    public GameObject[] camTriggers;

    public GameObject followCam; 
    // Start is called before the first frame update
    void Start()
    {
        if (staticCamerasEnabled)
        {
            followCam.gameObject.SetActive(false);

            for (int i = 0; i < camTriggers.Length; i++)
            {
                camTriggers[i].GetComponent<CamTrigger>().SetIndex(i);
                camTriggers[i].GetComponent<CamTrigger>().SetCamController(this);

                if (i == 0)
                    staticCams[i].gameObject.SetActive(true);
                else
                    staticCams[i].gameObject.SetActive(false);
            }
        }
        else
        {
            followCam.gameObject.SetActive(true);
            foreach (GameObject cam in staticCams)
                cam.SetActive(false);
            foreach (GameObject trigger in camTriggers)
                trigger.SetActive(false);
        }

    }

    public void SetCurrentCam(int camNum)
    {
        foreach(GameObject camera in staticCams)
        {
            if (staticCams[camNum] == camera)
                camera.gameObject.SetActive(true);
            else
                camera.gameObject.SetActive(false);
        }
    }
}
