using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : AInteractable , IDamageable
{    
    public Animator anim;
    private void Start() {
        anim = GetComponent<Animator>();
    }
    public override void Interact(PlayerInstance player){
            anim.SetTrigger("walking through");
    }
    public override void OnExit(PlayerInstance player){
        anim.SetTrigger("Exit");
    }

    public void Damage(Collider2D source, int dmgTaken){
        anim.SetTrigger("Attacked");
    }
}
