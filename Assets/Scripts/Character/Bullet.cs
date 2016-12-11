using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Vector2 speedDirection = Vector2.right;
    private const float SECONDS_LIFETIME = 2f;
	
	// Update is called once per frame
	void Update ()
	{
	    transform.Translate(speedDirection.x * Time.deltaTime,
	        speedDirection.y * Time.deltaTime,
	        0);
	}

    private void OnEnable()
    {
        Invoke("Destroy", SECONDS_LIFETIME);
    }

    private void OnDisable()
    {
        CancelInvoke("Destroy");
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }
}
