using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusHUD : MonoBehaviour
{
    public static StatusHUD instance;

    public List<StatusIcon> _healthIcons;

    public RectTransform manaTransform;
    public GameObject manaStem;
    public List<StatusIcon> _manaIcons;

    Camera _cam;
    Vector2 _uiResolution;
    PlayerInstance _playerInstance;


    private void Awake()
    {
        _cam = Camera.main;
        _uiResolution = GetComponent<CanvasScaler>().referenceResolution;

        _playerInstance = FindObjectOfType<PlayerInstance>();
    }

    public void RefreshMaxima()
    {
        for (int i = 0; i < _healthIcons.Count; i++)
        {
            if (i < _playerInstance.data.maxHP)
                _manaIcons[i].gameObject.SetActive(true);
            else
                _manaIcons[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < _manaIcons.Count; i++)
        {
            if (i < _playerInstance.data.maxMP)
                _manaIcons[i].gameObject.SetActive(true);
            else
                _manaIcons[i].gameObject.SetActive(false);
        }

        manaStem.SetActive(_playerInstance.data.maxMP > 0);

        UpdateHud(false);
    }

    private void Start()
    {
        RefreshMaxima();
        instance = this;
    }


    private void FixedUpdate()
    {
        // Allows mana bar to follow player.
        // Only works if anchor is at bottom left corner.
        manaTransform.anchoredPosition = Vector2.Scale(_cam.WorldToViewportPoint(_playerInstance.transform.position), _uiResolution);
    }

    public void UpdateHud(bool playAnimation = true)
    {
        for (int i = 0; i < _playerInstance.data.maxHP; i++)
        {
            _healthIcons[i].UpdateLook(_playerInstance.data.currentHP > i, playAnimation);
        }
        for (int i = 0; i < _playerInstance.data.maxMP; i++)
        {
            _manaIcons[i].UpdateLook(_playerInstance.data.currentMP > i, playAnimation);
        }
    }
}
