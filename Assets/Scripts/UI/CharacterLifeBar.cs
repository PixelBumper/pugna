using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLifeBar : MonoBehaviour {

    private Vector3 nextScale;
    public float speed = 1f;
    public Transform whatToModify;

    // Update is called once per frame
	void Update ()
	{
	    whatToModify.localScale = Vector3.Lerp(whatToModify.localScale, nextScale, speed * Time.deltaTime);
	}

    public void SetCurrentHp(float percentage)
    {
        nextScale = new Vector3(percentage, 1f, 1f);
    }
}
