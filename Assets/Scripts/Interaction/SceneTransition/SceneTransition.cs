using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : AInteractable
{
    [SerializeField] int sceneBuildIndex;
    [SerializeField] private Vector3 startPosition;
    private int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    public Animator transition;
    public float _transitionTime = 1.5f;

    private Dictionary<int, int[]> transitionDictionary = new Dictionary<int, int[]>()
    {
        {0, new int[] {1, 2}}
    };
    
    public override void Interact(PlayerInstance player)
    { 
;        LoadNextScene();
        startPosition = player.transform.position;
        
    }
    private void LoadNextScene()
    {
        StartCoroutine(LoadLevel(sceneBuildIndex));
    }
    IEnumerator LoadLevel(int sceneIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(_transitionTime);

        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        
        
    }
    

    
    
}
