using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    public void ShowDialogue(DialogueObject dialogueObject, Vector3 pos, Vector3 offset)
    {
        if (dialogueObject._StopPlayer)
        {
            // add implementation to switch action maps from player to dialogue
        }
        _dialogueBox.SetActive(true);
        _textLabel.GetComponent<RectTransform>().anchoredPosition = pos + offset;
        StartCoroutine(Stepthrough(dialogueObject));
    }

    private IEnumerator Stepthrough(DialogueObject dialogueObject)
    {
        _textLabel.text = string.Empty;

        foreach (string dialogue in dialogueObject.Dialogue)
        {

            if (dialogueObject.Effect.ToString() == "fade")
            {
                _FadeEffect = GetComponent<FadeEffect>();
                yield return _FadeEffect.Run(dialogue, _textLabel);

            }
            else if (dialogueObject.Effect.ToString() == "typewriter")
            {
                _typewriterEffect = GetComponent<TypewriterEffect>();
                yield return _typewriterEffect.Run(dialogue, _textLabel);
            }
            else // none case
            {
                _textLabel.text = dialogue;
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
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

        if (dialogueObject._StopPlayer)
        {
            PlayerInstance _player = FindObjectOfType<PlayerInstance>();
            _player.controller.ToggleActionMap(_player.controller.inputActions.Player);
        }
    }

}
