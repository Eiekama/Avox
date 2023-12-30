using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashCollectible : Collectible
{
    [SerializeField] GameObject _enableAfterCollect;

    protected override void Collect(PlayerInstance player)
    {
        player.data.hasDash = true;
        _enableAfterCollect.SetActive(true);
        Destroy(gameObject);
    }
}
