using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnInteract : AInteractable
{
    public override void Interact(PlayerInstance player)
    {
        Destroy(gameObject);
    }
}
