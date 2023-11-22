using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnInteract : AInteractable
{
    public float fadeSpeed = 3;
    private bool fade = false;
    public override void Interact(PlayerInstance player)
    {
        fade = true;
    }
    
    private void Update()
    {

        
        if (fade)
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
