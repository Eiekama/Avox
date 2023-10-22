using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class TransitionManager : MonoBehaviour
{
    public static int currentTransition = -1;
    
    public static bool _playerDirection;
    public static float _playerVertical;
    
    [Tooltip("The distance the player spawns from this entrance")]
    [SerializeField] private float _spawnDistance = 1.5f;
    [SerializeField] private float _verticalSpawnForce = 10f;
    private Vector2 _rightVerticalPush = new Vector2(30, 1);
    private Vector2 _leftVerticalPush = new Vector2(-30, 1);
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
                        _player.transform.position = st.transform.position + new Vector3(-_spawnDistance, _playerVertical * st.transform.localScale.y, 0);
                    }
                    else if (st.spawnLocation == SceneTransition.SpawnLocation.Right)
                    {
                        _player.transform.position = st.transform.position + new Vector3(_spawnDistance, _playerVertical * st.transform.localScale.y, 0);
                    }
                    else
                    {
                        _player.transform.position = st.transform.position + new Vector3(0, (st.transform.localScale.y / 2) + 1f, 0);
                        if (_playerDirection)
                        {
                            _player.RB.AddForce(_rightVerticalPush * _verticalSpawnForce, ForceMode2D.Impulse);
                        }
                        else
                        {
                            _player.RB.AddForce(_leftVerticalPush * _verticalSpawnForce, ForceMode2D.Impulse);  
                        }
                    }
                    currentTransition = -1;
                    break;
                }
            }
        }
    }
    
}
