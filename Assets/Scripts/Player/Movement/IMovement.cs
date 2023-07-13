// credit: https://github.com/Dawnosaur/platformer-movement/blob/main/Scripts/PlayerData.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    PlayerInstance player { get; set; }

    /// <summary>
    /// Determines when player can/cannot control the character.
    /// <br/>
    /// E.g. during dialogue, taking damage, after hard fall.
    /// </summary>
    /// <returns></returns>
    bool CanBeControlled();

    /// <summary>
    /// Controls the horizontal movement of the player character.
    /// </summary>
    void Run(Vector2 moveInput, float lerpAmount);
    void Turn();
    void Jump();

    void Dash();
    void DoubleJump();
}
