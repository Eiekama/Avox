using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingPlatform : MonoBehaviour
{    
    public Animator anim;
    [SerializeField] int _index;
    public int index { get { return _index; } }
    (PlatformData data, int i) _dataAndIndex;
    public (PlatformData data, int i) dataAndIndex
    {
        get { return _dataAndIndex; }
        set { if (_dataAndIndex.data == null) _dataAndIndex = value; }
    }
    private void Start() {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("OnStep");
            _dataAndIndex.data.info[_dataAndIndex.i].collapsed = true;
        }
    }
}
