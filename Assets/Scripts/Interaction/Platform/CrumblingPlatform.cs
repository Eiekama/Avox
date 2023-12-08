using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingPlatform : AInteractable
{    
    [SerializeField] OnBreak _mask;
    public static bool _activated;

    private void Start()
    {
        if (_activated) { Destroy(gameObject); }
    }

    public override void Interact(PlayerInstance player)
    {
        GetComponent<Animator>().SetTrigger("OnStep");
        _activated = true;
    }

    public void RemoveMask()
    {
        _mask.Fade();
    }
}
