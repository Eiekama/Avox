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
    private float _spawnDistance = 1.5f;
    private float _verticalSpawnForce = 10f;
    private static float _horizontalDistance = 30f;
    private Vector2 _rightVerticalPushUp = new Vector2(_horizontalDistance, 1);
    private Vector2 _leftVerticalPushUp = new Vector2(-_horizontalDistance, 1);

    private Vector2 _rightVerticalPushDown = new Vector2(_horizontalDistance, 0);
    private Vector2 _leftVerticalPushDown = new Vector2(-_horizontalDistance, 0);
    
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
                    else if (st.spawnLocation == SceneTransition.SpawnLocation.VerticalUp)
                    {
                        _player.transform.position = st.transform.position + new Vector3(0, (st.transform.localScale.y / 2) + (_player.transform.localScale.y / 2) + 0.05f, 0);
                        if (_playerDirection)
                        {
                            _player.RB.AddForce(_rightVerticalPushUp * _verticalSpawnForce, ForceMode2D.Impulse);
                        }
                        else
                        {
                            _player.RB.AddForce(_leftVerticalPushUp * _verticalSpawnForce, ForceMode2D.Impulse);  
                        }
                    }
                    else
                    {
                        _player.transform.position = st.transform.position + new Vector3(0, -(st.transform.localScale.y / 2) - (_player.transform.localScale.y / 2) - 0.05f, 0);
                        if (_playerDirection)
                        {
                            _player.RB.AddForce(_rightVerticalPushDown * _verticalSpawnForce, ForceMode2D.Impulse);
                        }
                        else
                        {
                            _player.RB.AddForce(_leftVerticalPushDown * _verticalSpawnForce, ForceMode2D.Impulse);  
                        }
                    }
                    currentTransition = -1;
                    break;
                }
            }
        }
    }
    
}
