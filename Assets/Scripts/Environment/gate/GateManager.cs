using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateManager : MonoBehaviour
{
    [SerializeField] GateData _data;
    public GateData data { get { return _data; } }

    LoweringGate[] _Gates;

    void Start()
    {
        int _currentScene = SceneManager.GetActiveScene().buildIndex;
        _Gates = GetComponentsInChildren<LoweringGate>();

        foreach (var Gate in _Gates)
        {
            for (int i = 0; i < _data.info.Length; i++)
            {
                if (_data.info[i].scene == _currentScene && _data.info[i].index == Gate.index)
                {
                    //if (_data.info[i].lowered) { Gate.gameObject.SetActive(false); }
                    //else { Gate.dataAndIndex = (_data, i); }
                    //break;
                    //uncomment above to get the data saving to work
                }
            }
        }
    }
}
