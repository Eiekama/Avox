using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class Combat : ASystem, ICombat
    {
        // public Animator animator;

        // nextAttackTime = Time.time + 1f / _attackRate;

        // float _attackRate = 2f;
        // float _nextAttackTime = 0f;

        public static float respawnTime = 1f; //Respawn time for platforming checkpoints

        public AttackHitbox attackHitbox { get; set; }

        public void Damage(Transform _, int dmg)
        {
            player.status.ChangeCurrentHP(-dmg);
        }

        public void Die(MonoBehaviour mono){
            Debug.Log("Player Died :(");
            player.status.ChangeCurrentHP(player.data.maxHP);
            mono.StartCoroutine(DieCoroutine());
        }
        IEnumerator DieCoroutine()
        {
            //Not finished:
            // int respawnScene = 16;
            // Animator anim = player.RespawnAnimator;
            // anim.SetTrigger("Start");

            // player.controller.playerInputActions.Disable();
            yield return new WaitForSeconds(respawnTime);

            // SceneManager.LoadScene(respawnScene, LoadSceneMode.Single);
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
        //Death respawn: Mostly a scene transition, heal to full, 
        //Anim manager w/ animator(s) in it which the function references; basically just can copy what SceneTransition did
        //TODO: Stop other things from happening in the scene here, too?
        //-> Same q for SceneTransitions.
        //If we reload the scene, that would fix the moving problem & black screen but yeah
        //Direction player is facing?
        //Death: RN need to -Go to the right scene
        //                  -Play the animation
        //                  -Make the coroutine work well w/ MonoBehaviour (check if things have been updated in ASystem to incl. the MB)
    }
}
