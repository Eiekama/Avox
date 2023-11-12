using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInstance))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInstance _player;
    public PlayerInputActions inputActions { get; private set; }

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void Update()
    {
        _player.movement.UpdateTimers();
        _player.movement.UpdateChecks();
        _player.movement.UpdateGravity();
        _player.movement.UpdateAnimationParameters();
    }

    private void FixedUpdate()
    {
        if (inputActions.Player.enabled)
        {
            _player.movement.Run(inputActions.Player.Run.ReadValue<float>());
        }
    }

    public void JumpCallback(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_player.movement.CanJump()) { _player.movement.Jump(); }
            else if (_player.movement.CanDoubleJump()) { _player.movement.DoubleJump(); }
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
            _player.combat.Attack();
        }
    }

    /// <summary>
    /// Disables all current active action maps before enabling <c>actionMap</c>.
    /// </summary>
    /// <param name="actionMap">Action map to enable.</param>
    public void ToggleActionMap(InputActionMap actionMap)
    {
        if (actionMap.enabled) { return; }
        inputActions.Disable();
        actionMap.Enable();
    }

    public void DisableActionMap(InputActionMap actionMap)
    {
        if (!actionMap.enabled) { return; }
        actionMap.Disable();
    }
}