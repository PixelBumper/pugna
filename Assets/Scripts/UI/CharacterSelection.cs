using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{

    public List<CharacterConfiguration> characters;

    private GameObject canvas;

    private PlayerState player1;
    private PlayerState player2;
    private PlayerState player3;
    private PlayerState player4;

    void Start ()
	{
	    player1= new PlayerState();
	    player2= new PlayerState();
	    player3= new PlayerState();
	    player4= new PlayerState();

//	    GameObject.Find("Player1Button").GetComponent<Button>().onClick.AddListener(()=>ShowTutorial());
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}

