using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPool : MonoBehaviour
{

    public GameObject pooledObject;
    public Transform parentForObjects;
    public int pooledAmount = 10;
    public bool willGrow = true;

    private List<GameObject> pool;

	// Use this for initialization
	void Start ()
	{
	    pool = new List<GameObject>(pooledAmount);
	    for (int i = 0; i < pooledAmount; i++)
	    {
	        GameObject obj = Instantiate(pooledObject, parentForObjects);
	        obj.SetActive(false);
	        pool.Add(obj);
	    }
	}

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }

        if (willGrow)
        {
            GameObject obj = Instantiate(pooledObject, parentForObjects);
            pool.Add(obj);
            return obj;
        }

        return null;
    }

}
