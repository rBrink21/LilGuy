using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Friends
{
    public class FriendManager : MonoBehaviour
    {
        [SerializeField] private ObtainableFriends obtainableFriendsAsset;
        private List<Friend> obtainableFriends;

        private void Start()
        {
            // So you don't mutate the asset.
            obtainableFriends = obtainableFriendsAsset.obtainableFriends;
        }

        public List<Friend> GetFriendSelection(int amount)
        {
            if (amount >= obtainableFriends.Count)
            {
                return obtainableFriends;
            }

            var randomizedFriends = obtainableFriends.OrderBy(_ => new Random().Next()).ToList();
            return randomizedFriends.Take(amount).ToList();
        }
    }
}