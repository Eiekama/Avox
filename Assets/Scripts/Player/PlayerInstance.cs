using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInstance : MonoBehaviour
{
    [SerializeField] PlayerData _data;
    public PlayerData data { get { return _data; } }

    public readonly IStatus status = new Status();
    public readonly IMovement movement = new Movement();
    public readonly ICombat combat = new Combat();

    public readonly IInteraction interaction = new Interaction();

    public Rigidbody2D RB { get; private set; }


    public AInteractable currentInteractable { get; private set; }

    private void Awake()
    {
        status.player = this;
        movement.player = this;
        combat.player = this;

        combat.meleeCollider = GetComponentInChildren<MeleeCollider>(true);

        RB = GetComponent<Rigidbody2D>();
    }


    // remark: i forsee some bugs relating to interaction but since the
    // specifics haven't been decided yet i'll not take any measures to fix
    // the potential bugs for now

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IContactDamage damager))
        {
            damager.DealContactDamage(this);
        }
        else if (other.TryGetComponent(out AInteractable interactable))
        {
            if (interactable.isAuto)
            {
                interactable.Interact(this);
            }
            else
            {
                interaction.OpenInteractableIcon(interactable);
                currentInteractable = interactable;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out AInteractable interactable))
        {
            interaction.CloseInteractableIcon(interactable);
            if (currentInteractable = interactable)
            {
                currentInteractable = null;
            }
        }
    }
}