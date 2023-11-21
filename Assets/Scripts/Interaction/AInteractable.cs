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

    public CollectIcon interactUI;

    public abstract void Interact(PlayerInstance player);

    public virtual void OnEnter(PlayerInstance player)
    {
        if (_isAuto) { Interact(player); }
        else
        {
            player.interaction.OpenInteractableIcon(this);
            player.currentManualInteractable = this;
        }
    }
    public virtual void OnExit(PlayerInstance player)
    {
        if (!_isAuto)
        {
            player.interaction.CloseInteractableIcon(this);
            player.currentManualInteractable = null;
        }
    }
}
