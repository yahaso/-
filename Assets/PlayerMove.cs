using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    private Vector2 move;
    private Rigidbody2D rb2d;

    public float flap = 1000f;
    bool jump = false;
    // Start
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    // Update
    void Update()
    {
        move.x = Input.GetAxis("Horizontal") * maxSpeed;
        move.y = rb2d.velocity.y;
        rb2d.velocity = move;

        if (Input.GetKeyDown("space") && !jump)
        {
            rb2d.AddForce(Vector2.up * flap);
            jump = true;
        }


    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jump = false;
        }
    }

   

   
}
