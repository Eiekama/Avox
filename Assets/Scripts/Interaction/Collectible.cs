using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : AInteractable
{
    public override void Interact(PlayerInstance player)
    {
        gameObject.GetComponent<Animator>().SetBool("IsCollected",true);
        Destroy(gameObject);
    }
}
