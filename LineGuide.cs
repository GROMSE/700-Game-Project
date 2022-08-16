using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGuide : MonoBehaviour
{
    //Declare variables
    private LineRenderer line;
    private GameObject player;
    private PlayerController playerController;
    private Rigidbody playerBody;
    public int pointsNum;

    // Start is called before the first frame update
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerBody = player.GetComponent<Rigidbody>();

        line.positionCount = pointsNum;
        for (int i = 0; i < pointsNum; i++)
        {
            float posX = i * playerController.jumpForce / 9.81f / pointsNum;
            float posY = playerController.jumpForce * posX - 9.81f * posX * posX;
            Vector3 posXYZ = new Vector3(posX, posY, 0.0f);
            line.SetPosition(i, posXYZ);
        } 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
