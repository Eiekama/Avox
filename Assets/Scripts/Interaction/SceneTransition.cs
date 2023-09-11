using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : AInteractable
{
    public int sceneBuildIndex;
    private void OnCollisionEnter2D(Collision2D other)
    {
        _isAuto = true;
        
    }
}
