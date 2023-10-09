using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class Combat : ASystem, ICombat
    {
        // public Animator animator;

        // nextAttackTime = Time.time + 1f / _attackRate;

        // float _attackRate = 2f;
        // float _nextAttackTime = 0f;

        public AttackHitbox attackHitbox { get; set; }

        public void Damage(int dmg)
        {
            player.status.ChangeCurrentHP(-dmg);
        }

        public void Die(){
            Debug.Log("Player Died :(");
        }

        IEnumerator AttackCoroutine()
        {
            attackHitbox.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            attackHitbox.gameObject.SetActive(false);
        }

        public void Attack(MonoBehaviour mono)
        {
            //animator.SetTrigger("Attack");
            mono.StartCoroutine(AttackCoroutine());
        }

        
        //TODO - Zach: Fix w/e is going on here (I just copied the stuff from TransitionManager;)
        // how to get the animator reference in here (basically just copying the animator from 
        // TransitionManager (in the scene) for now.)
        private Animator _animator;
        private void Start() {
            _animator = GetComponentInChildren<Animator>(true);
            _animator.gameObject.SetActive(true);
        }

        IEnumerator WaitAndRespawn(PlayerInstance player, float time)
        {
            player.controller.playerInputActions.Disable(); //TODO: Also disable jumps
            // _fade.SetTrigger("Start");
            Debug.Log("Before timer");
            yield return new WaitForSeconds(time);
            Debug.Log("After timer");
            
            //Respawning; Resets Player location/velocity
            player.transform.position = Checkpoint.currentCheckpoint;
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            //Can reset angular velocity, too, and then call RigidBody2D.Sleep();, if need be
            
            player.controller.playerInputActions.Enable();
        }
    }
}
