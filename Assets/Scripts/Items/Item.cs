using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public float flingVelocity=10.2f;

	// Use this for initialization
	void Start () {
		Fling();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Fling()
    {
        if (Random.value > 0.5)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(flingVelocity,flingVelocity);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-flingVelocity,flingVelocity);
        }
    }
}
