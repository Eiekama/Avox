using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AmbienceStates : MonoBehaviour
{

    private Scene currScene;
    // Start is called before the first frame update
    void Start()
    {
        currScene = SceneManager.GetActiveScene();
        if (currScene.name == "Menu")
        {
            AudioManager.Instance.setNoneState();
            AudioManager.Instance.setMenuState();
        }
        else if(currScene.name == "Room 1")
        {
            AudioManager.Instance.setNoneState();
            AudioManager.Instance.setForestState();
        }
        else if (currScene.name == "Room 2")
        {
            AudioManager.Instance.setCalmState();
        }
        else if (currScene.name == "Room 4")
        {
            AudioManager.Instance.setCaveState();
        }
        else if (currScene.name == "Room 5")
        {
            AudioManager.Instance.setForestState();
        }
        else if (currScene.name == "Room 9")
        {
            AudioManager.Instance.setCaveState();
        }
        else if (currScene.name == "Room 10")
        {
            AudioManager.Instance.setBattleState();
            AudioManager.Instance.setForestState();
        }
    }
}
