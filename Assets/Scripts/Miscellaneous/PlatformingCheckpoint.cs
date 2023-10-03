using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformingCheckpoint : AInteractable
{
    public override void Interact(PlayerInstance player)
    {
        player.currentCheckpoint = transform.position;
    }

}
