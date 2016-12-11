using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rat : MonoBehaviour
{
    private CapsuleCollider collider;

    public Items item = Items.Battery;

    public float speed = 3.0f;

    public RatState state;

    public enum Items
    {
        Ammo,
        Battery
    }

    public enum RatState
    {
        MovingLeft,
        MovingRight,
        Falling
    }

    // Use this for initialization
    void Start()
    {
        collider = GetComponent<CapsuleCollider>();
        state = RatState.Falling;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case RatState.MovingLeft:
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
                break;
            case RatState.MovingRight:
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
                break;
            case RatState.Falling:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void OnTakeDamage()
    {
        GameObject go;
        if (item == Items.Ammo)
        {
            go = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/ammo");
        }
        else
        {
            go = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/battery");
        }

        go.transform.position = transform.position;
        go.SendMessage("Fling");

        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var layerName = LayerMask.LayerToName(collision.gameObject.layer);

        if ("Ground".Equals(layerName) && state == RatState.Falling)
        {
            if (Random.value > 0.5)
            {
                state = RatState.MovingLeft;
            }
            else
            {
                state = RatState.MovingRight;
            }
        }
        if ("Border".Equals(layerName))
        {
            if (state == RatState.MovingLeft)
            {
                state = RatState.MovingRight;
            }
            else
            {
                state = RatState.MovingLeft;

            }
        }
    }

}