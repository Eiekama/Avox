// credit: https://github.com/Dawnosaur/platformer-movement/blob/main/Scripts/PlayerData.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    PlayerInstance player { get; set; }
    BoxCollider2D playerBoxCollider { get; set; }

    LayerMask groundLayer { get; set; }

    float lastOnGroundTime { get; }

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
    void DoubleJump();

    bool CanJump();
    bool CanDoubleJump();
}
