using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class TransitionData : ScriptableObject
{
    [SerializeField] Dictionary<int, Array> _transitionLink = new Dictionary<int, Array>();

    public Dictionary<int, Array> transitionLink
    {
        get => _transitionLink;
        set => _transitionLink = value;
    }
}
