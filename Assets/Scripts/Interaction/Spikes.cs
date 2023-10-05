using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour, IContactDamage
{
    [SerializeField] int damage;
    
    public void DealContactDamage(PlayerInstance player)
    {
        player.combat.Damage(damage);
        //May need to check if this kills player and not teleport them in that case.

        Debug.Log("Respawning @ Most Recent Platforming Checkpoint");

        //Temporarily disable player actions
        StartCoroutine(WaitAndRespawn(player, player.pRespawnTime));
    }

    IEnumerator WaitAndRespawn(PlayerInstance player, float time)
    {
        player.controller.playerInputActions.Disable();
        yield return new WaitForSeconds(time);
        
        //Respawning; Resets Player location/velocity
        player.transform.position = player.currentCheckpoint;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        //Can reset angular velocity, too, and then call RigidBody2D.Sleep();, if need be
        
        player.controller.playerInputActions.Enable();
    }
}
