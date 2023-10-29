using UnityEngine;

[System.Serializable]
public class DialogueObject
{
    public enum _Effect
    {
        none = 0,
        typewriter = 1,
        fade = 2
    };

    [SerializeField] [TextArea] private string[] dialogue;
    public _Effect effect;
    [SerializeField] private bool _stopPlayer;
    public string[] Dialogue => dialogue;
    public bool _StopPlayer => _stopPlayer;
    public _Effect Effect =>effect;

}

