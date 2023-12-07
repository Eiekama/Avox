using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    

    public static int currentTransition = -1;
    
    public static float _playerVertical;
 
    private PlayerInstance _player;
    private float _playerWidthOffset;
    private float _playerHeightOffset;
    private float _epsilon = 0.05f;
    private SceneTransition[] _transitions;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerInstance>();
        var collider = _player.GetComponent<BoxCollider2D>();
        _playerWidthOffset = Mathf.Abs(collider.offset.x) + collider.size.x / 2;
        _playerHeightOffset = Mathf.Abs(collider.offset.y) + collider.size.y / 2;

        _transitions = GetComponentsInChildren<SceneTransition>();

        Animator _animator = GetComponentInChildren<Animator>(true);
        _animator.gameObject.SetActive(true);

        foreach (var st in _transitions)
        {
            st.transitionAnim = _animator;
        }
    }

    private void Start()
    {
        if (currentTransition > -1)
        {
            foreach (SceneTransition st in _transitions)
            {
                if (st.index == currentTransition)
                {
                    if (st.spawnLocation == SceneTransition.SpawnLocation.Left)
                    {
                        _player.transform.position = st.transform.position + new Vector3 (
                            -(st.transform.localScale.x / 2) - _playerWidthOffset - _epsilon,
                            _playerVertical * st.transform.localScale.y,
                            0 );
                        StartCoroutine(PlayHorizontalTransition(false));
                    }
                    else if (st.spawnLocation == SceneTransition.SpawnLocation.Right)
                    {
                        _player.transform.position = st.transform.position + new Vector3 (
                            (st.transform.localScale.x / 2) + _playerWidthOffset + _epsilon,
                            _playerVertical * st.transform.localScale.y,
                            0 );
                        StartCoroutine(PlayHorizontalTransition(true));
                    }
                    else if (st.spawnLocation == SceneTransition.SpawnLocation.Down)
                    {
                        _player.transform.position = st.transform.position + new Vector3 (
                            0,
                            -(st.transform.localScale.y / 2) - _playerHeightOffset - _epsilon,
                            0 );
                        StartCoroutine(PlayDownTransition());
                    }
                    else
                    {
                        _player.transform.position = st.transform.position + new Vector3 (
                            0,
                            (st.transform.localScale.y / 2) + _playerHeightOffset + _epsilon,
                            0 );
                        if (st.spawnLocation == SceneTransition.SpawnLocation.Up)
                        {
                            StartCoroutine(PlayUpTransition(_player.data.isFacingRight));
                        }
                        else if (st.spawnLocation == SceneTransition.SpawnLocation.UpRightOnly)
                        {
                            StartCoroutine(PlayUpTransition(true));
                        }
                        else
                        {
                            StartCoroutine(PlayUpTransition(false));
                        }
                    }
                    currentTransition = -1;
                    break;
                }
            }
        }

        IEnumerator PlayUpTransition(bool isFacingRight)
        {
            float input;
            if (isFacingRight) { input = 1.0f; }
            else { input = -1.0f; }

            _player.controller.DisableActionMap(_player.controller.inputActions.Player);
            _player.movement.lastOnGroundTime = 0.1f; // random code but i had to do this to fix an animation bug
            yield return null;
            _player.movement.Jump();
            yield return new WaitForSeconds(0.2f);
            while (_player.movement.lastOnGroundTime < 0)
            {
                _player.movement.Run(input);
                yield return new WaitForFixedUpdate();
            }
            _player.controller.ToggleActionMap(_player.controller.inputActions.Player);
        }

        IEnumerator PlayHorizontalTransition(bool isFacingRight)
        {
            float input;
            if (isFacingRight) { input = 1.0f; }
            else { input = -1.0f; }

            _player.controller.DisableActionMap(_player.controller.inputActions.Player);

            for (float t = 0.0f; t < 0.2f; t += Time.fixedDeltaTime)
            {
                _player.movement.Run(input);
                yield return new WaitForFixedUpdate();
            }

            _player.controller.ToggleActionMap(_player.controller.inputActions.Player);
        }
        IEnumerator PlayDownTransition()
        {
            _player.controller.DisableActionMap(_player.controller.inputActions.Player);
            yield return new WaitForSeconds(0.1f);
            float _time = 0.1f;
            while (_player.movement.lastOnGroundTime < 0 && _time < 2.0f)
            {
                _time += Time.deltaTime;
                yield return null;
            }
            _player.controller.ToggleActionMap(_player.controller.inputActions.Player);
        }
    }
    
}
