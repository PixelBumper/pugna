using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "CharacterConfiguration")]
public class CharacterConfiguration : ScriptableObject
{
    public string name;
    public Sprite avatar;
    public GameObject asset;
}
