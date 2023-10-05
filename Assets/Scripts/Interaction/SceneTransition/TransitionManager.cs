using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public static int currentTransition = -1;

    [Tooltip("The distance the player spawns from this entrance")]
    [SerializeField] private float _spawnDistance = 1.5f;

    private PlayerInstance _player;
    private SceneTransition[] _transitions;
    
    
    void Start()
    {
        _player = FindObjectOfType<PlayerInstance>();
        _transitions = GetComponentsInChildren<SceneTransition>();

        Animator _animator = GetComponentInChildren<Animator>(true);
        _animator.gameObject.SetActive(true);

        foreach (var st in _transitions)
        {
            st.transitionAnim = _animator;
        }

        if (currentTransition > -1)
        {
            foreach (SceneTransition st in _transitions)
            {
                if (st.index == currentTransition)
                {
                    // NOTE: can try to preserve height at which player entered the transition.
                    if (st.spawnLocation == SceneTransition.SpawnLocation.Left)
                    {
                        _player.transform.position = st.transform.position + new Vector3(-_spawnDistance, 0, 0);
                    }
                    else
                    {
                        _player.transform.position = st.transform.position + new Vector3(_spawnDistance, 0, 0);
                    }
                    currentTransition = -1;
                    break;
                }
            }
        }
    }
    
}
