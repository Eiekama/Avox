using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, testinteract
{
    [SerializeField] private DialogueObject dialogueObject;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player")&& other.TryGetComponent(out testplayer player))
        {
            player.Interactable = this;
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player")&& other.TryGetComponent(out testplayer player))
        {
            if(player.Interactable is DialogueActivator dialogueActivator && dialogueActivator ==this)
            {
                player.Interactable = null;
            }
        }
    }

    public void Interact(testplayer player)
    {
        player.DialogueUI.ShowDialogue(dialogueObject);
    }

}
