using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class AttackHitbox : MonoBehaviour
{
    private PlayerData _data;
    public PlayerData data
    {
        get { return _data; }
        set { if (_data == null) _data = value; }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.Damage(_data.atk);
        }
    }


    private void OnDrawGizmos()
    {
         Gizmos.DrawWireCube(GetComponent<BoxCollider2D>().offset + (Vector2)transform.position, GetComponent<BoxCollider2D>().size);
    }
}
