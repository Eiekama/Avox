using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class AEnemy : MonoBehaviour, IDamageable, IContactDamage
{
    [SerializeField] protected int hp;
    [SerializeField] protected int atk;

    protected virtual bool SeesPlayer() { return false; }

    protected virtual void Attack() { }

    public void DealContactDamage(PlayerInstance player)
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
