using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectible : AInteractable
{
    new bool _isAuto = true;

    public override void Interact(PlayerInstance player)
    {
        Destroy(gameObject);
    }
}
