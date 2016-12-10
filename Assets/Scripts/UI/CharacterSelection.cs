using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{

    public List<CharacterConfiguration> characters;

    private GameObject canvas;


	void Start ()
	{
	    canvas = GameObject.Find("Canvas");
	    foreach (var character in characters)
	    {
	        GameObject avatarGO = new GameObject(character.name);
	        UnityEngine.UI.Button button = avatarGO.AddComponent<UnityEngine.UI.Button>();
//	        button.
	    }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
