//credit: https://gamedevbeginner.com/interfaces-in-unity/#interface_state_machine

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class AEnemy : MonoBehaviour, IDamageable, IContactDamage
    {
        [SerializeField] protected int hp;
        [SerializeField] protected int atk;

        public virtual void DealContactDamage(PlayerInstance player)
        {
            player.combat.Damage(atk);
        }

        public virtual void Damage(int dmg)
        {
            hp -= dmg;
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
