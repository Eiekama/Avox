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

public class MenuController : MonoBehaviour
{
    public int lastSelectedIndex;

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

        _title.SetActive(false);
        _startButton.SetActive(false);
        _quitButton.SetActive(false);
    }

    private void Start()
    {
        _title.SetActive(true);
        _startButton.SetActive(true);
        _quitButton.SetActive(true);
        Sequence.Create()
            .Group(Tween.Custom(0f, 1f, duration: 1.0f, onValueChange: newVal => _titleTMP.color = new Vector4(_titleTMP.color.r, _titleTMP.color.g, _titleTMP.color.b, newVal)))
            .Group(Tween.UIAnchoredPositionY(_titleTransform, startValue: _titleYPos - 50, endValue: _titleYPos, duration: 1.0f))
            .Group(Tween.Custom(0f, 1f, duration: 0.7f, onValueChange: newVal => _startTMP.color = new Vector4(_startTMP.color.r, _startTMP.color.g, _startTMP.color.b, newVal)))
            .Group(Tween.UIAnchoredPositionY(_startTransform, startValue: _startYPos - 20, endValue: _startYPos, duration: 0.7f, startDelay: 0.5f))
            .Group(Tween.Custom(0f, 1f, duration: 0.7f, onValueChange: newVal => _quitTMP.color = new Vector4(_quitTMP.color.r, _quitTMP.color.g, _quitTMP.color.b, newVal)))
            .Group(Tween.UIAnchoredPositionY(_quitTransform, startValue: _quitYPos - 20, endValue: _quitYPos, duration: 0.7f, startDelay: 0.5f))
            .ChainCallback(() => { lastSelectedIndex = 0; StartCoroutine(SetSelectedAfterOneFrame(0)); });
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

    public void StartGame()
    {
        Sequence.Create()
            .Group(Tween.Custom(1f, 0f, duration: 0.7f, onValueChange: newVal => _startTMP.color = new Vector4(_startTMP.color.r, _startTMP.color.g, _startTMP.color.b, newVal)))
            .Group(Tween.UIAnchoredPositionY(_startTransform, startValue: _startYPos, endValue: _startYPos - 20, duration: 0.7f))
            .Group(Tween.Custom(1f, 0f, duration: 0.7f, onValueChange: newVal => _quitTMP.color = new Vector4(_quitTMP.color.r, _quitTMP.color.g, _quitTMP.color.b, newVal)))
            .Group(Tween.UIAnchoredPositionY(_quitTransform, startValue: _quitYPos, endValue: _quitYPos - 20, duration: 0.7f))
            .Group(Tween.Custom(1f, 0f, duration: 1.0f, onValueChange: newVal => _titleTMP.color = new Vector4(_titleTMP.color.r, _titleTMP.color.g, _titleTMP.color.b, newVal)))
            .Group(Tween.UIAnchoredPositionY(_titleTransform, startValue: _titleYPos, endValue: _titleYPos - 50, duration: 1.0f))
            .ChainCallback(() => SceneManager.LoadScene(sceneName: "IntroCutScene"));
    }

    public void QuitGame()
    {
        Sequence.Create()
            .Group(Tween.Custom(1f, 0f, duration: 0.7f, onValueChange: newVal => _startTMP.color = new Vector4(_startTMP.color.r, _startTMP.color.g, _startTMP.color.b, newVal)))
            .Group(Tween.UIAnchoredPositionY(_startTransform, startValue: _startYPos, endValue: _startYPos - 20, duration: 0.7f))
            .Group(Tween.Custom(1f, 0f, duration: 0.7f, onValueChange: newVal => _quitTMP.color = new Vector4(_quitTMP.color.r, _quitTMP.color.g, _quitTMP.color.b, newVal)))
            .Group(Tween.UIAnchoredPositionY(_quitTransform, startValue: _quitYPos, endValue: _quitYPos - 20, duration: 0.7f))
            .Group(Tween.Custom(1f, 0f, duration: 1.0f, onValueChange: newVal => _titleTMP.color = new Vector4(_titleTMP.color.r, _titleTMP.color.g, _titleTMP.color.b, newVal)))
            .Group(Tween.UIAnchoredPositionY(_titleTransform, startValue: _titleYPos, endValue: _titleYPos - 50, duration: 1.0f))
            .ChainCallback(() =>
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode()
#else
        Application.Quit()
#endif
        );

    }
}
