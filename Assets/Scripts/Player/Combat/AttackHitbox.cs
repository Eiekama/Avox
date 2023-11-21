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

    private BoxCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.Damage(_collider, _data.atk); ;
        }
    }
}
