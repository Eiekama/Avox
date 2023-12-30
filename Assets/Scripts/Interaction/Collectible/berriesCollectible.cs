using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class berriesCollectible : Collectible
{
    [SerializeField] GameObject _enableAfterCollect;

    protected override void Collect(PlayerInstance player)
    {
        player.status.ChangeMaxMP(1);
        if (_enableAfterCollect != null) { _enableAfterCollect.SetActive(true); }
        Destroy(gameObject);
    }
}
