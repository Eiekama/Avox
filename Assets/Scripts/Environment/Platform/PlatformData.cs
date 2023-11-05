using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Platform Data")]
public class PlatformData : ScriptableObject
{
    [Serializable]
    public struct PlatformInfo
    {
        public int scene;
        public int index;
        public bool collapsed;
    }

    [SerializeField] PlatformInfo[] _info;
    public PlatformInfo[] info { get { return _info; } }
}
