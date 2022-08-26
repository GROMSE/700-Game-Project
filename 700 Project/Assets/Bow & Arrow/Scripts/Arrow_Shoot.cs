using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Class for controlling spawning and shooting arrows on player input. direction is controlled by where the bow is facing using the Bow_Rotation script.
 * Bow power min, max, speed of power build up and fire rate between arrows can be adjusted in the inspector with sliders.
 */

public class Arrow_Shoot : MonoBehaviour
{
    //Arrow prefab that is instantiated.
    public GameObject arrowPrefab;
    
    //Position where the arrow is spawned and the position it will move to when being drawn. 
    public Transform arrowSpawnTransform;
    public Transform arrowDrawnTransform;


    //minimum power that an arrow will be fired at. EX. tapping shoot without drawing the bow back. 
    [Range(1,50)]
    public float minArrowPower = 5;
    //maximum power that an arrow will be fired at. EX. Holding back the bow until its fully drawn. 
    [Range(50, 200)]
    public float maxArrowPower = 50;
    //How fast the arrow is drawn back and gains power in seconds. A higher rate will take longer to pull the bow back to full draw. 
    [Range(1, 10)]
    public float arrowPowerRate = 3;
    //How fast an arrow can be fired after the previous arrow in seconds. 
    [Range(0.1f, 5f)]
    public float fireRate = 1;
    //How long the mouse is held down before the arrow starts to gain power. 
    [Range(0.1f, 1f)]
    public float powerDelayTime;
    //The current arrow spawned and in the bow.
    private GameObject currentArrow;



    //Float value that is used to add force to the arrow. Visible in the inspector to aid in seeing how fast the bow gains power.
    [SerializeField]
    private float arrowPower;

    //Bool that allows an arrow to be spawned and fired.
    private bool canFire; 
    //Bool that allows an arrow to be spawned and fired.
    private bool powerDelayed;

    public PlayerManager player; 

    //On the first frame of the game. 
    private void Start()
    {
        //We can fire at the start of the game. 
        canFire = true;
    }

    private void Update()
    {
        //Checks to see if the mouse button was just pressed and we are allowed to spawn an arrow. If so, spawn an arrow. 
        if (Input.GetMouseButtonDown(0) && canFire)
            SpawnArrow();
        
        //While the mouse button is held down, current arrow gains power. If an arrow is not spawned yet (due to fire rate) but the mouse is being held down,
        //spawn an arrow first. 
        if (Input.GetMouseButton(0))
        {
            //Dont have an arrow and we can fire? Spawn an arrow. 
            if (currentArrow == null && canFire)
                SpawnArrow();

            //If an arrow is spawned, gain power. 
            if(currentArrow != null && powerDelayed)
                ArrowPower();
        }
      
        //When the mouse button is released, shoot the arrow. 
        if(Input.GetMouseButtonUp(0) && currentArrow != null)
            ShootArrow();
    }

    //Spawns an arrow and sets its physics and trail renderer to be disabled while in the bow. Current arrow is this arrow. 
    public void SpawnArrow()
    {
        GameObject arrowClone = Instantiate(arrowPrefab, arrowSpawnTransform.position, arrowSpawnTransform.rotation, this.transform);
        arrowClone.GetComponent<Rigidbody>().useGravity = false;
        arrowClone.GetComponent<Rigidbody>().isKinematic = true;
        arrowClone.transform.Find("Trail").gameObject.SetActive(false);
        arrowClone.GetComponent<Arrow>().SetArrowType(player.GetArrowType());
        StartCoroutine("PowerDelay");

        currentArrow = arrowClone;
    }

    //While mouse button is held, arrow gains power and is pulled back to the drawn location.
    public void ArrowPower()
    {
        //if the arrow is not close to maximum power, keep pulling it back. 
        if (arrowPower < maxArrowPower - 1)
        {
            //Arrow location.
            currentArrow.transform.position = Vector3.Lerp(currentArrow.transform.position, arrowDrawnTransform.position, Time.deltaTime * arrowPowerRate);
            //Power variable.
            arrowPower = Mathf.Lerp(arrowPower, maxArrowPower, Time.deltaTime * arrowPowerRate);
        }

        //If we are close to maximum power, set to maximums.
        else
        {
            arrowPower = maxArrowPower;
            currentArrow.transform.position = arrowDrawnTransform.position;
        }
    }


    //When the mouse button is let go, we shoot the arrow by setting its velocity on the rigidbody. The velocity is determined by how much power was gained. 
    public void ShootArrow()
    {
        //Make sure that the arrow is using physics and does not have a parent.
        currentArrow.GetComponent<Rigidbody>().useGravity = true;
        currentArrow.GetComponent<Rigidbody>().isKinematic = false;
        currentArrow.transform.parent = null;

        //The path of the arrow is the vector towards the aim location from its current position.
        Vector3 arrowPath = this.GetComponent<Bow_Rotation>().aimLocation.position - currentArrow.transform.position;

        //Adds velocity to the arrow in the direction of the arrow path with the arrow power as the speed.
        currentArrow.GetComponent<Rigidbody>().velocity = arrowPath.normalized * arrowPower;

        //Tells the arrow that it is fired to run code during its flight. 
        currentArrow.GetComponent<Arrow>().StartArrowFlight();


        //resets arrow power.
        arrowPower = minArrowPower;

        //This arrow is no longer in the bow. 
        currentArrow = null;

        //Start cooldown timer.
        StartCoroutine("Cooldown");
    }

    //Cannot shoot until a specificed time has past.
    public IEnumerator Cooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }

    public IEnumerator PowerDelay()
    {
        powerDelayed = false;
        yield return new WaitForSeconds(powerDelayTime);
        powerDelayed = true;
    }



}
