using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneTransition : AInteractable
{
    public enum SpawnLocation
    {
        Left,
        Right
    };

    [Tooltip("Pair with toSceneTransition" +
             "(-1 if there is no scene that leads to this)")]
    [SerializeField] public int index;
    
    [Tooltip("Which side the player lands")]
    [SerializeField] public SpawnLocation spawnLocation = new SpawnLocation();


    [Tooltip("Scene build index of the target scene")]
    [SerializeField] private int toScene;
    
    [Tooltip("Target scene transition index")]
    [SerializeField] private int toTransition;

 
    private Animator _transitionAnim;
    public Animator transitionAnim
    {
        get { return _transitionAnim; }
        set { if (_transitionAnim == null) _transitionAnim = value; }
    }

    [SerializeField] private float _transitionTime = 0.5f;


    public override void Interact(PlayerInstance player)
    {
        TransitionManager.currentTransition = toTransition;
        LoadNextScene(player.controller.playerInputActions);
    } 
    
    private void LoadNextScene(InputActions.PlayerActions playerInputActions)
    {
        StartCoroutine(LoadLevel(playerInputActions, toScene));
    }

    IEnumerator LoadLevel(InputActions.PlayerActions playerInputActions, int sceneIndex)
    {
        _transitionAnim.SetTrigger("Start");

        playerInputActions.Disable();
        yield return new WaitForSeconds(_transitionTime);

        // NOTE: can consider using Additive scene mode.
        // I think there's more flexibility for transition animations.
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
}
