using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerInstance player;
    
    void Start()
    {
        Debug.Log("Manager Ran");
        if (SceneTransition.sceneHistory.Count > 1)
        {
            Debug.Log("If Valid1");
            Debug.Log(SceneTransition.all.Last().fromScene);
            foreach (SceneTransition st in SceneTransition.all)
            {
                Debug.Log(st.fromScene);
                if (st.fromScene == SceneTransition.sceneHistory.Last())
                {
                    Debug.Log("If Valid2");
                    if (st.spawnLocation == SceneTransition.SpawnLocation.Left)
                    {
                        player.transform.position = st.transform.position + new Vector3(-3, 0, 0);
                    }
                    else
                    {
                        player.transform.position = st.transform.position + new Vector3(3, 0, 0);
                    }
                    
                }
            }
        }
            
        
        
    }
    
}
