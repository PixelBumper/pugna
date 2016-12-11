using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicalClock : MonoBehaviour
{
    public static GraphicalClock instance;

    public int points = 0;
    public int maxPoints = 3;
    public float rotationSpeed = 1.0f;

    private Transform childTransform;

    private Quaternion _quaternion;
    private BackgroundChanger _backgroundChanger;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        _backgroundChanger = GameObject.FindGameObjectWithTag("BackgroundChanger").GetComponent<BackgroundChanger>();
        childTransform = transform.GetChild(0);
        _quaternion = childTransform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Uncomment the line below to be able to update the points on live
        // AddPoints(0);

        childTransform.rotation = Quaternion.Lerp(childTransform.rotation, _quaternion, Time.deltaTime * rotationSpeed);
    }

    public void AddPoints(int pointsToAdd)
    {
        int previousBackgroundIndex = IndexForPoints(points);

        points += pointsToAdd;

        if (points > maxPoints)
        {
            // One team has won!
        }
        else if (points < -maxPoints)
        {
            // The other team has won!
        }
        points = Math.Min(maxPoints, Math.Max(-maxPoints, points));
        int currentBackgroundIndex = IndexForPoints(points);

        float rotationRequired = (maxPoints - points) / (float) maxPoints;
        _quaternion = Quaternion.Euler(0f, 0f, rotationRequired * 90f);


        // start changing the background here if necessary
        if (previousBackgroundIndex != currentBackgroundIndex)
        {
            _backgroundChanger.SetIndex(currentBackgroundIndex);
        }
    }

    private int IndexForPoints(int currentPoints)
    {
        var backgroundCount = _backgroundChanger.BackgroundCount();
        if (currentPoints == -maxPoints)
        {
            return 0;
        }
        else if (currentPoints == 0)
        {
            return backgroundCount / 2;
        }
        else if (currentPoints == maxPoints)
        {
            return backgroundCount - 1;
        }
        else if (currentPoints < 0)
        {
            return 1;
        }
        return 3;
    }
}