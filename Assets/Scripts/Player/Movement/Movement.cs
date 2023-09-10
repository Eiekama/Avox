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

        public void Run(Vector2 moveInput, float lerpAmount)
        {
            // ADD IMPLEMENTATION HERE
        }
        public void Turn()
        {
            // ADD IMPLEMENTATION HERE
        }
        public void Jump()
        {
            // ADD IMPLEMENTATION HERE
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
