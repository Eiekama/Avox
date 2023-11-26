// credit: https://github.com/Dawnosaur/platformer-movement/blob/main/Scripts/PlayerData.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Movement : ASystem, IMovement
    {
        public BoxCollider2D playerBoxCollider { get; set; }
        public LayerMask groundLayer { get; set; }

        private float _lastOnGroundTime;
        public float lastOnGroundTime
        {
            get { return _lastOnGroundTime; }
            set { _lastOnGroundTime = Mathf.Max(-1.0f, value); }
        }

        private bool _isDoubleJumping;
        private bool _isJumpCut;

        private bool _isDashing = false;
        
        public void UpdateTimers()
        {
            lastOnGroundTime -= Time.deltaTime;
        }

        private void SetGravityScale(float scale) { player.RB.gravityScale = scale; }
        public void UpdateGravity()
        {
            if (_isDashing)
            {
                //Very low gravity if dashing
                SetGravityScale(player.data.gravityScale * player.data.dashGravityMult);
            } else if (_isJumpCut)
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
            Vector2 terrainCheckPoint = (Vector2)player.transform.position + playerBoxCollider.offset - new Vector2(0.0f, 0.1f);
            Vector2 groundCheckSize = playerBoxCollider.size + new Vector2(-0.1f, 0.0f);
            if (Physics2D.OverlapBox(terrainCheckPoint, groundCheckSize, 0, groundLayer))
            {
                lastOnGroundTime = 0.1f;
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

        public void UpdateAnimationParameters()
        {
            player.animator.SetFloat("yVelocity", player.RB.velocity.y);
            player.animator.SetFloat("lastOnGroundTime", lastOnGroundTime);
        }

        public void Run(float moveInput)
        {
            player.animator.SetInteger("xInput", Mathf.RoundToInt(moveInput));

            if (!player.controller.inputActions.Player.enabled && moveInput == 0) { return; }

            if (moveInput != 0)
            {
                UpdateDirectionToFace(moveInput > 0);
            }

            float facing = (player.data.isFacingRight)
                ? 1.0f
                : -1.0f;

            float _targetSpeed = (_isDashing) 
                ? facing * player.data.dashMaxSpeed 
                : moveInput * player.data.runMaxSpeed;

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
                if (Mathf.Abs(_targetSpeed) > 0.01f
                 && Mathf.Sign(player.RB.velocity.x) == Mathf.Sign(_targetSpeed)
                 && Mathf.Abs(player.RB.velocity.x) > Mathf.Abs(_targetSpeed))
                {
                    _accelRate *= (1 - player.data.conservedMomentum);
                }
            }

            float _speedDif = _targetSpeed - player.RB.velocity.x;
            float _movement = _speedDif * _accelRate;
            
            player.RB.AddForce(_movement * Vector2.right, ForceMode2D.Force);
        }

        public void UpdateDirectionToFace(bool isMovingRight)
        {
            if (isMovingRight != player.data.isFacingRight) { Turn(); }
        }

        public void Turn()
        {
            if (!_isDashing) // Can't turn unless dash is not active
            {
                Vector3 scale = player.transform.localScale;
                scale.x *= -1;
                player.transform.localScale = scale;

                player.data.isFacingRight = !player.data.isFacingRight;
            }
        }


        public bool CanJump()
        {
            return lastOnGroundTime > 0;
        }

        public void Jump()
        {
            player.animator.SetTrigger("jump");

            _isJumpCut = false;
            _isDashing = false; // Double jump stops dash
            player.StopCoroutine(DashCoroutine());
            lastOnGroundTime = 0;
            player.RB.AddForce(Vector2.up * (player.data.jumpForce - player.RB.velocity.y), ForceMode2D.Impulse);
        }

        public void JumpCut()
        {
            _isJumpCut = true;
        }

        public bool CanDoubleJump()
        {
            return lastOnGroundTime < 0 && !_isDoubleJumping && player.data.currentMP > 0;
        }

        public void DoubleJump()
        {
            player.animator.SetTrigger("jump");

            _isJumpCut = false;
            _isDashing = false; // Double jump stops dash
            player.StopCoroutine(DashCoroutine());
            player.status.ChangeCurrentMP(-1);
            player.StartCoroutine(DoubleJumpCoroutine());
        }

        public IEnumerator DoubleJumpCoroutine()
        {
            _isDoubleJumping = true;

            float force = player.data.jumpForce - Physics2D.gravity.y * player.data.gravityScale *
                player.data.doubleJumpDuration * Time.fixedDeltaTime;
            if (player.RB.velocity.y < 0)
                force -= player.RB.velocity.y;
            else
                force -= player.data.conservedMomentum * player.RB.velocity.y;

            for (int i = 0; i < player.data.doubleJumpDelay; i++)
            {
                yield return new WaitForFixedUpdate();
            }

            for (int i = 0; i < player.data.doubleJumpDuration; i++)
            {
                if (lastOnGroundTime > 0)
                {
                    player.status.ChangeCurrentMP(1);
                    Jump();
                    break;
                }

                player.RB.AddForce(Vector2.up * (force / player.data.doubleJumpDuration), ForceMode2D.Impulse);
                yield return new WaitForFixedUpdate();
            }

            _isDoubleJumping = false;
        }

        public void Dash()
        {
            if (_isDashing) // If dashing, subtract from energy and stop the currently running dash coroutine.
            {
                player.status.ChangeCurrentMP(-1);
                player.StopCoroutine(DashCoroutine());
            }

            player.RB.velocity = new Vector2(player.RB.velocity.x, 0);
            player.StartCoroutine(DashCoroutine());
        }

        public IEnumerator DashCoroutine()
        {
            _isDashing = true;

            float force = player.data.dashForce;

            float direction;
            if (player.data.isFacingRight) direction = 1.0f;
            else direction = -1.0f;

            for (int i = 0; i < player.data.dashForceDuration; i++)
            {
                player.RB.AddForce(direction * Vector2.right * (force / player.data.dashForceDuration), ForceMode2D.Impulse);
                yield return new WaitForFixedUpdate();
            }
            for (int i = 0; i < player.data.dashDuration - player.data.dashForceDuration; i++) // Follow-through
            {
                yield return new WaitForFixedUpdate();
            }

            _isDashing = false;
        }
    }
}
