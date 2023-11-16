using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using PrimeTween;
using System;

public class CutSceneController : MonoBehaviour
{
    private PlayerInput _playerInput;

    [SerializeField] TMP_Text _prompt;
    private RectTransform _promptTransform;
    private float _promptYPos;

    [SerializeField] Image _progressIcon;
    private RectTransform _progressTransform;

    [SerializeField] Image _crossFade;

    private float _promptDuration = 5.0f;
    private float _progressDuration = 3.0f;

    private bool showPrompt;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions["ShowPrompt"].performed += ShowPromptPerformed;
        _playerInput.actions["ShowPrompt"].canceled += ShowPromptCanceled;
        _playerInput.actions["Skip"].performed += SkipPerformed;
        _playerInput.actions["Skip"].canceled += SkipCanceled;

        _promptTransform = _prompt.GetComponent<RectTransform>();
        _progressTransform = _progressIcon.GetComponent<RectTransform>();
        _promptYPos = _promptTransform.anchoredPosition.y;
        
        _prompt.gameObject.SetActive(false);
        _progressIcon.gameObject.SetActive(false);
        _progressIcon.fillAmount = 0;
        _crossFade.gameObject.SetActive(true);

        Sequence.Create()
            .Group(Tween.Alpha(_crossFade, startValue: 1, endValue: 0, duration: 0.5f))
            .ChainCallback(target: this, target => target._crossFade.gameObject.SetActive(false));
    }

    private void ShowPromptPerformed(InputAction.CallbackContext context)
    {
        showPrompt = true;
    }

    private Sequence promptSequence;
    private void ShowPromptCanceled(InputAction.CallbackContext context)
    {
        if (showPrompt && !_prompt.gameObject.activeInHierarchy)
        {
            _prompt.gameObject.SetActive(true);
            promptSequence = Sequence.Create()
                .Group(Tween.UIAnchoredPositionY(_promptTransform, startValue: _promptYPos - 10, endValue: _promptYPos, duration: 0.2f))
                .Group(Tween.Custom(this, 0f, 1f, duration: 0.2f, onValueChange: (target, newVal) => target.SetAlpha(target._prompt, newVal)))
                .ChainDelay(_promptDuration)
                .Chain(Sequence.Create()
                    .Group(Tween.UIAnchoredPositionY(_promptTransform, startValue: _promptYPos, endValue: _promptYPos - 10, duration: 0.2f))
                    .Group(Tween.Custom(this, 1f, 0f, duration: 0.2f, onValueChange: (target, newVal) => target.SetAlpha(target._prompt, newVal)))
                    .ChainCallback(target: this, target => target._prompt.gameObject.SetActive(false)));
        }
    }

    private Sequence progressSequence;
    private Tween progressTween;
    private void SkipPerformed(InputAction.CallbackContext context)
    {
        showPrompt = false;
        _progressIcon.gameObject.SetActive(true);
        progressTween = Tween.Scale(_progressTransform, startValue: 0.95f, endValue: 1.05f, duration: 0.4f, cycles: -1, cycleMode: CycleMode.Rewind);
        progressSequence = Sequence.Create()
            .Group(Tween.Alpha(_progressIcon, startValue: 0, endValue: 1, duration: 0.15f))
            .Group(Tween.UIFillAmount(_progressIcon, startValue: 0, endValue: 1, duration: _progressDuration))
            .ChainCallback(target: this, target => target.StartCoroutine(target.SkipCutScene()));
    }

    IEnumerator SkipCutScene()
    {
        promptSequence.Complete();
        Tween.Alpha(_progressIcon, startValue: 1, endValue: 0, duration: 0.15f);
        yield return FadeOut();
        SceneManager.LoadScene(1);
    }

    private void SkipCanceled(InputAction.CallbackContext context)
    {
        progressTween.Stop();
        progressSequence.Stop();
        if (_progressIcon.gameObject.activeInHierarchy)
        {
            Sequence.Create()
            .Group(Tween.Alpha(_progressIcon, startValue: 1, endValue: 0, duration: 0.15f))
            .ChainCallback(target: this, target => target._progressIcon.gameObject.SetActive(false));
        }
    }

    IEnumerator FadeOut()
    {
        _crossFade.gameObject.SetActive(true);
        yield return Tween.Alpha(_crossFade, startValue: 0, endValue: 1, duration: 0.5f).ToYieldInstruction();
    }

    private void SetAlpha(TMP_Text text, float a)
    {
        text.color = new Vector4(text.color.r, text.color.g, text.color.b, a);
    }
}
