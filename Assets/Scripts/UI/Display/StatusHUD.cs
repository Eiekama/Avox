using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusHUD : MonoBehaviour
{
    public static StatusHUD instance;

    public StatusIcon healthIconBase;

    public RectTransform manaTransform;
    public StatusIcon manaIconBase;

    public Vector2 healthIconDelta;
    public Vector2 manaIconDelta;

    List<StatusIcon> _healthIcons;
    List<StatusIcon> _manaIcons;
    PlayerInstance _playerInstance;

    // Start is called before the first frame update
    void Start()
    {
        // clone default objects to create HP/MP "bars"
        _playerInstance = FindObjectOfType<PlayerInstance>();

        _healthIcons = new List<StatusIcon>();
        _healthIcons.Add(healthIconBase);
        for (int i = 1; i < _playerInstance.data.maxHP; i++)
        {
            _healthIcons.Add(Instantiate(healthIconBase, healthIconBase.transform.parent));
            _healthIcons[i].GetComponent<RectTransform>().anchoredPosition = (
                healthIconBase.GetComponent<RectTransform>().anchoredPosition + healthIconDelta * i);
        }

        _manaIcons = new List<StatusIcon>();
        _manaIcons.Add(manaIconBase);
        for (int i = 1; i < _playerInstance.data.maxMP; i++)
        {
            _manaIcons.Add(Instantiate(manaIconBase, manaIconBase.transform.parent));
            _manaIcons[i].GetComponent<RectTransform>().anchoredPosition = (
                manaIconBase.GetComponent<RectTransform>().anchoredPosition + manaIconDelta * i);
        }

        UpdateHud(false);
        instance = this;
    }

    void FixedUpdate()
    {
        // move mana bar to player
        manaTransform.anchoredPosition = RectTransformUtility.WorldToScreenPoint(Camera.main,
            _playerInstance.transform.position);
    }

    public void UpdateHud(bool playAnimation = true)
    {
        for (int i = 0; i < _healthIcons.Count; i++)
        {
            _healthIcons[i].UpdateLook(_playerInstance.data.currentHP > i, playAnimation);
        }
        for (int i = 0; i < _manaIcons.Count; i++)
        {
            _manaIcons[i].UpdateLook(_playerInstance.data.currentMP > i, playAnimation);
        }
    }
}
