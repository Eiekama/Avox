using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : AInteractable
{
    public static List<int> sceneHistory = new List<int>();
    public enum SpawnLocation
    {
        Left,
        Right
    };    
    [Tooltip("Scene build index of the target scene")]
    [SerializeField] private int toScene;
    
    [Tooltip("Target scene transition index")]
    [SerializeField] private int toSceneTransition;

    [Tooltip("Pair with toSceneTransition" +
             "(-1 if there is no scene that leads to this)")]
    [SerializeField] public int sceneTransitionIndex;
    
    [Tooltip("Which side the player lands")]
    [SerializeField] public SpawnLocation spawnLocation = new SpawnLocation();
    
    
    public Animator transition;
    private float _transitionTime = 1.5f;
    
    public override void Interact(PlayerInstance player)
    {
        sceneHistory.Add(toSceneTransition);
        LoadNextScene();

    } 
    
    private void LoadNextScene()
    {
        StartCoroutine(LoadLevel(toScene));
    }
    IEnumerator LoadLevel(int sceneIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(_transitionTime);

        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        
    }
}
