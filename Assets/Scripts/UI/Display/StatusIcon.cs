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

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        _rt = GetComponent<RectTransform>();
    }

    public void UpdateLook(bool active)
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
            _image.color = new Color(0.8f, 0.8f, 0.8f, 0.8f);
            _rt.localScale = Vector3.one * 0.6f;
        }
    }
}
