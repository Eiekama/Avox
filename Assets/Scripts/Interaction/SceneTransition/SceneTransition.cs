using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : AInteractable
{
    public enum SpawnLocation
    {
        Left,
        Right,
        Up,
        UpRightOnly,
        UpLeftOnly,
        Down
    };


    
    // NOTE: I still think it might be better to collect all these information from
    // all scenes and store them in a single ScriptableObject instead.
    // It will be easier to organise this way if the project gets big.
    [Tooltip("Pair with toSceneTransition" +
             "(-1 if there is no scene that leads to this)")]
    [SerializeField] private int _index;
    public int index { get { return _index; } }
    
    [Tooltip("Which side the player lands")]
    [SerializeField] private SpawnLocation _spawnLocation = new SpawnLocation();
    public SpawnLocation spawnLocation { get { return _spawnLocation; } }


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
        TransitionManager._playerVertical = (player.transform.position.y - transform.position.y) / transform.localScale.y;
        
        //Debug.Log(TransitionManager._playerVertical);
        
        TransitionManager.currentTransition = toTransition;
        StartCoroutine(LoadScene(player, toScene));
    } 
    
    IEnumerator LoadScene(PlayerInstance player, int sceneIndex)
    {
        _transitionAnim.SetTrigger("Start");

        player.controller.DisableActionMap(player.controller.inputActions.Player);
        player.RB.isKinematic = true;
        yield return new WaitForSeconds(_transitionTime);

        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
}
