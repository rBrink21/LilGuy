using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Friends
{
    public class FriendManager : MonoBehaviour
    {
        [SerializeField] List<int> newFriendScoreThreshholds;
        [SerializeField] private float postThreshholdsScalingFactor = 2f;
        [SerializeField] private ObtainableFriends obtainableFriendsAsset;
        private List<Friend> obtainableFriends;
        public static int friendsUnlocked;
        public Action<List<Friend>> OnFriendUnlocked;

        public bool debug;
        private void Start()
        {
            // So you don't mutate the asset.
            obtainableFriends = obtainableFriendsAsset.obtainableFriends;
            
            FindFirstObjectByType<ScoreKeeper>().ScoreUpdated += CheckIfNewFriendUnlocked;
        }

        private void Update()
        {
            if (debug && Input.GetKeyDown(KeyCode.R))
            {
                OnFriendUnlocked?.Invoke(GetFriendSelection(3));
            }
        }

        private void CheckIfNewFriendUnlocked(int score)
        {
            if (friendsUnlocked >= newFriendScoreThreshholds.Count)
            {
                var currentCost = 10 * postThreshholdsScalingFactor * friendsUnlocked;
                if (score > currentCost)
                {
                    UnlockFriend();
                    return;
                }
            }

            var cost = newFriendScoreThreshholds.ElementAt(friendsUnlocked);
            if (score > cost)
            {
                UnlockFriend();
            }
        }

        private List<Friend> GetFriendSelection(int amount)
        {
            if (amount >= obtainableFriends.Count)
            {
                return obtainableFriends;
            }

            var randomizedFriends = obtainableFriends.OrderBy(_ => new Random().Next()).ToList();
            return randomizedFriends.Take(amount).ToList();
        }

        private void UnlockFriend()
        {
            friendsUnlocked++;
            OnFriendUnlocked?.Invoke(GetFriendSelection(3));
        }
        
    }
}