using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] PlatformData _data;
    public PlatformData data { get { return _data; } }

    CrumblingPlatform[] _Platforms;

    void Start()
    {
        int _currentScene = SceneManager.GetActiveScene().buildIndex;
        _Platforms = GetComponentsInChildren<CrumblingPlatform>();

        foreach (var Platform in _Platforms)
        {
            for (int i = 0; i < _data.info.Length; i++)
            {
                if (_data.info[i].scene == _currentScene && _data.info[i].index == Platform.index)
                {
                    //if (_data.info[i].collapsed) { Platform.gameObject.SetActive(false); }
                    { Platform.dataAndIndex = (_data, i); }
                    break;
                }
            }
        }
    }
}
