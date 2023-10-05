using UnityEngine;

[CreateAssetMenu(menuName = "DialogueObject")]
public class DialogueObject : ScriptableObject 
{
    [SerializeField] [TextArea] private string[] dialogue;

    public string[] Dialogue => dialogue;
}

