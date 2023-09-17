using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

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

        runWithMaxSpeed();
    }

    private void runWithMaxSpeed()
    {
        float runInput = _player.playerInputActions.Run.ReadValue<float>();
        float currSpeed = Vector2.SqrMagnitude(_player.RB.velocity);
        float maxSpeed = _player.maxSpeed * _player.maxSpeed;
        
        if (currSpeed > maxSpeed)
        {
            float brakeSpeed = currSpeed - maxSpeed;  // calculate the speed decrease

            Vector2 normalisedVelocity = _player.RB.velocity.normalized;
            Vector2 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector2 value
            Debug.Log(brakeSpeed);
            Debug.Log(currSpeed);

            _player.RB.AddForce(-brakeVelocity + (new Vector2(runInput, 0) * 5.0f), ForceMode2D.Force);  // apply opposing brake force
        }
        else
        {
            _player.RB.AddForce(new Vector2(runInput, 0) * 5.0f, ForceMode2D.Force);  // apply opposing brake force
        }
    }
}