﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (UnityEngine.Input.GetButtonUp("Fire1"))
	    {
	        Application.LoadLevel("CharacterSelection");
	    }
	}
}
