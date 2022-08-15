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
    public float cooled;
    private AudioSource playerAudio;

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
    }

    // Update is called once per frame
    void Update()
    {
        //Read for input
        horizontalInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetAxis("Jump");

        //Move side to side
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);

        //Allow jump when on ground
        if (isOnGround && jumpInput > 0)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            cooled = 0.0f; /*initialize cooldown*/
            jumps += 1;
        }
        //Allow a number of double jumps as directed on a specific cooldown
        else if (doubleJump && !isOnGround && jumps < jumpCount && cooled > jumpCool && jumpInput > 0)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            cooled = 0.0f;
            jumps += 1;
        }

        //rudimentary cooldown for jumping to prevent using all jumps on one press
        if (cooled < jumpCool)
        {
            cooled += Time.deltaTime;
        }
        //investigating alternatives 
        
    }

    //When Colliding
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            jumps = 0;
        }
        
    }
}
