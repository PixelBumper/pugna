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
    public float shootCooldownSeconds = 1f;
    private float currentShootCooldownSeconds = 0f;

    private GenericPool bulletPool;

    


    // Use this for initialization
	void Start ()
	{
	    ourRigidBody = GetComponent<Rigidbody2D>();
	    bulletPool = GameObject.FindGameObjectWithTag("BulletPool").GetComponent<GenericPool>();
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

        if (currentShootCooldownSeconds > 0f)
        {
            currentShootCooldownSeconds -= Time.deltaTime;
        }

        if (currentShootCooldownSeconds <= 0f && (Math.Abs(Input.GetAxis("Fire1"+input)) > 0.001f || Input.GetButton("Fire1"+input)))
        {
            var pooledObject = bulletPool.GetPooledObject();
            if (pooledObject)
            {
                currentShootCooldownSeconds = shootCooldownSeconds;
                pooledObject.transform.position = transform.position;

                if (Input.GetAxis("Vertical" + input) > 0)
                {
                    pooledObject.transform.rotation = new Quaternion(0f, 0f, 0.707f, 0.707f);
                }
                else if (!isFacingRight)
                {
                    pooledObject.transform.rotation = new Quaternion(0f, 0f, 1f, 0f);
                }
                else
                {
                    pooledObject.transform.rotation = new Quaternion(0f, 0f, 0f, 1f);
                }
                pooledObject.SetActive(true);
            }
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
