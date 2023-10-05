using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerController))]
public class PlayerInstance : MonoBehaviour
{
    [SerializeField] PlayerData _data;
    public PlayerData data { get { return _data; } }

    public PlayerController controller { get; private set; }

    public readonly IStatus status = new Status();
    public readonly IMovement movement = new Movement();
    public readonly ICombat combat = new Combat();

    public readonly IInteraction interaction = new Interaction();

    public Rigidbody2D RB { get; private set; }

    public AInteractable currentManualInteractable { get; private set; }


    private void Awake()
    {
        controller = GetComponent<PlayerController>();

        status.player = this;
        movement.player = this;
        combat.player = this;

        combat.attackHitbox = GetComponentInChildren<AttackHitbox>(true);
        combat.attackHitbox.data = _data;

        RB = GetComponent<Rigidbody2D>();
    }


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
                currentManualInteractable = interactable;
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out AInteractable interactable))
        {
            interaction.CloseInteractableIcon(interactable);
            if (currentManualInteractable = interactable)
            {
                currentManualInteractable = null;
            }
        }
    }
}