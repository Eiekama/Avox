using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponCollectible : Collectible
{
    [SerializeField] GameObject _enableAfterCollect;
    protected override void Collect(PlayerInstance player)
    {
        player.data.hasWeapon = true;
        _enableAfterCollect.SetActive(true);
        Destroy(gameObject);
    }
}
