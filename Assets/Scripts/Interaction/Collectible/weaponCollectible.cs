using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponCollectible : Collectible
{    
    protected override void Collect(PlayerInstance player)
    {
        player.data.weapon = true;
        Destroy(gameObject);
    }
}
