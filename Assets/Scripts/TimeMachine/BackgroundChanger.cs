using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{

    public GameObject[] backgrounds;

    public int BackgroundCount()
    {
        return backgrounds.Length;
    }

    public void SetIndex(int newIndex)
    {
        foreach (var background in backgrounds)
        {
            background.SetActive(false);
        }
        backgrounds[newIndex].SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
