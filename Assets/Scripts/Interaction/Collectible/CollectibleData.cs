using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Collectible Data")]
public class CollectibleData : ScriptableObject
{
    [Serializable]
    public struct CollectibleInfo
    {
        public int scene;
        public int index;
        public bool collected;
    }

    [SerializeField] CollectibleInfo[] _info;
    public CollectibleInfo[] info { get { return _info; } }
}
