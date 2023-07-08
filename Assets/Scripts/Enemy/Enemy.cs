using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected int hp;

    public void Damage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            //play dying animation then destroy itself
        }
    }

    protected virtual bool SeesPlayer() { return false; }

    protected virtual void Attack() { }

    protected void DealContactDamage(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //deal damage to player
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        DealContactDamage(other);
    }
}
