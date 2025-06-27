using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu]
public class DialogueList : ScriptableObject
{
    [Serializable]
    public struct DialogueLine
    {
        public int index;
        public string text;
        [HideInInspector]
        public bool shown;
    }
    
    [SerializeField]
    public List<DialogueLine> dialogueLines;
    
    public DialogueLine GetLineByIndex(int index)
    {
        return dialogueLines.First(d => d.index == index);
    }

    public DialogueLine GetNextUnshownLine()
    {
        var line = dialogueLines.First(d => d.shown != true);
        line.shown = true;
        return line;
    }

    public DialogueLine GetRandom()
    {
        var random = new Random();
        var i = random.Next(0, dialogueLines.Count);
        return dialogueLines[i];
    }
}
