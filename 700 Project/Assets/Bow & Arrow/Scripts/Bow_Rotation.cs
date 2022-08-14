using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Rotates the bow towards the players mouse position in the game world. 
 */
public class Bow_Rotation : MonoBehaviour
{

    //Object that will be the look at target for the bow to rotate towards. Has a sphere object as a child that can be disabled so that it is not visible to the player.
    public Transform aimLocation;


    void Update()
    {
        MouseToWorldPoint();

        //Bow rotates to look at the aim location every frame. 
        this.transform.LookAt(aimLocation);
    }

    //Function casts a ray from the mouse position into the world. Where it hits in the world, it sets the aim location to that point. 
    public void MouseToWorldPoint()
    {
        //Raycast from mouse point to world point.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        //if the raycast collides with something in the game world, it sets the aim location object to the collision point. 
        if (Physics.Raycast(ray, out RaycastHit raycastHit,1000))
        {
            aimLocation.transform.position = raycastHit.point;
        }
    }
}
    