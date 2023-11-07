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

    int atk { get; set; }
    #endregion
/////////////////////////////////// Movement ///////////////////////////////////
    #region Movement
    bool isFacingRight { get; set; }
    // Gravity //
    #region Gravity
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
    float runMaxSpeed { get; }
    
    /// <summary>
    /// The actual force (multiplied with speedDiff) applied to the player.
    /// </summary>
    float runAccelAmount { get; }
    
    /// <summary>
    /// Actual force (multiplied with speedDiff) applied to the player.
    /// </summary>
    float runDeccelAmount { get; }

    /// <summary>
    /// Multiplier applied to acceleration rate when airborne.
    /// </summary>
    float accelInAir { get; }
    /// <summary>
    /// Multiplier applied to acceleration rate when airborne.
    /// </summary>
    float deccelInAir { get; }
    #endregion
    // Jump //
    #region Jump
    /// <summary>
    /// The actual force applied (upwards) to the player when they jump.
    /// </summary>
    float jumpForce { get; }

    /// <summary>
    /// Number of frames to wait before applying force during double jump.
    /// </summary>
    int doubleJumpDelay { get; }
    /// <summary>
    /// Number of frames over which to apply force during double jump.
    /// </summary>
    int doubleJumpDuration { get; }

    /// <summary>
    /// Multiplier to increase gravity if the player releases the jump button while still jumping.
    /// </summary>
    float jumpCutGravityMult { get; }

    /// <summary>
    /// Reduces gravity while close to the apex (desired max height) of the jump
    /// </summary>
    float jumpHangGravityMult { get; }

    /// <summary>
    /// Speeds (close to 0) where the player will experience extra "jump hang".
    /// The player's velocity.y is closest to 0 at the jump's apex
    /// </summary>
    float jumpHangTimeThreshold { get; }

    float jumpHangAccelerationMult { get; }
    float jumpHangMaxSpeedMult { get; }
    #endregion

    #endregion
}
