using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class for testing bow & arrow shooting with movement. Player bounces between two points. Can Modify speed in inspector. No movement when set to 0. 
 */
public class Player_Move_Test : MonoBehaviour
{
    public Transform moveTargetLeft;
    public Transform moveTargetRight;

    [Range(0f,2f)]
    public float speed = 1;

    private Transform currentTarget; 

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = moveTargetLeft;
    }

    // Update is called once per frame
    void Update()
    {
        //Moves player to the target point from current location. 
        transform.position = Vector3.Lerp(transform.position, currentTarget.position, Time.deltaTime * speed);

        //if close to left target, change target to right target. 
        if (Vector3.Distance(transform.position,moveTargetLeft.position) < 1)
            currentTarget = moveTargetRight;

        //if close to right target, change target to left target. 
        else if (Vector3.Distance(transform.position, moveTargetRight.position) < 1)
            currentTarget = moveTargetLeft;
    }
}
