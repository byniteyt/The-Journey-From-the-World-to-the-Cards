using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityExtensions;

[CreateAssetMenu(fileName = "Question", menuName = "ScriptableObject/Question")]
public class Question : ScriptableObject
{
    public struct Options
    {
        [TextArea(1, 4)] public string option;
        public Conversation continuation;
    }
    [TextArea(1, 4)] public string question;
    [ReorderableList] public Options[] options;
}
