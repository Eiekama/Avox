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
    [SerializeField] int _atk;
    public int atk
    {
        get { return _atk; }
        set { _atk = Mathf.Max(0, value); }
    }

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

    [Space(30)]
    #endregion

    #region Movement
    
    [Header("Movement")]
    [Space(5)]

    [Header("Gravity")]
    [Space(5)]

    [SerializeField] float _fallGravityMult;
    public float fallGravityMult { get { return _fallGravityMult; } }

    [SerializeField] float _maxFallSpeed;
    public float maxFallSpeed { get { return _maxFallSpeed; } }

    /// <summary>
    /// Downwards force (gravity) needed for the desired jumpHeight and jumpTimeToApex.
    /// </summary>
    private float _gravityStrength;
    public float gravityScale { get; private set; }

    [Space(20)]


    [Header("Run")]
    [Space(5)]
    [SerializeField] private float _runMaxSpeed = 10.0f;
    public float runMaxSpeed { get { return _runMaxSpeed; } }

    [Tooltip("The speed at which our player accelerates to max speed," +
             "can be set to 1 for instant acceleration down to 0 for none at all.")]
    [Range(0f, 1)] [SerializeField] private float _runAcceleration = 0.8f;
    public float runAccelAmount { get; private set; }

    [Tooltip("The speed at which our player decelerates from their current speed," +
             "can be set to 1 for instant deceleration down to 0 for none at all.")]
    [Range(0f, 1)] [SerializeField] private float _runDecceleration = 0.8f;
    public float runDeccelAmount { get; private set; }

    [Space(5)]
    [Range(0f, 1)] [SerializeField] private float _accelInAir;
    public float accelInAir { get { return _accelInAir; } }
    [Range(0f, 1)] [SerializeField] private float _deccelInAir;
    public float deccelInAir { get { return _deccelInAir; } }

    [Space(20)]


    [Header("Jump")]
    [Space(5)]

    [Tooltip("Height of the player's jump.")]
    [SerializeField] private float _jumpHeight;

    [Tooltip("Time between applying the jump force and reaching the desired jump height." +
             "These values also control the player's gravity and jump force.")]
    [SerializeField] private float _jumpTimeToApex;
    [HideInInspector] public float jumpForce { get; private set; }


    [Space(10)]
    [Header("All Jumps")]
    [SerializeField] private float _jumpCutGravityMult;
    public float jumpCutGravityMult { get { return _jumpCutGravityMult; } }

    //below are stuff i havent organised yet



    [Space(50)]

    [Header("Run")]
    
    [Space(5)]
    public bool doConserveMomentum = true;

    [Space(20)]

    [Header("Both Jumps")]
    
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
        _gravityStrength = -(2 * _jumpHeight) / (_jumpTimeToApex * _jumpTimeToApex);

        //Calculate the rigidbody's gravity scale (ie: gravity strength relative to unity's gravity value, see project settings/Physics2D)
        gravityScale = _gravityStrength / Physics2D.gravity.y;

        //Calculate are run acceleration & deceleration forces using formula: amount = ((1 / Time.fixedDeltaTime) * acceleration) / runMaxSpeed
        runAccelAmount = 50 * _runAcceleration;
        runDeccelAmount = 50 * _runDecceleration;

        //Calculate jumpForce using the formula (initialJumpVelocity = gravity * timeToJumpApex)
        jumpForce = Mathf.Abs(_gravityStrength) * _jumpTimeToApex;

        #region Variable Ranges
        _runAcceleration = Mathf.Clamp(_runAcceleration, 0.01f, runMaxSpeed);
        _runDecceleration = Mathf.Clamp(_runDecceleration, 0.01f, runMaxSpeed);
        #endregion
    }
}