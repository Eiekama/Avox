using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInstance _player;

    private void Start()
    {
    }

    private void Update()
    {
        if (true) // replace later with key to press for interactions
        {
            if (_player.currentInteractable != null)
            {
                _player.currentInteractable.Interact(_player);
            }
        }
    }

    public void onRun(InputAction.CallbackContext context)
    {
        float movementInput = context.ReadValue<float>();
        Vector2 movementParsed = new Vector2(movementInput, 0);
        _player.movement.Run(movementParsed);
        return;
    }
}