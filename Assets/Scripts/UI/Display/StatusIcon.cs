using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusIcon : MonoBehaviour
{
    public Image _image;

    public Sprite baseSprite;
    public Sprite depletedSprite;
    public bool state;

    Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();

        state = true;
    }

    public void UpdateLook(bool active, bool playAnimation = true)
    {
        if (state == active && playAnimation)
            return;
        state = active;

        // this function will only get called when the player's health/mana changes
        if (_animator)
        {
            _animator.SetBool("Animate", playAnimation);
            _animator.SetBool("Active", active);
        }
        else
            _image.sprite = active ? baseSprite : depletedSprite;
    }
}
