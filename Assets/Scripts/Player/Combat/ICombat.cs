using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombat : IDamageable
{
    PlayerInstance player { get; set; }
    AttackHitbox attackHitbox { get; set; }

    void Attack(MonoBehaviour mono);
    
    void Die();

    IEnumerator WaitAndRespawn();

    // when we add combat skills those would go in here too
}