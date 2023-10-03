using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour, IDamageable
{
    //public Animator animator;

    public int maxHealth = 1;
    int _currentHealth;

    void Start()
    {
        _currentHealth = maxHealth;

    }

    void Die()
    {
        Debug.Log("Enemy died!");

        //animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

    }

    public void Damage(int dmgTaken)
    {
        _currentHealth -= dmgTaken;

        //animator.SetTrigger("Hurt");

        if (_currentHealth <= 0)
        {
            Die();
        }
    }
}
