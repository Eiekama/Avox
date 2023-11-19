using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class berriesCollectible : Collectible
{    
    protected override void Collect(PlayerInstance player)
    {
        player.status.ChangeMaxMP(1);
        Destroy(gameObject);
    }
}
