using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu]
public class DialogueList : ScriptableObject
{
    [Serializable]
    public struct DialogueLine
    {
        public int index;
        public string text;
        public bool shown;
    }
    
    [SerializeField]
    public List<DialogueLine> dialogueLines;

    public static int amountShown;
    
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
}
