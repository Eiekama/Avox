using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AInteractable : MonoBehaviour
{
    /// <summary>
    /// Determines whether interaction should occur automatically or by pressing
    /// a button.
    /// </summary>
    [SerializeField] protected bool _isAuto = true;
    public bool isAuto { get { return _isAuto; } }

    public abstract void Interact(PlayerInstance player);
}
