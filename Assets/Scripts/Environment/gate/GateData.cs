using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gate Data")]
public class GateData : ScriptableObject
{
    [Serializable]
    public struct GateInfo
    {
        public int scene;
        public int index;
        public bool lowered;
    }

    [SerializeField] GateInfo[] _info;
    public GateInfo[] info { get { return _info; } }
}
