using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMaker : MonoBehaviour
{
    //Declare Variables
    public GameObject linePrefab;
    private PlayerController playerControllerScript;
    private Vector3 origin = new Vector3(0, 1, 0);
    private GameObject[] lines;
    private float vMod = 0;
    private Vector3 coolSpot;
    public int pointsNum = 30;


    // Start is called before the first frame update
    void Start()
    {
        //Grab the player controller for physics info
        playerControllerScript = GetComponentInParent<PlayerController>();
        GameObject[] lines = new GameObject[playerControllerScript.jumpCount];

    }

    // Update is called once per frame
    void Update()
    {
        //Make the guidelines appear when G is pressed
        if (Input.GetKeyDown(KeyCode.G))
        {
            //Create a line for each jump
            for (int j = 0; j < playerControllerScript.jumpCount; j++)
            {
                //In each iteration creates a line and pulls its renderer so meddling can ensue
                GameObject lineObject = Instantiate(linePrefab, origin, linePrefab.transform.rotation);
                LineRenderer line = lineObject.GetComponent<LineRenderer>();

                //Reshape line to match jump trajectory using physics equations by replacing line's points with a prescribed number of new points
                line.positionCount = pointsNum;
                for (int i = 0; i < pointsNum; i++)
                {
                    //Make points for time t
                    //This is mostly important because x and y movement are completely seperate in this case
                    float posT = i * 2 * (playerControllerScript.jumpForce + vMod) / 9.81f / playerControllerScript.gravityMod / pointsNum;

                    //Create Vector3 points using method and set into line
                    Vector3 posXYZ = CreatePointByTime(posT);
                    line.SetPosition(i, posXYZ);
                }

                //Record point along arc at which jump comes off cooldown and projected y velocity at that point
                //This is used to position the subsequent line and adjust its arc to match where the jump could theoretically start
                coolSpot = CreatePointByTime(playerControllerScript.jumpCool);
                vMod = playerControllerScript.jumpForce - 9.81f * playerControllerScript.gravityMod * playerControllerScript.jumpCool;

                origin += coolSpot;
            }
            //Reset iterative properties so script can be used more than once in play mode
            vMod = 0;
            origin = Vector3.up;
        }
        //Delete all guidelines on H press
        //This also means multiple different guideline sets can coexist
        else if (Input.GetKeyDown(KeyCode.H))
        {
            foreach (GameObject l in GameObject.FindGameObjectsWithTag("GuideLine"))
            {
                GameObject.Destroy(l);
            }

            vMod = 0;
            origin = Vector3.up;
        }
    }

    //A method to create Vector3 points using point in time in seconds
    private Vector3 CreatePointByTime(float t)
    {
        //Create x position based on transform speed
        float posX = t * playerControllerScript.speed;

        //Create y position according to jump force and gravity
        float posY = (playerControllerScript.jumpForce + vMod) * t - 9.81f * playerControllerScript.gravityMod * Mathf.Pow(t, 2) / 2;

        //Set new point into the line
        Vector3 posXYZ = new Vector3(posX, posY, 0.0f);
        return posXYZ;
    }
}
