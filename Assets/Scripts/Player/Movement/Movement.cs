// credit: https://github.com/Dawnosaur/platformer-movement/blob/main/Scripts/PlayerData.cs

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class Movement : ASystem, IMovement
    {
        public bool Controllable()
        {
            // ADD IMPLEMENTATION HERE
            return true; //replace
        }

        public void Run(float moveInput)
        {
            float _targetSpeed = moveInput * player.data.runMaxSpeed;

            float _accelRate;
            _accelRate = (Mathf.Abs(_targetSpeed) > 0.01f) ? player.data.runAccelAmount : player.data.runDeccelAmount;

            float _speedDif = _targetSpeed - player.RB.velocity.x;
            float _movement = _speedDif * _accelRate;
            player.RB.AddForce(_movement * Vector2.right, ForceMode2D.Force);
        }

        public void Turn()
        {
            // ADD IMPLEMENTATION HERE
        }

        public void Jump()
        {
            player.RB.AddForce(Vector2.up * player.data.jumpStrength, ForceMode2D.Impulse);
        }

        public void Dash()
        {
            // ADD IMPLEMENTATION HERE
        }
        public void DoubleJump()
        {
            // ADD IMPLEMENTATION HERE
        }
    }
}
