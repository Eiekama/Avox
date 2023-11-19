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

        public static float respawnTime = 1f; //Respawn time for platforming checkpoints
        public static int deathRespawnScene = 1;

        
        public Vector3 moveDirection = new Vector3(-500, 3f, 0); //Used for knockback, hardcoded for now

        public void UpdateTimers()
        {
            lastAttackTime += Time.deltaTime;
            lastPressedAttackTime += Time.deltaTime;
        }

        public void Damage(Transform _, int dmg)
        {
            player.status.ChangeCurrentHP(-dmg);
            player.RB.AddForce(moveDirection, ForceMode2D.Impulse); //Knockaback
        }

        public void Die(){
            Debug.Log("Player Died :(");
            player.StartCoroutine(DieCoroutine());
        }
        IEnumerator DieCoroutine()
        {
            //Not finished:
            Animator anim = player.RespawnAnimator;
            anim.SetTrigger("Start");


            // player.controller.playerInputActions.Disable();
            yield return new WaitForSeconds(respawnTime/2);
            player.status.ChangeCurrentHP(player.data.maxHP);
            SceneManager.LoadScene(deathRespawnScene, LoadSceneMode.Single); 
        }

        public bool CanAttack()
        {
            return lastAttackTime > -0.2f;
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


        public IEnumerator WaitAndRespawn()
        {
            Animator anim = player.RespawnAnimator;
            player.controller.DisableActionMap(player.controller.inputActions.Player);

            anim.gameObject.SetActive(true);
            anim.SetTrigger("Start");
            yield return new WaitForSeconds(respawnTime/2);

            //Respawning; Resets Player location/velocity
            player.transform.position = Checkpoint.currentCheckpoint;
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            yield return new WaitForSeconds(respawnTime/2);

            anim.SetTrigger("FadeIn");

            //Can reset angular velocity, too, and then call RigidBody2D.Sleep();, if need be

            player.controller.ToggleActionMap(player.controller.inputActions.Player);
        }
    }
}
