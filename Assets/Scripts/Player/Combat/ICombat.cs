using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombat : IDamageable
{
    PlayerInstance player { get; set; }
    MeleeCollider meleeCollider { get; set; }

    void Attack(MonoBehaviour mono);

    // when we add combat skills those would go in here too
}