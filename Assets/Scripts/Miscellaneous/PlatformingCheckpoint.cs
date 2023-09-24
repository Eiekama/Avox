using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformingCheckpoint : AInteractable
{
    public bool triggered;
    // new bool _isAuto = true; //?


    private void Start() {
        triggered = false;
    }

    public override void Interact(PlayerInstance player)
    {
        Debug.Log(this);
        // Debug.Log(GetComponent<Transform>());
        player.currentPCheckpoint = transform.position;
        triggered = true;
    }

}
