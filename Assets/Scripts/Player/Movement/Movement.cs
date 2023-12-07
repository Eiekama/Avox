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
        private bool _isDashing = false;
        private bool _isJumpCut;

        private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
        
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
            Vector2 terrainCheckPoint = (Vector2)player.transform.position + playerBoxCollider.offset - new Vector2(0.0f, playerBoxCollider.size.y * 0.1f);
            Vector2 groundCheckSize = playerBoxCollider.size + new Vector2(-playerBoxCollider.size.x * 0.1f, 0.0f);
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

            if ((!player.controller.inputActions.Player.enabled && moveInput == 0)
             || _isDashing) { return; }

            if (moveInput != 0)
            {
                UpdateDirectionToFace(moveInput > 0);
            }

            float _targetSpeed = moveInput * player.data.runMaxSpeed;

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

        private void UpdateDirectionToFace(bool isMovingRight)
        {
            if (isMovingRight != player.data.isFacingRight) { Turn(); }
        }

        private void Turn()
        {
            Vector3 scale = player.transform.localScale;
            scale.x *= -1;
            player.transform.localScale = scale;

            player.data.isFacingRight = !player.data.isFacingRight;
        }


        public bool CanJump()
        {
            return lastOnGroundTime > 0 && !_isDashing;
        }

        public void Jump()
        {
            player.animator.SetTrigger("jump");

            _isJumpCut = false;
            lastOnGroundTime = 0;
            player.RB.AddForce(Vector2.up * (player.data.jumpForce - player.RB.velocity.y), ForceMode2D.Impulse);
        }

        public void JumpCut()
        {
            if (!_isDashing) _isJumpCut = true;
        }

        public bool CanDoubleJump()
        {
            return lastOnGroundTime < 0 && !_isDoubleJumping && !_isDashing && player.data.currentMP > 0;
        }

        public void DoubleJump()
        {
            player.animator.SetTrigger("jump");

            _isJumpCut = false;
            player.status.ChangeCurrentMP(-1);
            player.StartCoroutine(DoubleJumpCoroutine());
        }

        IEnumerator DoubleJumpCoroutine()
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
                yield return waitForFixedUpdate;
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
                yield return waitForFixedUpdate;
            }

            _isDoubleJumping = false;
        }

        public bool CanDash()
        {
            return player.data.hasDash && !_isDashing && player.data.currentMP > 1;
        }

        private IEnumerator dashCoroutine;
        public void Dash()
        {
            dashCoroutine = DashCoroutine();
            _isJumpCut = false;
            player.animator.SetTrigger("dash");
            player.status.ChangeCurrentMP(-2);
            player.StartCoroutine(dashCoroutine);
        }

        public void StopDash()
        {
            if (_isDashing)
            {
                player.StopCoroutine(dashCoroutine);
                _isDashing = false;
            }
        }

        IEnumerator DashCoroutine()
        {
            _isDashing = true;

            float _moveInput = player.controller.inputActions.Player.Run.ReadValue<float>();
            float _lookInput = player.controller.inputActions.Player.Look.ReadValue<float>();
            if (_moveInput == 0 && _lookInput == 0) { _moveInput = player.data.isFacingRight ? 1.0f : -1.0f; }
            Vector2 _direction = new(_moveInput, _lookInput);

            Vector2 _speedDif;
            Vector2 _movement;

            Vector2 _targetSpeed = player.data.dashMaxSpeed * _direction.normalized;
            float _accelRate = player.data.dashAccelAmount;
            for (float i = 0f; i < player.data.dashDeccelPoint * player.data.dashTime; i += Time.fixedDeltaTime)
            {
                _speedDif = _targetSpeed - player.RB.velocity;
                _movement = _speedDif * _accelRate;
                player.RB.AddForce(_movement, ForceMode2D.Force);
                yield return waitForFixedUpdate;
            }

            _targetSpeed = Vector2.zero;
            _accelRate = player.data.dashDeccelAmount;
            for (float i = 0; i < (1 - player.data.dashDeccelPoint) * player.data.dashTime; i += Time.fixedDeltaTime)
            {
                _speedDif = _targetSpeed - player.RB.velocity;
                _movement = _speedDif * _accelRate;
                player.RB.AddForce(_movement, ForceMode2D.Force);
                yield return waitForFixedUpdate;
            }

            _isDashing = false;
        }
    }
}
