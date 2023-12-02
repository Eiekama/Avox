using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PrimeTween;

namespace Player
{
    public class Combat : ASystem, ICombat
    {
        public AttackHitbox attackHitbox { get; set; }

        private float _lastAttackTime;
        public float lastAttackTime
        {
            get { return _lastAttackTime; }
            set { _lastAttackTime = Mathf.Min(1.0f, value); }
        }
        private float _lastPressedAttackTime;
        public float lastPressedAttackTime
        {
            get { return _lastPressedAttackTime; }
            set { _lastPressedAttackTime = Mathf.Min(1.0f, value); }
        }

        private float _attackCooldown = 0.25f;
        public float attackCooldown => _attackCooldown;
        private float _bufferTime = 0.2f;
        
        public bool isInvincible = false;

        public static float respawnTime = 1f; //Respawn time for platforming checkpoints
        public static int deathRespawnScene = 1;
        public static float invincibilityTime = 10f;


        public void UpdateTimers()
        {
            lastAttackTime += Time.deltaTime;
            lastPressedAttackTime += Time.deltaTime;
        }

        public void Damage(Collider2D source, int dmg)
        {
            if(!isInvincible){
                player.status.ChangeCurrentHP(-dmg);
            }
            if (_player.data.currentHP == 0)
            {
                Die();
                return;
            }

            player.StartCoroutine(RunIFrames(invincibilityTime));

            Knockback(source);
            player.animator.SetTrigger("damage");
            // action map will be toggled as animation event
        }

        public void Knockback(Collider2D source)
        {
            var direction = source.ClosestPoint(player.transform.position) - (Vector2)player.transform.position;
            Vector2 force;
            if (direction.x < 0)
            {
                force = Vector2.right * 10 + 0.5f * player.data.jumpForce * Vector2.up;
            }
            else
            {
                force = Vector2.left * 10 + 0.5f * player.data.jumpForce * Vector2.up;
            }
            player.RB.velocity = Vector2.zero;
            player.RB.AddForce(force, ForceMode2D.Impulse);
        }

        IEnumerator RunIFrames(float duration){
            isInvincible = true;
            yield return new WaitForSeconds(duration);
            isInvincible = false;
        }

        private void Die(){
            player.animator.SetTrigger("die");
            // rest of behaviour defined through animation events
        }

        // used in animation event
        public void Respawn()
        {
            Sequence.Create()
                .Chain(Tween.Alpha(player.crossfade, startValue: 0, endValue: 1, duration: 0.5f))
                .ChainDelay(1.5f)
                .ChainCallback(() =>
                {
                    player.status.ChangeCurrentHP(player.data.maxHP);
                    SceneManager.LoadScene(deathRespawnScene, LoadSceneMode.Single);
                });
        }

        public bool CanAttack()
        {
            return player.data.hasWeapon && lastAttackTime > -0.2f;
        }

        public void Attack()
        {
            var _lookInput = player.controller.inputActions.Player.Look.ReadValue<float>();
            player.animator.SetInteger("yInput", Mathf.RoundToInt(_lookInput));
            if (lastPressedAttackTime < _bufferTime || lastAttackTime < 0) { player.animator.SetBool("buffered", true); }
            else { player.animator.SetBool("buffered", false); }
            player.animator.SetTrigger("attack");
            // for more reliable behaviour lastAttackTime is set using an animation event
            // this line is just to make sure we can't glitch a second attack in before the animation event calls
            player.combat.lastAttackTime = -2 * _attackCooldown;
        }
    }
}
