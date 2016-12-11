using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
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
	    if (UnityEngine.Input.GetButtonUp("Fire1"))
	    {

	    }
	}

    void StartGame()
    {
        MainController.SwitchScene("DefaultScene");
    }

    void ShowTutorial()
    {
        throw new NotImplementedException();
    }
}
