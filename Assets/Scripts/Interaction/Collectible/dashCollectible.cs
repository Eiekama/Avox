using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashCollectible : Collectible
{    
    protected override void Collect(PlayerInstance player)
    {
        player.data.hasDash = true;
        Destroy(gameObject);
    }
}
