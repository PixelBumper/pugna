﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{

    public GameObject tutorial;

    public GameObject canvas;

    public bool showingTutorial;

    private bool initialPress = false;

	// Use this for initialization
	void Start ()
	{
	    canvas = GameObject.Find("Canvas");
	    tutorial = GameObject.Find("TutorialScreen");
	    showingTutorial = false;
	    tutorial.SetActive(false);
	    EventSystem.current.SetSelectedGameObject(GameObject.Find("play"));
	    GameObject.Find("play").GetComponent<Button>().onClick.AddListener(()=>StartGame());
	    GameObject.Find("tutorial").GetComponent<Button>().onClick.AddListener(()=>ShowTutorial());
	    GameObject.Find("exit").GetComponent<Button>().onClick.AddListener(()=>Exit());
	}

    private void Exit()
    {
        Application.Quit();
    }

    // Update is called once per frame
	void Update () {
	    if (showingTutorial && Input.GetButtonUp("Submit"))
	    {
	        if (initialPress)
	        {
	            HideTutorial();
	        }
	        else
	        {
	            initialPress = true;
	        }
	    }

	}

    void StartGame()
    {
        MainController.SwitchScene("DefaultStage");
    }

    void ShowTutorial()
    {
        Debug.LogError("Show");
        showingTutorial = true;
        canvas.SetActive(false);
        tutorial.SetActive(true);
        initialPress = false;

    }

    void HideTutorial()
    {
        Debug.LogError("Hide");
        tutorial.SetActive(false);
        canvas.SetActive(true);
        showingTutorial = false;
        EventSystem.current.SetSelectedGameObject(GameObject.Find("play"));
    }
}
