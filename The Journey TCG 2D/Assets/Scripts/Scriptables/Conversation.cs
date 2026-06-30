using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityExtensions;

[CreateAssetMenu(fileName = "Conversation", menuName = "ScriptableObject/Conversation")]
public class Conversation : ScriptableObject
{
    [System.Serializable]
    public struct Line
    {
        public Character character;
        public AudioClip clip;
        [TextArea(2, 4)] public string dialogue;
    }
    public bool startAgain;
    [ReorderableList] public Line[] lines;
    public Question quest;
}
