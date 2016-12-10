using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{

    public CharacterConfiguration character;

	// Use this for initialization
	void Start ()
	{
	    var name = GetComponent<Text>();
	    name.text = character.name;
	    var avatar = GetComponent<Image>();
	    avatar.sprite = character.avatar;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
