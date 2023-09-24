using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour, IDamageable
{
    public Animator animator;

    public int _maxHealth = 1;
    int currentHealth;

    void Start()
    {
        currentHealth = _maxHealth;

    }


    void Die()
    {
        Debug.Log("Enemy died!");

        animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

    }

    public void Damage(int dmgTaken)
    {
        currentHealth -= dmgTaken;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
