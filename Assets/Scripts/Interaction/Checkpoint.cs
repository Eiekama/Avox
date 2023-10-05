using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : AInteractable
{
    public static Vector3 currentCheckpoint { get; private set; }
    public override void Interact(PlayerInstance player)
    {
        currentCheckpoint = transform.position;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(GetComponent<BoxCollider2D>().offset + (Vector2)transform.position, GetComponent<BoxCollider2D>().size);
    }
}
