using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using PrimeTween;

public class ButtonInstance : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    public delegate void Function();
    [HideInInspector] public Function function;

    [SerializeField] float _duration = 0.15f;

    [SerializeField] GameObject _selectIcon;
    private Image _selectRenderer;
    private RectTransform _selectTransform;

    [SerializeField] GameObject _backgroundIcon;
    private Image _backgroundRenderer;
    private RectTransform _backgroundTransform;

    private MenuController _controller;


    private void Awake()
    {
        _selectRenderer = _selectIcon.GetComponent<Image>();
        _selectTransform = _selectIcon.GetComponent<RectTransform>();

        _backgroundRenderer = _backgroundIcon.GetComponent<Image>();
        _backgroundTransform = _backgroundIcon.GetComponent<RectTransform>();

        _controller = GetComponentInParent<MenuController>();
    }

    public void OnClick()
    {
        _backgroundIcon.SetActive(true);
        Sequence.Create()
            .Group(Tween.Alpha(_backgroundRenderer, startValue: 0, endValue: 148, duration: 0.25f * _duration))
            .Group(Tween.ScaleX(_backgroundTransform, startValue: 0.2f, endValue: 1.0f, duration: 0.25f * _duration))
            .ChainCallback(() => function())
            .Group(Tween.Alpha(_backgroundRenderer, startValue: 148, endValue: 0, duration: 0.25f * _duration, startDelay: 0.25f * _duration))
            .Group(Tween.ScaleX(_backgroundTransform, startValue: 1.0f, endValue: 0.2f, duration: 0.25f * _duration, startDelay: 0.25f * _duration))
            .Group(Tween.Alpha(_selectRenderer, startValue: 255, endValue: 0, duration: _duration, startDelay: 0.25f * _duration))
            .Group(Tween.ScaleX(_selectTransform, startValue: 1.0f, endValue: 0.8f, duration: _duration, startDelay: 0.25f * _duration));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.selectedObject = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.selectedObject = null;
    }


    public void OnSelect(BaseEventData eventData)
    {
        _selectIcon.SetActive(true);
        Sequence.Create()
            .Group(Tween.Alpha(_selectRenderer, startValue: 0, endValue: 255, duration: _duration))
            .Group(Tween.ScaleX(_selectTransform, startValue: 0.8f, endValue: 1.0f, duration: _duration));
        
        
        for (int i = 0; i < _controller.buttons.Length; i++)
        {
            if (_controller.buttons[i] == this)
            {
                _controller.lastSelectedIndex = i;
                return;
            }
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Sequence.Create()
            .Group(Tween.Alpha(_selectRenderer, startValue: 255, endValue: 0, duration: _duration))
            .Group(Tween.ScaleX(_selectTransform, startValue: 1.0f, endValue: 0.8f, duration: _duration))
            .ChainCallback(() => _selectIcon.SetActive(false));
    }
}
