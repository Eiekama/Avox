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

        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        public virtual void DealContactDamage(PlayerInstance player)
        {
            player.combat.Damage(_collider, atk);
        }

        protected virtual void Die()
        {
            gameObject.SetActive(false);
        }

        public virtual void Damage(Collider2D source, int dmg)
        {
            hp -= dmg;
            if (hp <= 0) { Die(); }
        }
    }
}
