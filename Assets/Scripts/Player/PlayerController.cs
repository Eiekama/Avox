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

        inputActions.Player.Jump.performed += JumpPerformed;
        inputActions.Player.Jump.canceled += JumpCanceled;
        inputActions.Player.Interact.performed += InteractPerformed;
        inputActions.Player.Attack.performed += AttackPerformed;
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Start()
    {
        if (_player.data.isFacingRight && transform.localScale.x < 0
         || !_player.data.isFacingRight && transform.localScale.x > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
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
        _player.movement.Run(inputActions.Player.Run.ReadValue<float>());
    }

    public void JumpPerformed(InputAction.CallbackContext _)
    {
        if (_player.movement.CanJump()) { _player.movement.Jump(); }
        else if (_player.movement.CanDoubleJump()) { _player.movement.DoubleJump(); }
    }

    public void JumpCanceled(InputAction.CallbackContext _)
    {
        _player.movement.JumpCut();
    }

    public void InteractPerformed(InputAction.CallbackContext context)
    {
        if (_player.currentManualInteractable != null)
        {
            _player.currentManualInteractable.Interact(_player);
        }
    }

    public void AttackPerformed(InputAction.CallbackContext context)
    {
        _player.combat.Attack();
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