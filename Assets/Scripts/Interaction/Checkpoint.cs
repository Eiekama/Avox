using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : AInteractable
{
    public static Vector3 currentCheckpoint;
    public override void Interact(PlayerInstance player)
    {
        currentCheckpoint = transform.position;
    }

    // you can ignore this, its just to look fancy and help in debugging
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(GetComponent<BoxCollider2D>().offset + (Vector2)transform.position, GetComponent<BoxCollider2D>().size);
    }
}
