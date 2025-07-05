using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

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
    private UIDocument doc;
    [SerializeField] public List<DialogueCondition> dialogueConditions;
    [SerializeField] public DialogueList dialogueList;
    [SerializeField] private float dialogueShowTime = 20f;
    [SerializeField] private bool debug;
    private float dialogueShownDuration = Mathf.Infinity;
    private void Start()
    {
        doc = FindFirstObjectByType<UIDocument>();
        FindFirstObjectByType<ScoreKeeper>().ScoreUpdated += (OnScoreUpdated);
    }

    private void Update()
    {
        dialogueShownDuration += Time.deltaTime;
        var dialogueText = doc.rootVisualElement.Q<Label>("dialogueText");
        if (dialogueText == null)
        {
            return;
        }
        if (dialogueShownDuration > dialogueShowTime && !dialogueText.GetClasses().Contains("invisible"))
        {
            dialogueText.AddToClassList("invisible");
        }

        if (!debug) return;
        if (Input.GetKeyDown(KeyCode.P))
        {
            ShowDialogue(dialogueList.GetRandom());
        }
    }

    private void OnScoreUpdated(int score)
    {
        foreach (var condition in dialogueConditions.Where(d => d.conditionType == DialogueCondition.ConditionType.Score))
        {
            if (score >= condition.amount)
            {
                ShowDialogue(condition);
            }
        }
    }

    private void ShowDialogue(DialogueCondition condition)
    {
        var dialogueText = doc.rootVisualElement.Q<Label>("dialogueText");
        if (dialogueText != null)
        {
            dialogueText.text = HandleShowCondition(condition);
            dialogueText.RemoveFromClassList("invisible");
            dialogueShownDuration = 0;
        }
    }

    public void ShowDialogue(DialogueList.DialogueLine line){
        var dialogueText = doc.rootVisualElement.Q<Label>("dialogueText");
        dialogueText.text = line.text;
        dialogueText.RemoveFromClassList("invisible");
        dialogueShownDuration = 0;
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
