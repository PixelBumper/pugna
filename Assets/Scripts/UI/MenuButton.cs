using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    private Button button;

	// Use this for initialization
	void Start ()
	{
	    button = GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnSelect(BaseEventData eventData)
    {

    }

    public void OnDeselect(BaseEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
