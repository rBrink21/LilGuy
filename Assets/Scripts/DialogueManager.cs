using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Serializable]
    public struct DialogueCondition
    {
        [Serializable]
        public enum ConditionType{Score}
        [Serializable]
        public enum ShowType{NextUnshown,Index,Random}
        
        public ConditionType conditionType;
        public int amount;
        [Tooltip("Random: picks a random one from the provided dialogue list\nNextUnshown: picks the next one the player has not seen in the provided dialogue list\nIndex: picks the one with matching index from the list")]
        public ShowType showType;
        public int index;
    }

    [SerializeField] public List<DialogueCondition> dialogueConditions;
    [SerializeField] public DialogueList dialogueList;

    private void Start()
    {
        FindFirstObjectByType<ScoreKeeper>().ScoreUpdated += (OnScoreUpdated);
    }


    private void OnScoreUpdated(int score)
    {
        foreach (var condition in dialogueConditions.Where(d => d.conditionType == DialogueCondition.ConditionType.Score))
        {
            if (score >= condition.amount)
            {
                print(HandleShowCondition(condition));
            }
        }
    }

    private string HandleShowCondition(DialogueCondition condition)
    {
        return condition.showType switch
        {
            DialogueCondition.ShowType.NextUnshown => dialogueList.GetNextUnshownLine().text,
            DialogueCondition.ShowType.Index => dialogueList.GetLineByIndex(condition.index).text,
            DialogueCondition.ShowType.Random => dialogueList.GetRandom().text,
            _ => null
        };
    }
}
