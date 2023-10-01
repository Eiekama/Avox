// credit: https://github.com/Dawnosaur/platformer-movement/blob/main/Scripts/PlayerData.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Data")]
public class PlayerData : ScriptableObject, IPlayerData
{
    #region Status

    [Header("Status")] /////////////////////////////////////////////////////////
    [Space(5)]

    [SerializeField] int _maxHP;
    public int maxHP
    {
        get { return _maxHP; }
        set { _maxHP = Mathf.Max(0, value); }
    }

    [SerializeField] int _currentHP;
    public int currentHP
    {
        get { return _currentHP; }
        set { _currentHP = Mathf.Max(0, Mathf.Min(value, _maxHP)); }
    }

    [SerializeField] int _maxMP;
    public int maxMP
    {
        get { return _maxMP; }
        set { _maxMP = Mathf.Max(0, value); }
    }

    [SerializeField] int _currentMP;
    public int currentMP
    {
        get { return _currentMP; }
        set { _currentMP = Mathf.Max(0, Mathf.Min(value, _maxMP)); }
    }

    [Space(5)]

    [SerializeField] float _MPRecovaryRate;
    public float MPRecoveryRate { get { return _MPRecovaryRate; } }

    #endregion

    #region Movement
    
    [Header("Movement")]
    [Space(5)]
    [SerializeField] private float _maxSpeed = 10.0f;
    public float maxSpeed { get { return _maxSpeed; } }
    [SerializeField] private float _runSpeed = 25.0f;
    public float runSpeed { get { return _runSpeed; }}
    [SerializeField] private float _jumpStrength = 3.0f;
    public float jumpStrength { get { return _jumpStrength; } }
    [Space(30)]
    [Header("Gravity")]
    [Space(5)]
    private float _gravityStrength;
    public float gravityStrength { get { return _gravityStrength; } }
    private float _gravityScale;
    public float gravityScale { get { return _gravityScale; } }
    [SerializeField] float _fallGravityMult;
    public float fallGravityMult { get { return _fallGravityMult; } }
    [SerializeField] float _maxFallSpeed;
    public float maxFallSpeed { get { return _maxFallSpeed; } }
    [Space(20)]

    //below are stuff i havent organised yet





    [Header("Run")]
    public float runMaxSpeed; //Target speed we want the player to reach.
    public float runAcceleration; //The speed at which our player accelerates to max speed, can be set to runMaxSpeed for instant acceleration down to 0 for none at all
    [HideInInspector] public float runAccelAmount; //The actual force (multiplied with speedDiff) applied to the player.
    public float runDecceleration; //The speed at which our player decelerates from their current speed, can be set to runMaxSpeed for instant deceleration down to 0 for none at all
    [HideInInspector] public float runDeccelAmount; //Actual force (multiplied with speedDiff) applied to the player .
    [Space(5)]
    [Range(0f, 1)] public float accelInAir; //Multipliers applied to acceleration rate when airborne.
    [Range(0f, 1)] public float deccelInAir;
    [Space(5)]
    public bool doConserveMomentum = true;

    [Space(20)]

    [Header("Jump")]
    public float jumpHeight; //Height of the player's jump
    public float jumpTimeToApex; //Time between applying the jump force and reaching the desired jump height. These values also control the player's gravity and jump force.
    [HideInInspector] public float jumpForce; //The actual force applied (upwards) to the player when they jump.

    [Header("Both Jumps")]
    public float jumpCutGravityMult; //Multiplier to increase gravity if the player releases thje jump button while still jumping
    [Range(0f, 1)] public float jumpHangGravityMult; //Reduces gravity while close to the apex (desired max height) of the jump
    public float jumpHangTimeThreshold; //Speeds (close to 0) where the player will experience extra "jump hang". The player's velocity.y is closest to 0 at the jump's apex (think of the gradient of a parabola or quadratic function)
    [Space(0.5f)]
    public float jumpHangAccelerationMult;
    public float jumpHangMaxSpeedMult;

    [Header("Assists")]
    [Range(0.01f, 0.5f)] public float coyoteTime; //Grace period after falling off a platform, where you can still jump
    [Range(0.01f, 0.5f)] public float jumpInputBufferTime; //Grace period after pressing jump where a jump will be automatically performed once the requirements (eg. being grounded) are met.

    #endregion

    //Unity Callback, called when the inspector updates
    private void OnValidate()
    {
        //Calculate gravity strength using the formula (gravity = 2 * jumpHeight / timeToJumpApex^2) 
        _gravityStrength = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);

        //Calculate the rigidbody's gravity scale (ie: gravity strength relative to unity's gravity value, see project settings/Physics2D)
        _gravityScale = _gravityStrength / Physics2D.gravity.y;

        //Calculate are run acceleration & deceleration forces using formula: amount = ((1 / Time.fixedDeltaTime) * acceleration) / runMaxSpeed
        runAccelAmount = (50 * runAcceleration) / runMaxSpeed;
        runDeccelAmount = (50 * runDecceleration) / runMaxSpeed;

        //Calculate jumpForce using the formula (initialJumpVelocity = gravity * timeToJumpApex)
        jumpForce = Mathf.Abs(_gravityStrength) * jumpTimeToApex;

        #region Variable Ranges
        runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
        runDecceleration = Mathf.Clamp(runDecceleration, 0.01f, runMaxSpeed);
        #endregion
    }
}