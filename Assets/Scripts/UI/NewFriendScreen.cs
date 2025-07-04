using System;
using Friends;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class NewFriendScreen : MonoBehaviour
    {
        [SerializeField] private int newFriendScoreThreshhold = 10;
        [SerializeField] private VisualTreeAsset newFriendScreen;
        [SerializeField] private VisualTreeAsset cardTemplate;
        private UIDocument doc;
        private VisualTreeAsset originalRoot;
        private FriendManager fm;
        private void Start()
        {
            FindFirstObjectByType<ScoreKeeper>().ScoreUpdated += CheckIfNewFriendUnlocked;
            doc = GetComponent<UIDocument>();
            originalRoot = doc.visualTreeAsset;
            fm = FindFirstObjectByType<FriendManager>();
        }

        private void CheckIfNewFriendUnlocked(int score)
        {
            if (score % newFriendScoreThreshhold == 0)
            {
                ShowScreen();
            }
        }

        private void ShowScreen()
        {
            Time.timeScale = 0;
            doc.visualTreeAsset = newFriendScreen;

            var container = doc.rootVisualElement.Q<VisualElement>("cardContainer");
            foreach (var friend in fm.GetFriendSelection(3))
            {
                var templateContainer = cardTemplate.Instantiate();
                var cardElement = templateContainer.Q<FriendSelectionCard>();
                container.Add(cardElement);
                cardElement.Init(friend);
            }


        }

        private void StopShowingScreen()
        {
            Time.timeScale = 1;
            doc.visualTreeAsset = originalRoot;
        }
        
        
    }
}