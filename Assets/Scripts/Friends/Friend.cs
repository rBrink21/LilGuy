using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Friends
{
    [Serializable]
    public struct Friend
    {
        public GameObject friendPrefab;
        public string displayName;
        public string description;
    }
}