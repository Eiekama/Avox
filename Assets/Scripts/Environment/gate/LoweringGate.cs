using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoweringGate : MonoBehaviour
{    
    public Animator anim;
    [SerializeField] int _index;
    public int index { get { return _index; } }
    (GateData data, int i) _dataAndIndex;
    public (GateData data, int i) dataAndIndex
    {
        get { return _dataAndIndex; }
        set { if (_dataAndIndex.data == null) _dataAndIndex = value; }
    }
    private void Start() {
        anim = GetComponent<Animator>();
    }

    public void Lower() 
    {
            anim.SetTrigger("entered");
            _dataAndIndex.data.info[_dataAndIndex.i].lowered = true;
    }
}
