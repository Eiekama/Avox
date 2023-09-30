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
    bool Controllable();

    /// <summary>
    /// Controls the horizontal movement of the player character.
    /// </summary>
    /// TODO: Add lerpAmount back to do funny things with math and linear interpolation
    void Run(float moveInput);
    void Turn();
    void Jump();

    void Dash();
    void DoubleJump();
}
