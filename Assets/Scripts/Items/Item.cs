using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent(typeof(Rigidbody2D))]
public class Item : MonoBehaviour
{

    public float flingVelocity = 10.2f;

    public Rat.Items item;

    private Rigidbody2D rigidbody;

    private float _invalidSeconds = 0f;
    public float _flingInvalidSeconds = 0.7f;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        Fling();
    }

    // Update is called once per frame
    void Update()
    {
        if (_invalidSeconds > 0)
        {
            _invalidSeconds -= Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_invalidSeconds <= 0f && "Player".Equals(LayerMask.LayerToName(collision.gameObject.layer)))
        {
            collision.gameObject.SendMessage("CollectItem", item);
            Destroy();
        }

    }

    public void Fling()
    {
        _invalidSeconds = _flingInvalidSeconds;
        if (Random.value > 0.5)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(flingVelocity, flingVelocity);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-flingVelocity, flingVelocity);
        }
    }


    public void Destroy()
    {
        gameObject.SetActive(false);
    }
//    void OnTriggerEnter(Collider other)
//    {
//        Debug.LogError("wtf");
//        other.gameObject.SendMessage("CollectItem", item);
//    }

}