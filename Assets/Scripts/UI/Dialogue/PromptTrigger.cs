using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptTrigger : AInteractable
{
    [SerializeField] DialogueObject _dialogueObject;
    [SerializeField] DialogueUI _dialogueUI;

    public override void OnExit(PlayerInstance player)
    {
        base.OnExit(player);
        _dialogueUI.CloseDialogue(_dialogueObject);
        
    }

    public override void Interact(PlayerInstance player)
    {
        _dialogueUI.ShowDialogue(_dialogueObject);
    }
}
