using UnityEngine;

[CreateAssetMenu(menuName = "DialogueObject")]


public class DialogueObject : ScriptableObject 
{
public enum _Effect
{
none,
typewriter,
fade

};
    [SerializeField] [TextArea] private string[] dialogue;
    public _Effect effect;
    [SerializeField] private bool _stopPlayer;
    public string[] Dialogue => dialogue;
    public bool _StopPlayer => _stopPlayer;
    public _Effect Effect =>effect;

}

