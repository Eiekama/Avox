using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OnBreak : MonoBehaviour
{
    [SerializeField] private GameObject breakObject;

    private void Update()
    {
        if (!breakObject)
        {
            Destroy(gameObject);
        }
    }   
}
