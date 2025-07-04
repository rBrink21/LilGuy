using System;
using System.Collections.Generic;
using UnityEngine;

namespace Friends
{
    [CreateAssetMenu]
    public class ObtainableFriends : ScriptableObject
    {
        [SerializeField] public List<Friend> obtainableFriends;
    }
}
