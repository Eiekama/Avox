using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject textDialogue;
    [SerializeField] public bool stopPlayer;

    public bool IsOpen {get; private set; }
    private TypewriterEffect typewriterEffect;

    private void Awake() {
        typewriterEffect = GetComponent<TypewriterEffect>();
        CloseDialogue();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(Stepthrough(dialogueObject));
    }
    private IEnumerator Stepthrough(DialogueObject dialogueObject)
    {
        textLabel.text = string.Empty;
        foreach (string dialogue in dialogueObject.Dialogue)
        {
            yield return typewriterEffect.Run(dialogue, textLabel);
            //yield return textLabel.text = dialogue; //if you dont want the typewriter effect use this line
            yield return new WaitUntil(()=> Input.GetKeyDown(KeyCode.Space));
        }
        CloseDialogue();
    }

    public void CloseDialogue()
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }

}
