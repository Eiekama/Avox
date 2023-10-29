using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Unity.VisualScripting;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerController))]
public class PlayerInstance : MonoBehaviour
{
    [SerializeField] PlayerData _data;
    public PlayerData data { get { return _data; } }

    [SerializeField] LayerMask _groundLayer;

    public PlayerController controller { get; private set; }

    public readonly IStatus status = new Status();
    public readonly IMovement movement = new Movement();
    public readonly ICombat combat = new Combat();

    public readonly IInteraction interaction = new Interaction();

    public Rigidbody2D RB { get; private set; }

    public AInteractable currentManualInteractable;

    public Animator RespawnAnimator;


    private void Awake()
    {
        controller = GetComponent<PlayerController>();

        status.player = this;
        movement.player = this;
        movement.playerBoxCollider = GetComponent<BoxCollider2D>();
        movement.facing = 1.0f;
        combat.player = this;

        movement.groundCheckSize = GetComponent<BoxCollider2D>().size + new Vector2(-0.02f, 0.0f);
        movement.groundLayer = _groundLayer;

        combat.attackHitbox = GetComponentInChildren<AttackHitbox>(true);
        combat.attackHitbox.data = _data;

        RespawnAnimator = GetComponentInChildren<Animator>(true);
        if(RespawnAnimator == null){ int i = 0; int j = 1/i; }
        RespawnAnimator.gameObject.SetActive(true);

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
            interactable.OnEnter(this);
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out AInteractable interactable))
        {
            interactable.OnExit(this);
        }
    }
}