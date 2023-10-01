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
    
    [SerializeField] private int toScene;
    [SerializeField] private int myScene;
    [SerializeField] public int fromScene;
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
