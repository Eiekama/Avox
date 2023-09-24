using System.Collections;
using UnityEngine;
using DialogueObject
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;

    private void Start() {
        //textLabel.text = dialogueObject;
        ShowDialogue(testDialogue);
    }

    public void ShowDialogue(DialogueObject
    dialogueObject)
    {
        StartCoroutine(Dialoguerun(dialogueObject));
    }
    private IEnumerator Dialoguerun(DialogueObject
    dialogueObject)
    {
        foreach(string dialogue in dialogueObject.Dialogue);
        {
            return textLabel.text = dialogueObject;
        }
    }
}
