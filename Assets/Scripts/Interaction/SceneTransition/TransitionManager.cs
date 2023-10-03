using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public PlayerInstance player;
    [Tooltip("The distance the player spawns from this entrance")]
    [SerializeField] private float spawnDistance = 1.5f;

    private SceneTransition[] all;
    
    
    void Start()
    {
        all = GetComponentsInChildren<SceneTransition>();
        if (SceneTransition.sceneHistory.Count > 1)
        {
            foreach (SceneTransition st in all)
            {
                if (st.sceneTransitionIndex == SceneTransition.sceneHistory.Last())
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
