using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OnBreak : MonoBehaviour
{
    [SerializeField] private GameObject breakObject;
    public float fadeSpeed = 3;
    private void Update()
    {

        
        if (!breakObject)
        {
            Color _objectColor = gameObject.GetComponent<Renderer>().material.color;
            float _fadeAmount = _objectColor.a - (fadeSpeed * Time.deltaTime);

            _objectColor = new Color(_objectColor.r, _objectColor.g, _objectColor.b, _fadeAmount);
            gameObject.GetComponent<Renderer>().material.color = _objectColor;

            if (_objectColor.a <= 0)
            {
                Destroy(gameObject);
            }
            
        }
    }


}
