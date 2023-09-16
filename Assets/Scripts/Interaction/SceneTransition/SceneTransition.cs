using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : AInteractable
{
    [SerializeField] int sceneBuildIndex;
    public Animator transition;
    public float _transitionTime = 1f;
    
    
    public override void Interact(PlayerInstance player)
    {
        LoadNextScene();
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
