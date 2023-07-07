using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    /// <summary>
    /// Determines whether interaction should occur automatically or by pressing
    /// a button.
    /// </summary>
    bool isAuto { get; }

    abstract void Interact(Collider2D other);
}
