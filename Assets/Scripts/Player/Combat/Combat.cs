using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class Combat : ASystem, ICombat
    {
        public Animator animator;
        public LayerMask enemyLayers;

        public float attackRange = 0.5f;
        public int attackDamage = 1;
        // nextAttackTime = Time.time + 1f / _attackRate;

        public float attackRate = 2f;
        //float _nextAttackTime = 0f;

        public MeleeCollider meleeCollider { get; set; }

        public void Damage(int dmg)
        {
            player.status.ChangeCurrentHP(-dmg);
        }

        IEnumerator AttackCoroutine()
        {
            meleeCollider.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            meleeCollider.gameObject.SetActive(false);
        }

        public void Attack(MonoBehaviour mono)
        {

            //animator.SetTrigger("Attack");
            Debug.Log("Attack");
            mono.StartCoroutine(AttackCoroutine());
            //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            //foreach (Collider2D enemy in hitEnemies)
            //{
            //    enemy.GetComponent<TestEnemy>().Damage(attackDamage);
            //}
        }
    }
}
