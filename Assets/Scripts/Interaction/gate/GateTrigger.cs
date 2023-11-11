using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : AInteractable
{
        [SerializeField] LoweringGate _gate;
    public override void Interact(PlayerInstance player)
    {
        _gate.Lower();
    }
}
