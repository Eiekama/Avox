using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : AInteractable
{
    public static List<SceneTransition> all = new List<SceneTransition>();
    public static List<int> sceneHistory = new List<int>();
    public enum SpawnLocation
    {
        Left,
        Right
    };    
    [Tooltip("Scene build index of the target scene")]
    [SerializeField] private int toScene;
    
    [Tooltip("Scene build index of the current scene")]
    [SerializeField] private int myScene;
    
    [Tooltip("Scene build index of the previous scene")]
    [SerializeField] public int fromScene;
    
    [Tooltip("Which side the player lands")]
    [SerializeField] public SpawnLocation spawnLocation = new SpawnLocation();
    
    
    public Animator transition;
    public float _transitionTime = 1.5f;
    
    

    private void Awake()
    {
        myScene = SceneManager.GetActiveScene().buildIndex;
        all.Add(this);
        
    }

    private void OnDisable()
    {
        all.Remove(this);
    }

    public override void Interact(PlayerInstance player)
    {
        sceneHistory.Add(myScene);
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
