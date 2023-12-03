using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using PrimeTween;

public class MainMenuController : MonoBehaviour
{
    [HideInInspector] public int lastSelectedIndex=0;

    public ButtonInstance[] buttons { get; private set; }

    private PlayerInput _playerInput;

    [SerializeField] GameObject _title;
    private TMP_Text _titleTMP;
    private RectTransform _titleTransform;
    private float _titleYPos;
    [SerializeField] GameObject _startButton;
    private TMP_Text _startTMP;
    private RectTransform _startTransform;
    private float _startYPos;
    [SerializeField] GameObject _quitButton;
    private TMP_Text _quitTMP;
    private RectTransform _quitTransform;
    private float _quitYPos;

    [SerializeField] Image _crossFade;


    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions["Navigate"].performed += NavigatePerformed;

        buttons = GetComponentsInChildren<ButtonInstance>(true);
        buttons[0].function = StartGame;
        buttons[1].function = QuitGame;

        _titleTMP = _title.GetComponent<TMP_Text>();
        _titleTransform = _title.GetComponent<RectTransform>();
        _titleYPos = _titleTransform.anchoredPosition.y;

        _startTMP = _startButton.GetComponentInChildren<TMP_Text>();
        _startTransform = _startButton.GetComponent<RectTransform>();
        _startYPos = _startTransform.anchoredPosition.y;

        _quitTMP = _quitButton.GetComponentInChildren<TMP_Text>();
        _quitTransform = _quitButton.GetComponent<RectTransform>();
        _quitYPos = _quitTransform.anchoredPosition.y;
    }

    private void Start()
    {
        SetAlpha(_titleTMP, 0);
        SetAlpha(_startTMP, 0);
        SetAlpha(_quitTMP, 0);
        _crossFade.gameObject.SetActive(true);
        Sequence.Create()
            .Group(Tween.Alpha(_crossFade, startValue: 1, endValue: 0, duration: 0.5f))
            .ChainCallback(target: this, target => { target._crossFade.gameObject.SetActive(false); target.StartCoroutine(target.ShowMenu()); });

        
    }

    IEnumerator ShowMenu()
    {
        yield return Sequence.Create()
            .Group(Tween.Custom(this, 0f, 1f, duration: 1.0f, onValueChange: (target, newVal) => target.SetAlpha(target._titleTMP, newVal)))
            .Group(Tween.UIAnchoredPositionY(_titleTransform, startValue: _titleYPos - 50, endValue: _titleYPos, duration: 1.0f))

            .Group(Tween.Custom(this, 0f, 1f, duration: 0.7f, startDelay: 0.9f, onValueChange: (target, newVal) => target.SetAlpha(target._startTMP, newVal)))
            .Group(Tween.UIAnchoredPositionY(_startTransform, startValue: _startYPos - 20, endValue: _startYPos, duration: 0.7f, startDelay: 0.9f))

            .Group(Tween.Custom(this, 0f, 1f, duration: 0.7f, startDelay: 0.9f, onValueChange: (target, newVal) => target.SetAlpha(target._quitTMP, newVal)))
            .Group(Tween.UIAnchoredPositionY(_quitTransform, startValue: _quitYPos - 20, endValue: _quitYPos, duration: 0.7f, startDelay: 0.9f))
            .ToYieldInstruction();
        lastSelectedIndex = 0;
        StartCoroutine(SetSelectedAfterOneFrame(0));
    }

    IEnumerator SetSelectedAfterOneFrame(int i)
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(buttons[i].gameObject);
    }

    private void NavigatePerformed(InputAction.CallbackContext context)
    {
        var _input = context.ReadValue<Vector2>();
        if (_input.y > 0) { HandleNavigation(-1); }
        else if (_input.y < 0) { HandleNavigation(1); }
    }
    private void HandleNavigation(int addition)
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            var newIndex = lastSelectedIndex + addition;
            newIndex = Mathf.Clamp(newIndex, 0, buttons.Length - 1);
            EventSystem.current.SetSelectedGameObject(buttons[newIndex].gameObject);
        }
    }

    private void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }
    IEnumerator StartGameCoroutine()
    {
        HideMenu();
        yield return FadeOut(1.3f);
        SceneManager.LoadScene(sceneName: "IntroCutScene");
    }

    private void QuitGame()
    {
        StartCoroutine(QuitGameCoroutine());
    }
    IEnumerator QuitGameCoroutine()
    {
        HideMenu();
        yield return FadeOut(1.3f);
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    IEnumerator FadeOut(float delay)
    {
        yield return Tween.Delay(delay).ToYieldInstruction();
        _crossFade.gameObject.SetActive(true);
        yield return Tween.Alpha(_crossFade, startValue: 0, endValue: 1, duration: 0.5f).ToYieldInstruction();
    }

    private void HideMenu()
    {
        Sequence.Create()
            .Group(Tween.Custom(this, 1f, 0f, duration: 0.7f, startDelay: 0.1f, onValueChange: (target, newVal) => target.SetAlpha(target._startTMP, newVal)))
            .Group(Tween.UIAnchoredPositionY(_startTransform, startValue: _startYPos, endValue: _startYPos - 20, duration: 0.7f, startDelay: 0.1f))

            .Group(Tween.Custom(this, 1f, 0f, duration: 0.7f, startDelay: 0.1f, onValueChange: (target, newVal) => target.SetAlpha(target._quitTMP, newVal)))
            .Group(Tween.UIAnchoredPositionY(_quitTransform, startValue: _quitYPos, endValue: _quitYPos - 20, duration: 0.7f, startDelay: 0.1f))

            .Group(Tween.Custom(this, 1f, 0f, duration: 1.0f, startDelay: 0.6f, onValueChange: (target, newVal) => target.SetAlpha(target._titleTMP, newVal)))
            .Group(Tween.UIAnchoredPositionY(_titleTransform, startValue: _titleYPos, endValue: _titleYPos - 50, duration: 1.0f, startDelay: 0.6f));
    }

    private void SetAlpha(TMP_Text text, float a)
    {
        text.color = new Vector4(text.color.r, text.color.g, text.color.b, a);
    }
}
