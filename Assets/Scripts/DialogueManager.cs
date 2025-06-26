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
        public enum ShowType{Random,Next,Index}
        
        public ConditionType conditionType;
        public int amount;
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
            if (score > condition.amount)
            {
                print(dialogueList.GetNextUnshownLine().text);
            }
        }
    }
}
