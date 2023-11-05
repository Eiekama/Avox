using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour, IContactDamage
{
    [SerializeField] int damage;

    public void DealContactDamage(PlayerInstance player)
    {
        player.combat.Damage(transform, damage);
        //May need to check if this kills player and not teleport them in that case.

        Debug.Log("Respawning @ Most Recent Platforming Checkpoint");

        //Temporarily disable player actions
        StartCoroutine(player.combat.WaitAndRespawn());
    }
}
