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
        public static int deathRespawnScene = 1;

        public AttackHitbox attackHitbox { get; set; }

        private Vector3 front = new Vector3(2.0f, 0.0f, 0.0f);

        private Vector3 up = new Vector3(0.0f, 2.0f, 0.0f);

        private Vector3 down = new Vector3(0.0f, -2.0f, 0.0f);

        public void Damage(Transform _, int dmg)
        {
            player.status.ChangeCurrentHP(-dmg);
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

        IEnumerator AttackCoroutine(Vector3 position)
        {
            attackHitbox.gameObject.transform.localPosition = position;
            attackHitbox.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            attackHitbox.gameObject.SetActive(false);
        }

        public void Attack()
        {
            Debug.Log(player.controller.inputActions.Player.Look.ReadValue<int>());

            if (player.controller.inputActions.Player.Look.ReadValue<int>() == 0)
            {
                //position = new Vector3 (2.0f, 0.0f, 0.0f);
                player.StartCoroutine(AttackCoroutine(front));
            }
            else if (player.controller.inputActions.Player.Look.ReadValue<int>() == 1)
            {
                //position = new Vector3 (0.0f, 2.0f, 0.0f);
                player.StartCoroutine(AttackCoroutine(up));
            }
            else if (player.controller.inputActions.Player.Look.ReadValue<int>() == -1)
            {
                //position = new Vector3(0.0f, -2.0f, 0.0f);
                player.StartCoroutine(AttackCoroutine(down));
            }
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
