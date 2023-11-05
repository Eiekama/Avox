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
    private void OnTriggerEnter2D(Collider2D other) 
    {

        if(other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("walking through");
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {

        if(other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Exit");
        }
    }

    public void Damage(Transform source, int dmgTaken){
        anim.SetTrigger("Attacked");
    }
}
