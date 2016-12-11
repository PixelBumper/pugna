using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Team))]
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

    public int bulletCount = 3;
    public int maxBullets = 5;
    public int bulletsOnReload = 5;
    public int maxHp = 4;
    public int currentHp;

    private float secondsSinceLastUnitGained = 0f;
    public float lifeRegenerationPerSecond = 0.5f;
    public float lifeRegenerationCooldownInSeconds = 7f;
    private float remainingLifeRegenerationCooldownInSeconds = 0f;
    public CharacterLifeBar lifeBar;

    private bool isStunned = false;
    private Team _team;


    // Use this for initialization
	void Start ()
	{
	    ourRigidBody = GetComponent<Rigidbody2D>();
	    bulletPool = GameObject.FindGameObjectWithTag("BulletPool").GetComponent<GenericPool>();
	    _team = GetComponent<Team>();
	    SetCurrentHp(maxHp);
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


        // Movement!
        float move = Input.GetAxis("Horizontal" + input);

        MoveBy(move);
    }

    public void MoveBy(float move)
    {
        if (isStunned)
        {
            if (anim != null)
            {
                anim.SetFloat("Speed", 0);
            }
            return;
        }

        bool isTouchingWall = Physics2D.OverlapArea(wallCheckTopLeft.position, wallCheckBottomRight.position, whatIsGround);
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
        if (currentShootCooldownSeconds > 0f)
        {
            currentShootCooldownSeconds -= Time.deltaTime;
        }

        if (remainingLifeRegenerationCooldownInSeconds > 0f)
        {
            secondsSinceLastUnitGained = 0f;
            remainingLifeRegenerationCooldownInSeconds -= Time.deltaTime;
            if (remainingLifeRegenerationCooldownInSeconds <= 0f)
            {
                SetCurrentHp(currentHp + 1);
                if (anim)
                {
                    anim.SetBool("IsStunned", isStunned);
                }
            }
        }
        else if (currentHp < maxHp)
        {
            secondsSinceLastUnitGained += Time.deltaTime;
            int secondsPassed = (int) secondsSinceLastUnitGained;
            if (secondsPassed * lifeRegenerationPerSecond > 1)
            {
                int hpGained = (int) (secondsPassed * lifeRegenerationPerSecond);
                secondsSinceLastUnitGained -= hpGained;
                SetCurrentHp(currentHp + hpGained);
            }
        }

        ProcessMovement();
    }

    private void ProcessMovement()
    {
        if (Input.GetButton("Jump"+input))
        {
            Jump();
        }

        if (Math.Abs(Input.GetAxis("Fire1"+input)) > 0.001f || Input.GetButton("Fire1"+input))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (!isStunned && currentShootCooldownSeconds <= 0f)
        {
            if (bulletCount > 0)
            {
                bulletCount--;
                var pooledObject = bulletPool.GetPooledObject();
                if (pooledObject)
                {
                    currentShootCooldownSeconds = shootCooldownSeconds;
                    pooledObject.transform.position = transform.position;

                    pooledObject.GetComponent<Bullet>().SetShooter(gameObject);

                    var spriteRenderer = pooledObject.GetComponent<SpriteRenderer>();
                    if (spriteRenderer)
                    {
                        spriteRenderer.material.SetColor("_Color", _team.teamColor);
                    }

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
    }

    public void Jump()
    {
        if (!isStunned && isOnGround)
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

        if (lifeBar)
        {
            Vector3 lifeBarScale = lifeBar.gameObject.transform.localScale;
            lifeBarScale.x *= -1;
            lifeBar.gameObject.transform.localScale = lifeBarScale;
        }
    }

    void CollectItem(Rat.Items item)
    {
        switch (item)
        {
            case Rat.Items.Ammo:
                bulletCount = bulletsOnReload;
                break;
            case Rat.Items.Battery:
                throw new NotImplementedException("charakter cannot handle battery yet");
                break;
            default:
                throw new ArgumentOutOfRangeException("item", item, null);
        }
    }

    public void ReceiveDamage(int damage)
    {
        if (!isStunned)
        {
            SetCurrentHp(currentHp - damage);
            remainingLifeRegenerationCooldownInSeconds = lifeRegenerationCooldownInSeconds;
        }
    }

    private void SetCurrentHp(int hpToSet)
    {
        currentHp = hpToSet;
        if (currentHp <= 0)
        {
            currentHp = 0;
            isStunned = true;
            if (anim)
            {
                anim.SetBool("IsStunned", isStunned);
            }
        }

        if (currentHp > 0)
        {
            isStunned = false;
            if (anim)
            {
                anim.SetBool("IsStunned", isStunned);
            }
            currentHp = Math.Min(maxHp, currentHp);
        }

        if (lifeBar)
        {
            lifeBar.SetCurrentHp(currentHp / (float)maxHp);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.LogError("u suck");
    }
}
