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

        // TODO: Math to get this feeling correct lol
        public void Run(float moveInput)
        {
            float currSpeed = Mathf.Abs(player.RB.velocity.x);
            float maxSpeed = player.data.maxSpeed;
            
            if (currSpeed > maxSpeed)
            {
                float brakeSpeed = Mathf.Abs(maxSpeed - currSpeed);  // calculate the speed decrease
                player.RB.AddForce((-moveInput * player.data.runSpeed * new Vector2(brakeSpeed, 0)) + (new Vector2(moveInput, 0) * player.data.runSpeed), ForceMode2D.Force);  // apply opposing brake force
            }
            else
            {
                Debug.Log(new Vector2(moveInput, 0) * player.data.runSpeed);
                player.RB.AddForce(new Vector2(moveInput, 0) * player.data.runSpeed, ForceMode2D.Force); // apply force normally
            }
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
