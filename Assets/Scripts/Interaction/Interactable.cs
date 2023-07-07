using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] bool _isAuto;
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
