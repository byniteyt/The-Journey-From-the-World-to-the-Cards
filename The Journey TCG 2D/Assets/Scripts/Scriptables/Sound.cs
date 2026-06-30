using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Sound",menuName = "ScriptableObject/Sound")]
public class Sound : ScriptableObject
{
    public string audioName;
    public AudioClip clip;
}
