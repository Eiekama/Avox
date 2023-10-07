using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    //change this so that you only need one canvas per type of text.
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private TMP_Text _textLabel;
    [SerializeField] private bool _stopPlayer;

    private TypewriterEffect _typewriterEffect;

    private void Awake()
    {
        _typewriterEffect = GetComponent<TypewriterEffect>();
        CloseDialogue();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        if (_stopPlayer)
        {
            // add implementation to switch action maps from player to dialogue
        }
        _dialogueBox.SetActive(true);
        _textLabel.text = dialogueObject.Dialogue[0];
        //StartCoroutine(Stepthrough(dialogueObject));
    }
    private IEnumerator Stepthrough(DialogueObject dialogueObject)
    {
        _textLabel.text = string.Empty;
        foreach (string dialogue in dialogueObject.Dialogue)
        {
            yield return _typewriterEffect.Run(dialogue, _textLabel);
            //yield return textLabel.text = dialogue; //if you dont want the typewriter effect use this line
            yield return new WaitUntil(()=> Input.GetKeyDown(KeyCode.Space));
        }
        CloseDialogue();
    }

    public void CloseDialogue()
    {
        if (_stopPlayer)
        {
            // add implementation to switch action map back to player
        }
        _dialogueBox.SetActive(false);
        _textLabel.text = string.Empty;
    }

}
