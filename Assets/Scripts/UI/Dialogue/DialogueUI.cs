using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    //change this so that you only need one canvas per type of text.
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private TMP_Text _textLabel;

    private TypewriterEffect _typewriterEffect;
    private FadeEffect _FadeEffect;

    private void Awake()
    {
        _dialogueBox.SetActive(false);
        _textLabel.text = string.Empty;
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        if (dialogueObject._StopPlayer)
        {
            // add implementation to switch action maps from player to dialogue
        }
        _dialogueBox.SetActive(true);
        if(dialogueObject.Effect.ToString() == "none")
        {
            _textLabel.text = dialogueObject.Dialogue[0];
        }
        else
        {
            StartCoroutine(Stepthrough(dialogueObject));
        }
    }
    private IEnumerator Stepthrough(DialogueObject dialogueObject)
    {
        _textLabel.text = string.Empty;
        if(dialogueObject.Effect.ToString() == "typewriter")
        {
        foreach (string dialogue in dialogueObject.Dialogue)
        {
            _typewriterEffect = GetComponent<TypewriterEffect>();
            yield return _typewriterEffect.Run(dialogue, _textLabel);
            yield return new WaitUntil(()=> Input.GetKeyDown(KeyCode.Space));
        }
        }
        else if(dialogueObject.Effect.ToString() == "fade")
        {
            foreach (string dialogue in dialogueObject.Dialogue)
            {
                _FadeEffect = GetComponent<FadeEffect>();
                yield return _FadeEffect.Run(dialogue, _textLabel);
                yield return new WaitUntil(()=> Input.GetKeyDown(KeyCode.Space));
            }

        }

        CloseDialogue(dialogueObject);
    }

    public void CloseDialogue(DialogueObject dialogueObject)
    {
        if (dialogueObject._StopPlayer)
        {
            // add implementation to switch action maps from player to dialogue
        }
        if(dialogueObject.Effect.ToString() == "fade")
        {
            foreach (string dialogue in dialogueObject.Dialogue)
            {
            _FadeEffect = GetComponent<FadeEffect>();
            _FadeEffect.Run2(dialogue, _textLabel);
            }
        }else{
        _dialogueBox.SetActive(false);
        _textLabel.text = string.Empty;
        }

    }

}
