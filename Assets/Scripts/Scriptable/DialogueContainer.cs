using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Dialogue", menuName = "create Dialogue")]
public class DialogueContainer : ScriptableObject
{
    [System.Serializable]
    public struct Choice
    {
        
        public string text;
        public int linkTo;
    }

    [System.Serializable]
    public struct Dialogue
    {
        [Multiline]
        public string text;
        public Choice[] choices;
        public int linkTo;
    }

    public string speakerName;
    public int startDialogue;
    public Dialogue[] Dialogues;
   
}
