using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptTrigger : AInteractable
{
    [SerializeField] DialogueObject _dialogueObject;
    [SerializeField] DialogueUI _dialogueUI;
    [SerializeField] private Vector2 offset;
    [SerializeField] bool startupDialogue;

    public void Start()
    {
        if (startupDialogue || InitialiseData.initialised)
            StartCoroutine(Activate());
    }

    IEnumerator Activate()
    {
        yield return new WaitForEndOfFrame();
        Interact(FindObjectOfType<PlayerInstance>());
    }

    public override void OnExit(PlayerInstance player)
    {
        base.OnExit(player);
        _dialogueUI.CloseDialogue(_dialogueObject);
        
    }

    public override void Interact(PlayerInstance player)
    {
        _dialogueUI.ShowDialogue(_dialogueObject, transform.position, (Vector3)offset);
        if (_dialogueObject._StopPlayer)
        {
            player.controller.ToggleActionMap(player.controller.inputActions.Dialogue);
        }
    }
}
