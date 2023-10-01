using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : Enemy.AEnemy
{
    bool isCollected = false;
    int despawnTime = 5;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 5); // just an intial start direction
    }

    // Update is called once per frame
    // this could be removed and we can have the enemy who spawns the projectile remove the projectile
    void Update()
    {
        timer += Time.deltaTime;
        if ( (isCollected) || ((int)timer > despawnTime) )
        {
            Destroy(gameObject);
        }

    }

    new void DealContactDamage(PlayerInstance player)
    {
        base.DealContactDamage(player);
        isCollected = true;

    }
}
