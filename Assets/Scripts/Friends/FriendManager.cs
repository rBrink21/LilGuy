using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

namespace Friends
{
    public class FriendManager : MonoBehaviour
    {
        public static FriendManager instance;
        
        [SerializeField] List<int> newFriendScoreThreshholds;
        [SerializeField] private float postThreshholdsScalingFactor = 2f;
        [SerializeField] private ObtainableFriends obtainableFriendsAsset;
        private List<Friend> obtainableFriends;
        private static int friendsUnlocked;
        public Action<List<Friend>> OnFriendUnlocked;

        public List<Friend> unlockedFriends =new List<Friend>();
        public bool debug;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
        
        private void Start()
        {
            // So you don't mutate the asset.
            obtainableFriends = obtainableFriendsAsset.obtainableFriends;
            friendsUnlocked = 0;
            
            FindFirstObjectByType<ScoreKeeper>().ScoreUpdated += CheckIfNewFriendUnlocked;

            SceneManager.sceneLoaded += Initialize;
        }


        private void Initialize(Scene scene, LoadSceneMode mode)
        {
            foreach (var friend in unlockedFriends)
            {
                var friendInstance = Instantiate(friend.friendPrefab);
                friendInstance.transform.position = GameObject.FindWithTag("Player").transform.position;
            }
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
            if (score >= cost)
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

        public ScoreKeeper.ScoreRange GetScoreRangeCost()
        {
            if (friendsUnlocked >= newFriendScoreThreshholds.Count)
            {
                return new ScoreKeeper.ScoreRange
                {
                    low = 0,
                    high = int.MaxValue,
                };
            }

            if (friendsUnlocked == 0)
            {
                return new ScoreKeeper.ScoreRange
                {
                    low = 0,
                    high = newFriendScoreThreshholds.ElementAt(0)
                };
            }

            return new ScoreKeeper.ScoreRange
            {
                low = newFriendScoreThreshholds.ElementAt(friendsUnlocked - 1),
                high = newFriendScoreThreshholds.ElementAt(friendsUnlocked)
            };
        }
        
    }
}