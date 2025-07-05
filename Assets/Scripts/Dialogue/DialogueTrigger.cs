using System;
using UnityEngine;

namespace Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private DialogueList list;
        [SerializeField] private int index;
        private void OnTriggerEnter2D(Collider2D other)
        {
            print("hello");
            if (other.CompareTag("Player"))
            {
                print("you're the player");
                print(list.GetLineByIndex(index).text);
                FindFirstObjectByType<DialogueManager>().ShowDialogue(list.GetLineByIndex(index));
            }
        }
    }
}