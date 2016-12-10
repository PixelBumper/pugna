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
	    EventSystem.current.SetSelectedGameObject(GameObject.Find("1 vs 1 button"));
	    GameObject.Find("tutorial").GetComponent<Button>().onClick.AddListener(()=>ShowTutorial());
	    GameObject.Find("1 vs 1 button").GetComponent<Button>().onClick.AddListener(()=>LoadCharacterSelection());
	    GameObject.Find("2 vs 2 button").GetComponent<Button>().onClick.AddListener(()=>ShowTutorial());
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

    void LoadCharacterSelection()
    {
        MainController.SwitchScene("CharacterSelection");
    }

    void ShowTutorial()
    {
        throw new NotImplementedException();
    }
}
