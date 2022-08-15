using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Controls the arrow during its flight and what happens when it collides with an object.
 */
public class Arrow : MonoBehaviour
{
  
    private bool arrowFired;
    private Rigidbody rb;

    public float arrowGravity = 1;

    bool collided;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //If the arrow is fired from the bow, the arrow rotates to its current movement direction. Gives arrow an arching effect. 
        if (arrowFired)
            transform.rotation = Quaternion.LookRotation(rb.velocity);         
    }

    private void FixedUpdate()
    {
        if(arrowFired)
            rb.AddForce(Physics.gravity * rb.mass * arrowGravity);
    }

    //Sets arrow to true when called. Used by Arrow_Shoot script.
    public void SetArrowFired()
    {
        arrowFired = true;
    }

    //Timer that destroys the arrow after a specified time.
    public IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }

    //Called by the Arrow_Shoot class. Sets the arrow fired bool, starts death time, and turns on the trail renderer. 
    public void StartArrowFlight()
    {
        SetArrowFired();
        StartCoroutine("DeathTime");
        transform.Find("Trail").gameObject.SetActive(true);
    }

    //when the arrow collides with something.
    //*** Work in progress for arrow behaviors like bouncing off hard objects, skipping on the ground at shallow angles, etc. ***
    private void OnCollisionEnter(Collision collision)
    {
        //Turns physics off from arrow, making it stop moving in its current location. Makes it look like it sticks into the object.
        rb.useGravity = false;
        rb.isKinematic = true;

        arrowFired = false;
        collided = true;

        // *** WIP code below for arrow behaviors ***

        //rb.velocity = rb.velocity / 2;
        // rb.AddTorque(new Vector3(1000000, 10000, 100000), ForceMode.VelocityChange);
        //this.transform.parent = collision.transform;

        //if we havent collided with anything yet. 
        if (!collided)
        {

            float surfaceAngle = 0;
            RaycastHit hit;
            Ray ray = new Ray(this.transform.position, transform.forward);

            Debug.DrawLine(this.transform.position, transform.forward * 100, Color.red, 5f);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                surfaceAngle = Vector3.Angle(hit.normal, Vector3.forward);
                Debug.Log(surfaceAngle);
            }
        }
    }
}
