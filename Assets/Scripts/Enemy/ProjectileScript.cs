using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour, IDamageable, IContactDamage
{
    // still using IDamageable, IContactDamage classes to have interactions with the player
    public int atk = 1;
    public Vector2 velocity = new Vector2(5, 5); // just an intial start direction
    int despawnTime = 5;


    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
        StartCoroutine(Despawn());
        
    }

    void AfterEffects()
    {
        //for special projectiles, can do something once it hits the ground or player
        Destroy(gameObject);
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(despawnTime);
        AfterEffects();
    }

    public void DealContactDamage(PlayerInstance player)
    {
        player.combat.Damage(GetComponent<Collider2D>(), atk); // I think damage isn't implemented yet, so I don't think we will be able to see the results right now
        AfterEffects(); //could also just disappear? 

    }

    public void Damage(Collider2D _, int damage)
    {
        // So the player can just block/hit the projectile
        Destroy(gameObject);
    }

}
