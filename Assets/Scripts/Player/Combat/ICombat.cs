using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombat : IDamageable
{
    PlayerInstance player { get; set; }
    AttackHitbox attackHitbox { get; set; }

    /// <summary>
    /// If positive, then we are allowed to attack again.
    /// </summary>
    float lastAttackTime { get; set; }
    float attackCooldown { get; }
    float lastPressedAttackTime { get; set; }

    void UpdateTimers();

    bool CanAttack();
    void Attack();
    
    void Respawn();
}