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

        public static float respawnTime = 2f; //Respawn time for death checkpoints

        public AttackHitbox attackHitbox { get; set; }

        public void Damage(int dmg)
        {
            player.status.ChangeCurrentHP(-dmg);
        }

        public void Die(){
            Debug.Log("Player Died :(");
            player.status.ChangeCurrentHP(player.data.maxHP);
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

        
        public IEnumerator WaitAndRespawn()
        {
            Animator anim = player.RespawnAnimator;
            player.controller.playerInputActions.Disable(); //TODO: Also disable jumps

            Debug.Log("Before timer");
            anim.SetTrigger("Start");
            yield return new WaitForSeconds(respawnTime);
            Debug.Log("After timer");
            
            //Respawning; Resets Player location/velocity
            player.transform.position = Checkpoint.currentCheckpoint;
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            //Can reset angular velocity, too, and then call RigidBody2D.Sleep();, if need be
            
            player.controller.playerInputActions.Enable();
        }
        //Death respawn: Mostly a scene transition, heal to full, 
        //Anim manager w/ animator(s) in it which the function references; basically just can copy what SceneTransition did
        //Try and like post progress on discord if I can't come to the meeting.
        //Post any questions/progress in the discord
        //TODO: Stop other things from happening in the scene here, too?
        //-> Same q for SceneTransitions.
    }
}
