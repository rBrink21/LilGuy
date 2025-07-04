using System;
using System.Collections.Generic;
using Friends;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class NewFriendScreen : MonoBehaviour
    {
        [SerializeField] private VisualTreeAsset newFriendScreen;
        [SerializeField] private VisualTreeAsset cardTemplate;
        private UIDocument doc;
        private VisualTreeAsset originalRoot;
        private FriendManager fm;
        private void Start()
        {
            doc = GetComponent<UIDocument>();
            originalRoot = doc.visualTreeAsset;
            fm = FindFirstObjectByType<FriendManager>();
            fm.OnFriendUnlocked += ShowScreen;
        }

        private void ShowScreen(List<Friend> friends)
        {
            Time.timeScale = 0;
            doc.visualTreeAsset = newFriendScreen;

            var container = doc.rootVisualElement.Q<VisualElement>("cardContainer");
            foreach (var friend in friends)
            {
                var templateContainer = cardTemplate.Instantiate();
                var cardElement = templateContainer.Q<FriendSelectionCard>();
                container.Add(cardElement);
                cardElement.Init(friend);
                cardElement.FriendSelected += HandleFriendSelected;
            }


        }


        private void HandleFriendSelected(Friend friend)
        {
            var newFriend = Instantiate(friend.friendPrefab);
            newFriend.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;   
            StopShowingScreen();
        }
        
        private void StopShowingScreen()
        {
            Time.timeScale = 1;
            doc.visualTreeAsset = originalRoot;
            
            FindAnyObjectByType<HealthUIUpdater>().Init();
            FindAnyObjectByType<ScoreKeeper>().LoadScoreText();
        }
        
        
    }
}