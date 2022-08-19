using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Declare Variables, starting with the privates
    private Rigidbody playerRb;
    public float horizontalInput;
    public float jumpInput;
    public bool isOnGround;
    public bool cooled = true;
    private double coolStart;

    //Declare Audio resources
    /* Commented out due to not being used currently
    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip walkSound;
    */

    public float gravityMod;
    public bool doubleJump;
    public int jumps = 0;
    public int jumpCount;
    public float jumpCool;
    public float jumpForce = 1.0f;
    //public int jumpType;
    public float speed = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        //Obtain player rigidbody for movement and declare grounded state
        playerRb = gameObject.GetComponent<Rigidbody>();
        isOnGround = true;
        Physics.gravity *= gravityMod;
    }

    // Update is called once per frame
    void Update()
    {
 
        
    }

    //Movement tied to fixed physics update frames 
    private void FixedUpdate()
    {
        //Read for input
        horizontalInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetAxis("Jump");

        //Move side to side
        //playerRb.MovePosition(transform.position + Vector3.right * horizontalInput * speed * Time.deltaTime);
        playerRb.velocity =new Vector3(horizontalInput * speed, playerRb.velocity.y, 0);

        //check for cooldown before jumps
        if (!cooled)
        {
            cooled = CooldownByTime(coolStart, jumpCool);
        }
        

        //Allow jump when on ground
        if (isOnGround && jumpInput > 0)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            cooled = false; /*initialize cooldown*/
            coolStart = Time.fixedTimeAsDouble;
            jumps += 1;
        }
        //Allow a number of double jumps as directed on a specific cooldown
        else if (doubleJump && !isOnGround && jumps < jumpCount && cooled && jumpInput > 0)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            cooled = false;
            coolStart = Time.fixedTimeAsDouble;
            jumps += 1;
        }


    }

    //When Colliding
    private void OnCollisionEnter(Collision other)
    {
        //refresh jumps on landing
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            jumps = 0;
        }
        
    }

    //Attempt at writing a method to calculate cooldown by time
    private bool CooldownByTime(double coolStart, float cooldown)
    {
        double currentTime = Time.timeAsDouble;
        if (currentTime - coolStart >= cooldown)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
