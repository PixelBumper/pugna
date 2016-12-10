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

    public LayerMask whatIsGround;
    public Transform groundCheckTopLeft;
    public Transform groundCheckBottomRight;
    public Transform wallCheckTopLeft;
    public Transform wallCheckBottomRight;

    public Animator anim;

    public float jumpForce = 700f;

    public string input = "_INPUT1";


    // Use this for initialization
	void Start ()
	{
	    ourRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        // Check for ground
        isOnGround = Physics2D.OverlapArea(groundCheckTopLeft.position, groundCheckBottomRight.position, whatIsGround);
        if (anim != null)
        {
            anim.SetBool("IsGrounded", isOnGround);
        }

        bool isTouchingWall = Physics2D.OverlapArea(wallCheckTopLeft.position, wallCheckBottomRight.position, whatIsGround);

        // Movement!
        float move = Input.GetAxis("Horizontal" + input);
        if (anim != null)
        {
            anim.SetFloat("Speed", Mathf.Abs(move));
        }

        ourRigidBody.velocity = isTouchingWall ? new Vector2(0, ourRigidBody.velocity.y) : new Vector2(move * maxSpeed, ourRigidBody.velocity.y);

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
        if (isOnGround && Input.GetButton("Jump"+input))
        {
            isOnGround = false;
            if (anim != null)
            {
                anim.SetBool("IsGrounded", isOnGround);
            }
            ourRigidBody.velocity = new Vector2(ourRigidBody.velocity.x, jumpForce);
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
