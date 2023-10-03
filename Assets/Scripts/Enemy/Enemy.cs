//credit: https://gamedevbeginner.com/interfaces-in-unity/#interface_state_machine

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Collider2D))]
    public class Enemy : MonoBehaviour, IDamageable, IContactDamage
    {
        [SerializeField] protected int hp = 1;
        [SerializeField] protected int atk = 1;

        public virtual void DealContactDamage(PlayerInstance player)
        {
            player.combat.Damage(atk);
        }

        protected virtual void Die()
        {
            gameObject.SetActive(false);
        }

        public virtual void Damage(int dmg)
        {
            hp -= dmg;
            if (hp <= 0) { Die(); }
        }
    }
}
