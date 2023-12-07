using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectible : AInteractable
{
    [SerializeField] int _index;
    public int index { get { return _index; } }

    (CollectibleData data, int i) _dataAndIndex;
    public PromptTrigger collectPrompt;
    public (CollectibleData data, int i) dataAndIndex
    {
        get { return _dataAndIndex; }
        set { if (_dataAndIndex.data == null) _dataAndIndex = value; }
    }

    protected virtual void Collect(PlayerInstance player)
    {
        gameObject.GetComponent<Animator>().SetBool("IsCollected", true);
        Destroy(gameObject);
    }

    public override void Interact(PlayerInstance player)
    {
        _dataAndIndex.data.info[_dataAndIndex.i].collected = true;
        Collect(player);
        if (collectPrompt != null)
            collectPrompt.Interact(player);
    }
}
