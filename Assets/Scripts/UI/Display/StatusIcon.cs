using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusIcon : MonoBehaviour
{
    public Sprite baseSprite;
    public bool state;

    Image _image;
    RectTransform _rt;

    void Awake()
    {
        _image = GetComponent<Image>();
        _rt = GetComponent<RectTransform>();

        state = true;
    }

    public void UpdateLook(bool active, bool playAnimation = true)
    {
        if (state == active)
            return;
        state = active;

        // later, put animator calls here to make fancier effects
        // this function will only get called when the player's health changes
        if (active)
        {
            _image.color = new Color(1, 1, 1, 1);
            _rt.localScale = Vector3.one;
            
        }
        else
        {
            _image.color = new Color(0.7f, 0.5f, 0.5f, 0.7f);
            _rt.localScale = Vector3.one * 0.8f;
        }
    }
}
