using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObject/Character")]
public class Character : ScriptableObject
{
    public string nombre;
    public Sprite image;
}
