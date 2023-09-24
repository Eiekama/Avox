using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;

    private void Start() {
        textLabel.text = testDialogue.Dialogue.ToString();
        //ShowDialogue(testDialogue);
    }

    /**public void ShowDialogue(DialogueObject
    dialogueObject)
    {
        StartCoroutine(Dialoguerun(dialogueObject));
    }
    private IEnumerator Dialoguerun(DialogueObject
    dialogueObject)
    {
        foreach(string dialogue in dialogueObject.Dialogue);
        {
            yield return textLabel.text = dialogueObject.Dialogue.ToString();
        }
    }**/
}
