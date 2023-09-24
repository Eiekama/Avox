using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class Combat : ASystem, ICombat
    {
        public Animator _animator;

        public Transform _attackPoint;
        public LayerMask _enemyLayers;

        public float _attackRange = 0.5f;
        public int _attackDamage = 1;

        public float _attackRate = 2f;
        float nextAttackTime = 0f;

        public void Damage(int dmg)
        {
            // ADD IMPLEMENTATION HERE
        }

        public void Update()
        {
            if (Time.time >= nextAttackTime)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / _attackRate;
                }
            }

        }

        public void Attack()
        {
            _animator.SetTrigger("Attack");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<TestEnemy>().Damage(_attackDamage);
            }
        }
    }
}
