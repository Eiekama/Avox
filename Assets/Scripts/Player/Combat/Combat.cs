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
    }
}
