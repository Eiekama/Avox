// credit: https://github.com/Dawnosaur/platformer-movement/blob/main/Scripts/PlayerData.cs

using UnityEngine;

public interface IPlayerData
{
//////////////////////////////////// Status ////////////////////////////////////
    #region Status
    int maxHP { get; set; }
    int currentHP { get; set; }
    int maxMP { get; set; }
    int currentMP { get; set; }
    /// <summary>
    /// Amount of MP that player recovers per second when conditions are met.
    /// </summary>
    float MPRecoveryRate { get; }

    //int atk { get; }
    #endregion
/////////////////////////////////// Movement ///////////////////////////////////
    #region Movement
    // Gravity //
    #region Gravity
    /// <summary>
    /// Downwards force (gravity) needed for the desired jumpHeight and jumpTimeToApex.
    /// </summary>
    float gravityStrength { get; }
    /// <summary>
    /// Strength of the player's gravity as a multiplier of gravity (set in ProjectSettings/Physics2D).
    /// <br/>
    /// Also the value the player's rigidbody2D.gravityScale is set to.
    /// </summary>
    float gravityScale { get; }

    /// <summary>
    /// Multiplier to the player's gravityScale when falling.
    /// </summary>
    float fallGravityMult { get; }
    /// <summary>
    /// Maximum fall speed (terminal velocity) of the player when falling.
    /// </summary>
    float maxFallSpeed { get; }
    #endregion
    // Run //
    #region Run
    /// <summary>
    /// Target speed we want the player to reach.
    /// </summary>
    public float runMaxSpeed { get; }
    
    /// <summary>
    /// The actual force (multiplied with speedDiff) applied to the player.
    /// </summary>
    public float runAccelAmount { get; }
    
    /// <summary>
    /// Actual force (multiplied with speedDiff) applied to the player.
    /// </summary>
    public float runDeccelAmount { get; }

    /// <summary>
    /// Multiplier applied to acceleration rate when airborne.
    /// </summary>
    public float accelInAir { get; }
    /// <summary>
    /// Multiplier applied to acceleration rate when airborne.
    /// </summary>
    public float deccelInAir { get; }
    #endregion
    // Jump //
    #region Jump
    public float jumpStrength { get; }
    #endregion

    #endregion
}
