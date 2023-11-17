using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PrimeTween;

public class BorderInstance : MonoBehaviour
{
    [SerializeField] float _duration = 0.3f;

    [SerializeField] Image[] _fillImages;
    [SerializeField] Image[] _otherImages;

    public void Enable()
    {
        foreach (var item in _otherImages)
        {
            Tween.Alpha(item, startValue: 0, endValue: 1, duration: 0.5f * _duration);
        }
        foreach (var item in _fillImages)
        {
            Sequence.Create()
                .Group(Tween.Alpha(item, startValue: 0, endValue: 1, duration: _duration))
                .Group(Tween.UIFillAmount(item, startValue: 0, endValue: 1, duration: 0.7f * _duration, startDelay: 0.3f * _duration));
        }
    }

    public void Disable()
    {
        foreach (var item in _fillImages)
        {
            Sequence.Create()
                .Group(Tween.Alpha(item, startValue: 1, endValue: 0, duration: _duration))
                .Group(Tween.UIFillAmount(item, startValue: 0, endValue: 1, duration: 0.7f * _duration));
        }
        foreach (var item in _otherImages)
        {
            Tween.Alpha(item, startValue: 1, endValue: 0, duration: 0.5f * _duration, startDelay: 0.5f * _duration);
        }
    }
}
