using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour , IDamageable
{    
    public Animator anim;
    private void Start() {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {

        if(other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("walking through");
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {

        if(other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Exit");
        }
    }

    private void Damage(Transform source, int dmgTaken){
        
    }
}
