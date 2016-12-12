using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rat : MonoBehaviour
{
    private CapsuleCollider collider;

    public Items item = Items.Battery;

    public float speed = 3.0f;

    public RatState state;

    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rigidbody2D;

    private GenericPool ammoPool;

    private GenericPool batteryPool;

    private GameObject ratSpawner;

    private Animator _animator;

    public enum Items
    {
        Ammo,
        Battery
    }

    public enum RatState
    {
        MovingLeft,
        MovingRight,
        Falling,
        Dead
    }

    // Use this for initialization
    void Start()
    {
        collider = GetComponent<CapsuleCollider>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        batteryPool=GameObject.Find("BatteryPool").GetComponent<GenericPool>();
        ammoPool=GameObject.Find("AmmoPool").GetComponent<GenericPool>();

        ratSpawner=GameObject.Find("RatSpawner");
        state = RatState.Falling;

        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case RatState.MovingLeft:
                rigidbody2D.velocity=new Vector2(-speed, 0);
                break;
            case RatState.MovingRight:
                rigidbody2D.velocity=new Vector2(speed, 0);
                break;
            case RatState.Falling:

                break;
            case RatState.Dead:
                rigidbody2D.velocity=new Vector2(0,rigidbody2D.velocity.y);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var layerName = LayerMask.LayerToName(collision.gameObject.layer);

        if ("Ground".Equals(layerName) && state == RatState.Falling)
        {
            if (Random.value > 0.5)
            {
                state = RatState.MovingLeft;
                spriteRenderer.flipX = false;
            }
            else
            {
                state = RatState.MovingRight;
                spriteRenderer.flipX = true;
            }
        }
        if ("Border".Equals(layerName))
        {
            if (state == RatState.MovingLeft)
            {
                state = RatState.MovingRight;
                spriteRenderer.flipX = true;
            }
            else
            {
                state = RatState.MovingLeft;
                spriteRenderer.flipX = false;
            }
        }
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        CancelInvoke("Destroy");
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
        state=RatState.Falling;
        gameObject.layer = LayerMask.NameToLayer("Rat");
    }

    public void ReceiveDamage(int damage)
    {
        if (state != RatState.Dead)
        {
            GameObject go;
            if (item == Items.Ammo)
            {
                go = ammoPool.GetPooledObject();
            }
            else
            {
                go = batteryPool.GetPooledObject();
            }

            go.transform.position = transform.position;
            go.SetActive(true);
            go.SendMessage("Fling");

            _animator.SetTrigger("Died");
            state = RatState.Dead;

            ratSpawner.SendMessage("RatDied");
            gameObject.layer = LayerMask.NameToLayer("dead");


            Invoke("Destroy", 3);
        }

    }



}