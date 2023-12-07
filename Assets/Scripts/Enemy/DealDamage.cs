using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
using Player;

public class DealDamage : MonoBehaviour
{
    Enemy.Enemy enemy;

    void Awake()
    {
        enemy = this.transform.parent.gameObject.GetComponent<Enemy.Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Damage");
            enemy.DealContactDamage(collision.gameObject.GetComponent<PlayerInstance>());
        }
    }
}
