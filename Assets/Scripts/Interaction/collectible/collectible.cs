using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectible : AInteractable
{
    public override void Interact(PlayerInstance player)
    {
        gameObject.GetComponent<Animator>().SetBool("IsCollected",true);
        Destroy(gameObject);
    }
}
