using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectible : AInteractable
{
    new bool _isAuto = true;

    public override void Interact(PlayerInstance player)
    {
        gameObject.GetComponent<Animator>().SetBool("IsCollected",true);
        Destroy(gameObject);
    }
}
