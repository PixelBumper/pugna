using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicalClock : MonoBehaviour
{
    public static GraphicalClock instance;

    public int points = 0;
    public int maxPoints = 3;
    public float rotationSpeed = 1.0f;

    private Transform childTransform;

    private Quaternion _quaternion;
    private BackgroundChanger _backgroundChanger;

    private GameObject _futureGuy;
    private GameObject _pastGuy;
    private GameObject _ratSpawner;

    public GameObject _victory_panel_past;
    public GameObject _victory_panel_future;

    public SpriteRenderer _time_panel;
    public Sprite[] _time_panel_sprites;

    public AudioClip timeChangeSound;

    private AudioSource _audioSource;

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
        _futureGuy = GameObject.Find("futuristic_pink");
        _pastGuy = GameObject.Find("green_gladiator");
        _ratSpawner = GameObject.Find("RatSpawner");
        _victory_panel_future.SetActive(false);
        _victory_panel_past.SetActive(false);
        _audioSource = GetComponent<AudioSource>();
        AddPoints(0);
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
            // Future team has won!
            _futureGuy.SendMessage("Won");
            _pastGuy.SendMessage("Lost");
            _ratSpawner.SendMessage("GameOver");
            _victory_panel_future.SetActive(true);
            Invoke("GoToMainMenu", 7);

        }
        else if (points < -maxPoints)
        {
            // Past team has won!
            _futureGuy.SendMessage("Lost");
            _pastGuy.SendMessage("Won");
            _ratSpawner.SendMessage("GameOver");
            _victory_panel_past.SetActive(true);
            Invoke("GoToMainMenu", 7);
        }
        _audioSource.PlayOneShot(timeChangeSound);
        points = Math.Min(maxPoints, Math.Max(-maxPoints, points));
        int currentBackgroundIndex = IndexForPoints(points);

        float rotationRequired = (maxPoints - points) / (float) maxPoints;
        _quaternion = Quaternion.Euler(0f, 0f, rotationRequired * 90f);


        // start changing the background here if necessary
        if (previousBackgroundIndex != currentBackgroundIndex)
        {
            _backgroundChanger.SetIndex(currentBackgroundIndex);
        }
        SetChronoText(currentBackgroundIndex);
    }

    private void SetChronoText(int currentBackgroundIndex)
    {
        _time_panel.sprite = _time_panel_sprites[currentBackgroundIndex];
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

    void GoToMainMenu()
    {
        MainController.SwitchScene("StartMenu");
    }
}