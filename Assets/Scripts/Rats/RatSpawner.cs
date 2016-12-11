using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class RatSpawner : MonoBehaviour
{

    public float chanceForBatteryRat = 0.3f;

    private int ratCount=0;

    public int maximumRats = 5;

    public float minTimeBetweenSpawns = 1.0f;

    public float maxTimeBetweenSpawns = 2.0f;

    public List<GameObject> spawningPoints;

    private GenericPool batteryRatPool;

    private GenericPool ammoRatPool;

    private float timePassed;

    private int timeUntilNextSpawn;

	// Use this for initialization
	void Start () {
		batteryRatPool=GameObject.Find("BatteryRatPool").GetComponent<GenericPool>();
	    ammoRatPool=GameObject.Find("AmmoRatPool").GetComponent<GenericPool>();
	    ScheduleNextSpawn();
	}
	
	// Update is called once per frame
	void Update ()
	{

	    timePassed += Time.deltaTime;

	    if (timePassed >= timeUntilNextSpawn)
	    {

	        if (ratCount < maximumRats)
	        {
	            GameObject rat;
	            if (Random.value <= chanceForBatteryRat)
	            {
	                rat = batteryRatPool.GetPooledObject();
	            }
	            else
	            {
	                rat = ammoRatPool.GetPooledObject();
	            }

	            rat.transform.position = spawningPoints[Random.Range(0, spawningPoints.Count - 1)].transform.position;
	            rat.SetActive(true);

	            ratCount++;
	        }

	        ScheduleNextSpawn();
	    }
	}

    void ScheduleNextSpawn()
    {
        timePassed = 0;
        timeUntilNextSpawn=Random.Range((int)minTimeBetweenSpawns, (int)maxTimeBetweenSpawns);

    }

    void RatDied()
    {
        ratCount--;
    }

    public void GameOver()
    {
        ratCount = Int32.MaxValue;
        foreach (var rat in GameObject.FindGameObjectsWithTag("rat"))
        {
            rat.SendMessage("ReceiveDamage", 1);
        }

    }


}