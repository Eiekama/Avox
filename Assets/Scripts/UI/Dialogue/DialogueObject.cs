using UnityEngine;

[CreateAssetMenu(menuName = "Avox/Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject 
{
    [SerializeField] [TextArea] private string[] dialogue;

    public string[] Dialogue => dialogue;
}

