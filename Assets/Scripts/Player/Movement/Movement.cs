// credit: https://github.com/Dawnosaur/platformer-movement/blob/main/Scripts/PlayerData.cs

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class Movement : ASystem, IMovement
    {
        public BoxCollider2D playerBoxCollider { get; set; }
        public Vector2 groundCheckSize { get; set; }
        public LayerMask groundLayer { get; set; }

        private float _lastOnGroundTime;
        public float lastOnGroundTime
        {
            get { return _lastOnGroundTime; }
            set { _lastOnGroundTime = Mathf.Max(-0.1f, value); }
        }
        
        private float _facing;
        public float facing
        {
            get { return _facing; }
            set { _facing = value; }
        }

        private bool _isJumpCut;

        private float _lastDashTime;
        public float lastDashTime
        {
            get { return _lastDashTime; }
            set { _lastDashTime = Mathf.Max(-0.1f, value); }
        }

        public void UpdateTimers()
        {
            lastOnGroundTime -= Time.deltaTime;
            lastDashTime -= Time.deltaTime;
        }

        private void SetGravityScale(float scale) { player.RB.gravityScale = scale; }
        public void UpdateGravity()
        {
            if (lastDashTime > 0)
            {
                // Near-zero gravity if dashing
                SetGravityScale(player.data.gravityScale * player.data.dashGravityMultiplier);
            }
            else if (_isJumpCut)
            {
                //Higher gravity if jump button released
                SetGravityScale(player.data.gravityScale * player.data.jumpCutGravityMult);
                //Caps maximum fall speed
                player.RB.velocity = new Vector2(player.RB.velocity.x, Mathf.Max(player.RB.velocity.y, -player.data.maxFallSpeed));
            } else if (System.Math.Abs(player.RB.velocity.y) < player.data.jumpHangTimeThreshold 
                       && lastOnGroundTime <= 0)
            {
                SetGravityScale(player.data.gravityScale * player.data.jumpHangGravityMult);
            }
            else if (player.RB.velocity.y < 0)
            {
                //Higher gravity if falling
                SetGravityScale(player.data.gravityScale * player.data.fallGravityMult);
                //Caps maximum fall speed
                player.RB.velocity = new Vector2(player.RB.velocity.x, Mathf.Max(player.RB.velocity.y, -player.data.maxFallSpeed));
            }
            else
            {
                //Default gravity if standing on a platform or moving upwards
                SetGravityScale(player.data.gravityScale);
            }
        }

        public void UpdateChecks()
        {
            #region COLLISION CHECKS    
            Vector2 terrainCheckPoint = (Vector2)player.transform.position + playerBoxCollider.offset - new Vector2(0.0f, 0.01f);
            if (Physics2D.OverlapBox(terrainCheckPoint, groundCheckSize, 0, groundLayer))
            {
                lastOnGroundTime = 0.01f;
                _isJumpCut = false;
            }
            #endregion

            #region JUMP CHECKS
            if (player.RB.velocity.y < 0)
            {
                _isJumpCut = false;
            }
            #endregion
        }

        public void Run(float moveInput)
        {
            float currMaxSpeed = player.data.runMaxSpeed;
            if (lastDashTime > 0)
            {
                currMaxSpeed = player.data.dashMaxSpeed;
            }
            if (moveInput < 0.0f || moveInput > 0.0f)
            {
                Turn(moveInput);
            }

            float _targetSpeed = moveInput * currMaxSpeed;

            float _accelRate;
            //Gets an acceleration value based on if we are accelerating (includes turning) 
            //or trying to decelerate (stop). As well as applying a multiplier if we're air borne.
            if (lastOnGroundTime > 0)
            {
                _accelRate = (Mathf.Abs(_targetSpeed) > 0.01f)
                    ? player.data.runAccelAmount
                    : player.data.runDeccelAmount;
            }
            else
            {
                _accelRate = (Mathf.Abs(_targetSpeed) > 0.01f)
                    ? player.data.runAccelAmount * player.data.accelInAir
                    : player.data.runDeccelAmount * player.data.deccelInAir;
                if (Mathf.Abs(player.RB.velocity.y) < player.data.jumpHangTimeThreshold)
                {
                    _accelRate *= player.data.jumpHangAccelerationMult;
                    _targetSpeed *= player.data.jumpHangMaxSpeedMult;
                }
            }

            float _speedDif = _targetSpeed - player.RB.velocity.x;
            float _movement = _speedDif * _accelRate;
            
            player.RB.AddForce(_movement * Vector2.right, ForceMode2D.Force);
        }

        public void Turn(float direction)
        {
            facing = direction;
            Vector3 scale = player.gameObject.transform.localScale;
            scale.x = direction;
            player.gameObject.transform.localScale = scale;
        }

        public void Jump()
        {
            // reset gravity, jump cut, and velocity
            if (lastOnGroundTime > 0)
            {
                player.RB.AddForce(Vector2.up * player.data.jumpForce, ForceMode2D.Impulse);
            }
            else
            {
                DoubleJump();
            }
        }
        public void JumpCut()
        {
            _isJumpCut = true;
        }

        public void Dash()
        {
            Debug.Log("dash " + facing);
            player.RB.AddForce(facing * new Vector2(player.data.dashForce, 0), ForceMode2D.Impulse);
            lastDashTime = player.data.dashMaxSpeedDuration;
        }
        public void DoubleJump()
        {
            SetGravityScale(player.data.gravityScale);
            player.RB.velocity = new Vector2(player.RB.velocity.x, 0);
        }

        public IEnumerator DoubleJumpCoroutine()
        {
            for (int i = 0; i < 6; i++)
            {
                if (lastOnGroundTime > 0)
                {
                    yield break;
                }
                if (i > 1)
                {
                    player.RB.AddForce(Vector2.up * player.data.jumpForce * 15.0f, ForceMode2D.Force);
                }
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
