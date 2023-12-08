using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnInteract : AInteractable
{
    [SerializeField] float fadeSpeed = 3;

    public override void Interact(PlayerInstance player)
    {
        StartCoroutine(FadeCoroutine());
    }

    IEnumerator FadeCoroutine()
    {
        Renderer _renderer = gameObject.GetComponent<Renderer>();
        Color _objectColor = _renderer.material.color;
        while (_objectColor.a > 0)
        {
            float _fadeAmount = _objectColor.a - (fadeSpeed * Time.deltaTime);

            _objectColor = new Color(_objectColor.r, _objectColor.g, _objectColor.b, _fadeAmount);
            _renderer.material.color = _objectColor;
            yield return null;
        }
        Destroy(gameObject);
    }    
}
