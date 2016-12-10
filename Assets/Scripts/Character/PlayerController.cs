using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 10;
    private bool isFacingRight = true;

    private Rigidbody2D ourRigidBody;

    private bool isOnGround = false;
    private float groundRadius = 0.2f;

    public LayerMask whatIsGround;
    public Transform groundCheck;

    public Animator anim;

    public float jumpForce = 700f;


    // Use this for initialization
	void Start ()
	{
	    ourRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        // Check for ground
        isOnGround = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if (anim != null)
        {
            anim.SetBool("IsGrounded", isOnGround);
        }

        // Movement!
        float move = Input.GetAxis("Horizontal");
        if (anim != null)
        {
            anim.SetFloat("Speed", Mathf.Abs(move));
        }

        ourRigidBody.velocity = new Vector2(move * maxSpeed, ourRigidBody.velocity.y);

        if (move > 0 && !isFacingRight)
        {
            Flip();
        }
        else if(move < 0 && isFacingRight)
        {
            Flip();
        }
    }

    void Update()
    {
        if (isOnGround && Input.GetButton("Jump"))
        {
            if (anim != null)
            {
                anim.SetBool("IsGrounded", false);
            }
            ourRigidBody.AddForce(new Vector2(0, jumpForce));
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }

}
