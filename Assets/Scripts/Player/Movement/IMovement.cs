// credit: https://github.com/Dawnosaur/platformer-movement/blob/main/Scripts/PlayerData.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    PlayerInstance player { get; set; }
    BoxCollider2D playerBoxCollider { get; set; }

    LayerMask groundLayer { get; set; }

    /// <summary>
    /// If positive, then player is grounded.
    /// </summary>
    float lastOnGroundTime { get; set; }

    void UpdateTimers();
    void UpdateGravity();
    void UpdateChecks();
    void UpdateAnimationParameters();

    /// <summary>
    /// Controls the horizontal movement of the player character.
    /// </summary>
    void Run(float moveInput);
    void Jump();
    void JumpCut();

    void Dash();
    /// <summary>
    ///  Checks if we're dashing and if so, stops dash.
    /// </summary>
    void StopDash();
    void DoubleJump();

    bool CanJump();
    bool CanDoubleJump();
    bool CanDash();
}
