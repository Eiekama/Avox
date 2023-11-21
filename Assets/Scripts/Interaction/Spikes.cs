using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Spikes : MonoBehaviour, IContactDamage
{
    [SerializeField] int damage;
    [SerializeField] bool respawns;

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }


    public void DealContactDamage(PlayerInstance player)
    {
        if (player.data.currentHP <= damage)
        {
            player.combat.Damage(_collider, damage); // since dying is taken care of in normal damage function
        }
        else
        {
            DealSpikeDamage(player, damage, respawns);
            if (respawns)
            {
                Debug.Log("Respawning @ Most Recent Platforming Checkpoint");
                WaitAndRespawn(player);
            }
        }
    }

    private void DealSpikeDamage(PlayerInstance player, int dmg, bool respawns)
    {
        player.status.ChangeCurrentHP(-dmg);
        if (respawns) { player.animator.SetTrigger("respawnDamage"); }
        else { player.animator.SetTrigger("damage"); }

    }

    private void WaitAndRespawn(PlayerInstance player)
    {
        Sequence.Create()
            .ChainDelay(0.2f)
            .Chain(Tween.Alpha(player.crossfade, startValue: 0, endValue: 1, duration: 0.5f))
            .ChainCallback(() =>
            {
                player.transform.position = Checkpoint.currentCheckpoint;
                player.RB.isKinematic = false;
            })
            .ChainDelay(0.5f)
            .Chain(Tween.Alpha(player.crossfade, startValue: 1, endValue: 0, duration: 0.5f))
            .ChainDelay(0.2f)
            .ChainCallback(() => player.controller.ToggleActionMap(player.controller.inputActions.Player));
    }
}
