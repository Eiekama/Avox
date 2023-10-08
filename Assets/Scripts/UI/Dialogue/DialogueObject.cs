using UnityEngine;

[CreateAssetMenu(menuName = "DialogueObject")]
public class DialogueObject : ScriptableObject 
{
    [SerializeField] [TextArea] private string[] dialogue;

    public string[] Dialogue => dialogue;

    // move stop player to here. As well as a way to set what effect to use
}

