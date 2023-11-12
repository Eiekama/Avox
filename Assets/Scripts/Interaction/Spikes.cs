using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour, IContactDamage
{
    [SerializeField] int damage;
    [SerializeField] bool respawns;
    

    public void DealContactDamage(PlayerInstance player)
    {
        bool isKilled = player.data.currentHP <= damage;
        player.combat.Damage(transform, damage);
        //May need to check if this kills player and not teleport them in that case.

        Debug.Log("Respawning @ Most Recent Platforming Checkpoint");

        //Animation, Temporarily disable player actions
        if(respawns && !isKilled){
            StartCoroutine(player.combat.WaitAndRespawn());
        }
    }
}
