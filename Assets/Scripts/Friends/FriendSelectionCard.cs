using System;
using Friends;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


    [UxmlElement]
    public partial class FriendSelectionCard : VisualElement
    {
        public Action<Friend> FriendSelected; 
        
        private VisualElement image => this.Q<VisualElement>("sprite");
        private Label nameLabel => this.Q<Label>("friendName");
        private Label description => this.Q<Label>("friendDescription");

        public void Init(Friend friend)
        {
            image.style.backgroundImage = new StyleBackground(friend.friendPrefab.GetComponent<SpriteRenderer>().sprite);
            nameLabel.text = friend.displayName;
            description.text = friend.description;
            
            this.AddManipulator(new Clickable(() => FriendSelected?.Invoke(friend)));
            
        }
        
        
        
        public FriendSelectionCard() {}
    }
