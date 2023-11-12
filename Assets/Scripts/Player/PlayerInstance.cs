using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(Animator))]
public class PlayerInstance : MonoBehaviour
{
    [SerializeField] PlayerData _data;
    public PlayerData data { get { return _data; } }

    [SerializeField] LayerMask _groundLayer;

    public PlayerController controller { get; private set; }
    public Animator animator { get; private set; }

    public readonly IStatus status = new Status();
    public readonly IMovement movement = new Movement();
    public readonly ICombat combat = new Combat();

    public readonly IInteraction interaction = new Interaction();

    public Rigidbody2D RB { get; private set; }

    [HideInInspector] public AInteractable currentManualInteractable;

    public Animator RespawnAnimator;


    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();

        status.player = this;

        movement.player = this;
        movement.playerBoxCollider = GetComponent<BoxCollider2D>();
        movement.groundLayer = _groundLayer;

        combat.player = this;

        combat.attackHitbox = GetComponentInChildren<AttackHitbox>(true);
        combat.attackHitbox.data = _data;

        Animator[] Anims = GetComponentsInChildren<Animator>(true);
        foreach (Animator anim in Anims)
        {
            if (anim.name == "WSCrossfade"){ RespawnAnimator = anim; Debug.Log(anim); }
        }
        if(RespawnAnimator == null){ int i = 0; int j = 1/i; }
        
        StartCoroutine(status.RecoverMP());

        RB = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (_data.isFacingRight && transform.localScale.x < 0
         || !_data.isFacingRight && transform.localScale.x > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
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