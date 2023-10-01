using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerInstance player;
    [Tooltip("The distance the player spawns from this entrence")]
    [SerializeField] private float spawnDistance = 1.5f;
    
    void Start()
    {
        if (SceneTransition.sceneHistory.Count > 1)
        {
            foreach (SceneTransition st in SceneTransition.all)
            {
                if (st.fromScene == SceneTransition.sceneHistory.Last())
                {
                    if (st.spawnLocation == SceneTransition.SpawnLocation.Left)
                    {
                        player.transform.position = st.transform.position + new Vector3(-spawnDistance, 0, 0);
                    }
                    else
                    {
                        player.transform.position = st.transform.position + new Vector3(spawnDistance, 0, 0);
                    }
                    
                }
            }
        }
            
        
        
    }
    
}
