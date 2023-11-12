using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombat : IDamageable
{
    PlayerInstance player { get; set; }
    AttackHitbox attackHitbox { get; set; }

    void Attack();
    
    void Die();

    IEnumerator WaitAndRespawn();

    void DownAttack(MonoBehaviour mono);

    void UpAttack(MonoBehaviour mono);
    // when we add combat skills those would go in here too
}