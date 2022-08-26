using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;
    private bool isFloating;

    public Animator anim; 
    public int health;

    public Material gray;
    public Material white;
    public Material red;

    public GameObject player; 

    private int hitCount; 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
        rb.isKinematic = true;
        isFloating = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
            GetComponent<Renderer>().material = gray;
        }

    }

    public void TakeDamage()
    {
        health -= 1;

        if(health > 0)
            StartCoroutine(HitFX());
    }

    public void Float()
    {

        rb.useGravity = false;
        rb.isKinematic = false;

        if(!isFloating)
            rb.AddForce(Vector3.up * 5000);

        StartCoroutine(FloatTimer());
            

        isFloating = true;
    }

    public bool GetFloating()
    {
        return isFloating;
    }

    private IEnumerator FloatTimer()
    {
        hitCount += 1; 
        yield return new WaitForSeconds(10);
        hitCount -= 1;

        if (hitCount <= 0)
        {

            rb.AddForce(Vector3.down * 500);

            yield return new WaitForSeconds(5);
            if (hitCount <= 0)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
                isFloating = false;
            }
        }

    }

    public IEnumerator HitFX()
    {
        GetComponent<Renderer>().material = white;
        yield return new WaitForSeconds(0.1f);

        GetComponent<Renderer>().material = red;
    }

    
}
