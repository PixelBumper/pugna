using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicalClock : MonoBehaviour
{

    public static GraphicalClock instance;

    public int points = 0;
    public int maxPoints = 4;
    public float rotationSpeed = 1.0f;

    private Transform childTransform;

    private Quaternion _quaternion;

    private void Awake()
    {
        instance = this;
        _quaternion = transform.rotation;
    }

    // Use this for initialization
	void Start ()
	{
	    childTransform = transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    // Uncomment the line below to be able to update the points on live
	    // AddPoints(0);

	    childTransform.rotation = Quaternion.Lerp(childTransform.rotation, _quaternion, Time.deltaTime * rotationSpeed);
	}

    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;

        if (points > maxPoints)
        {
            // One team has won!

        } else if (points < -maxPoints)
        {
            // The other team has won!

        }
        points = Math.Min(maxPoints, Math.Max(-maxPoints, points));

        float rotationRequired = (maxPoints - points) / (float) maxPoints;
        _quaternion = Quaternion.Euler(0f, 0f, rotationRequired * 90f);


        // TODO (slumley): start changing the background here if necessary
    }

}
