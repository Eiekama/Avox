using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInstance _player;
    public PlayerInput playerInput;
    public InputActions.PlayerActions playerInputActions;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new InputActions().Player;
        playerInputActions.Enable();
    }

    private void FixedUpdate()
    {
        //if (true) // replace later with key to press for interactions
        //{
        //    if (_player.currentManualInteractable != null)
        //    {
        //        _player.currentManualInteractable.Interact(_player);
        //    }
        //}
        
        _player.movement.Run(playerInputActions.Run.ReadValue<float>());
    }

    public void JumpCallback(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() == 1.0f)
        {
            _player.movement.Jump();
        }
    }
}