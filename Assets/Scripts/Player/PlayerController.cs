using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInstance))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInstance _player;
    public PlayerInput playerInput { get; private set; }
    public InputActions.PlayerActions playerInputActions { get; private set; }

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new InputActions().Player;
        playerInputActions.Enable();
    }

    private void Update()
    {
        _player.movement.UpdateTimers();
        _player.movement.UpdateChecks();
        _player.movement.UpdateGravity();
    }

    private void FixedUpdate()
    {
        _player.movement.Run(playerInputActions.Run.ReadValue<float>());
    }

    public void JumpCallback(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _player.movement.Jump();
        }
        if (context.canceled)
        {
            _player.movement.JumpCut();
        }
    }

    public void InteractCallback(InputAction.CallbackContext context)
    {
        if (_player.currentManualInteractable != null && context.performed)
        {
            _player.currentManualInteractable.Interact(_player);
        }
    }

    public void AttackCallback(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _player.combat.Attack(this);
        }
    }
}