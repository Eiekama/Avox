using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingPlatform : MonoBehaviour
{    
    public Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionStay2D(Collision2D other) 
    {

        if(other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("OnStep");
            anim.SetTrigger("Remove");
        }
    }
}
