using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollider : MonoBehaviour
{
    [SerializeField] PlayerInstance player;
    [SerializeField] PlayerData _data;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            Debug.Log("Enemy takes 1 damage");
            damageable.Damage(1);
        }
    }

}
