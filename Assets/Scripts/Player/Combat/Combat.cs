using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class Combat : ASystem, ICombat
    {
        // public Animator animator;

        public AttackHitbox attackHitbox { get; set; }

        public void Damage(int dmg)
        {
            player.status.ChangeCurrentHP(-dmg);
        }

        IEnumerator AttackCoroutine(Vector3 position) // Vector3 position
        {
            //Debug.Log(attackHitbox.gameObject.transform.localPosition);

            attackHitbox.gameObject.transform.localPosition = position;

            //Debug.Log(attackHitbox.gameObject.transform.localPosition);

            attackHitbox.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.0f); //should be length of animation
            attackHitbox.gameObject.SetActive(false);
        }

        public void Attack(MonoBehaviour mono)
        {
            Debug.Log(" attack");
            //attackHitbox.gameObject.transform.localPosition = new Vector3(2.0f, 0.0f, 0.0f);

            //if (player.controller.inputActions.Player.Look.ReadValue<int>() == 1)
            Vector3 leftright_position = new Vector3(2.0f, 0.0f, 0.0f);

            if (attackHitbox.gameObject.activeInHierarchy == false)
            {
                //animator.SetTrigger("Attack");

                //mono.StartCoroutine(AttackCoroutine());
                mono.StartCoroutine(AttackCoroutine(leftright_position));
            }

        }
        public void DownAttack(MonoBehaviour mono)
        {
            Debug.Log("down attack");
            //attackHitbox.gameObject.transform.localPosition = new Vector3(0.0f, -2.0f, 0.0f);


            if (attackHitbox.gameObject.activeInHierarchy == false)
            {
                //animator.SetTrigger("Attack");

                Debug.Log("down attack");

                Vector3 down_position = new Vector3(0.0f, -2.0f, 0.0f);

                //ono.StartCoroutine(AttackCoroutine());
                mono.StartCoroutine(AttackCoroutine(down_position));
            }
        }

        public void UpAttack(MonoBehaviour mono)
        {
            Debug.Log("up attack");
            //attackHitbox.gameObject.transform.localPosition = new Vector3(0.0f, 2.0f, 0.0f);


            if (attackHitbox.gameObject.activeInHierarchy == false)
            {
                //animator.SetTrigger("Attack");

                Debug.Log("up attack 2");

                Vector3 up_position = new Vector3(0.0f, 2.0f, 0.0f);

                //mono.StartCoroutine(AttackCoroutine());
                mono.StartCoroutine(AttackCoroutine(up_position));
            }
            
        }
    }
}

  
