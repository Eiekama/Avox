using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollectible : Collectible
{
    [SerializeField] DialogueObject _dialogueObject;
    [SerializeField] DialogueUI _dialogueUI;
    [SerializeField] private Vector2 offset;
    protected override void Collect(PlayerInstance player)
    {
        gameObject.GetComponent<Animator>().SetBool("IsCollected", true);
        _dialogueUI.ShowDialogue(_dialogueObject, transform.position, (Vector3)offset);
        Destroy(gameObject);

    }
}
