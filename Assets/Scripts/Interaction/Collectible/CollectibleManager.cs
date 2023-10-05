using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectibleManager : MonoBehaviour
{
    [SerializeField] CollectibleData _data;
    public CollectibleData data { get { return _data; } }

    Collectible[] _collectibles;

    void Start()
    {
        int _currentScene = SceneManager.GetActiveScene().buildIndex;
        _collectibles = GetComponentsInChildren<Collectible>();

        foreach (var collectible in _collectibles)
        {
            for (int i = 0; i < _data.info.Length; i++)
            {
                if (_data.info[i].scene == _currentScene && _data.info[i].index == collectible.index)
                {
                    if (_data.info[i].collected) { collectible.gameObject.SetActive(false); }
                    else { collectible.dataAndIndex = (_data, i); }
                    break;
                }
            }
        }
    }
}
