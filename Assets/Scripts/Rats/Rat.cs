using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rat : MonoBehaviour
{

    private CapsuleCollider collider;

    public double dropchanceBattery=0.5;

    private Items item;

    public enum Items
    {
        Ammo,
        Battery
    }

	// Use this for initialization
	void Start ()
	{
	    if (Random.value > dropchanceBattery)
	    {
	        item = Items.Ammo;
	    }
	    else
	    {
	        item = Items.Battery;
	    }
	    collider = GetComponent<CapsuleCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTakeDamage()
    {
        GameObject go;
        if (item == Items.Ammo)
        {
            go=AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/ammo");
        }
        else
        {
            go=AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/battery");
        }

        go.transform.position = transform.position;
        go.SendMessage("Fling");

        Destroy(gameObject);

    }


}
