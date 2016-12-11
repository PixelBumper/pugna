using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent(typeof(Rigidbody2D))]
public class Item : MonoBehaviour
{

    public float flingVelocity = 10.2f;

    public Rat.Items item;

    private Rigidbody2D rigidbody;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        Fling();
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidbody.velocity.Equals(Vector2.zero))
        {
            rigidbody.bodyType=RigidbodyType2D.Kinematic;
        }
    }

    public void Fling()
    {
        if (Random.value > 0.5)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(flingVelocity, flingVelocity);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-flingVelocity, flingVelocity);
        }
    }

//    public void OnCollisionEnter(Collision collision)
//    {
//        Debug.LogError("wtf");
//        collision.gameObject.SendMessage("CollectItem", item);
//    }
//
//    void OnTriggerEnter(Collider other)
//    {
//        Debug.LogError("wtf");
//        other.gameObject.SendMessage("CollectItem", item);
//    }

}