// credit: https://github.com/Dawnosaur/platformer-movement/blob/main/Scripts/PlayerData.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    PlayerInstance player { get; set; }

    Vector2 groundCheckSize { get; set; }
    LayerMask groundLayer { get; set; }

    float lastOnGroundTime { get; }

    void UpdateTimers();
    void UpdateGravity();
    void UpdateChecks();

    /// <summary>
    /// Controls the horizontal movement of the player character.
    /// </summary>
    /// TODO: Add lerpAmount back
    void Run(float moveInput);
    void Turn();
    void Jump();
    void JumpCut();

    void Dash();
    void DoubleJump();
}
