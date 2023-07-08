using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour
{
    [SerializeField] bool _isAuto;
    /// <summary>
    /// Determines whether interaction should occur automatically or by pressing
    /// a button.
    /// </summary>
    public bool isAuto { get { return _isAuto; } }

    public abstract void Interact(Collider2D other);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isAuto)
        {
            Interact(other);
        }
        else
        {
            //display icon
            //tell inputmaster that there's an interactable in range
        }
    }
}
