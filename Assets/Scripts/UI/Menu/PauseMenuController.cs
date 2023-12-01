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
using UnityEditor.Rendering.LookDev;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] PlayerInstance _player;

    public static PauseMenuController instance;

    [HideInInspector] public int lastSelectedIndex=0;

    public ButtonInstance[] buttons { get; private set; }

    [SerializeField] GameObject _menu;

    [SerializeField] GameObject _waring;

    [SerializeField] GameObject _topBorder;
    private RectTransform _topBorderTransform;
    private float _topBorderYPos;
    [SerializeField] GameObject _bottomBorder;
    private RectTransform _bottomBorderTransform;
    private float _bottomBorderYPos;
    [SerializeField] GameObject _continueButton;
    private TMP_Text _continueTMP;
    private RectTransform _continueTransform;
    private float _continueYPos;
    [SerializeField] GameObject _quitButton;
    private TMP_Text _quitTMP;
    private RectTransform _quitTransform;
    private float _quitYPos;

    private bool isWarning = false;

    [SerializeField] GameObject _yeButton;
    [SerializeField] GameObject _noButton;



    [SerializeField] GameObject _background;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        buttons = GetComponentsInChildren<ButtonInstance>(true);
        buttons[0].function = ContinueGame;
        buttons[1].function = OpenWarning;

        buttons[2].function = QuitToMain;
        buttons[3].function = CloseWarning;

        _topBorderTransform = _topBorder.GetComponent<RectTransform>();
        _topBorderYPos = _topBorderTransform.anchoredPosition.y;

        _bottomBorderTransform = _bottomBorder.GetComponent<RectTransform>();
        _bottomBorderYPos = _bottomBorderTransform.anchoredPosition.y;

        _continueTMP = _continueButton.GetComponentInChildren<TMP_Text>();
        _continueTransform = _continueButton.GetComponent<RectTransform>();
        _continueYPos = _continueTransform.anchoredPosition.y;

        _quitTMP = _quitButton.GetComponentInChildren<TMP_Text>();
        _quitTransform = _quitButton.GetComponent<RectTransform>();
        _quitYPos = _quitTransform.anchoredPosition.y;

        _player.controller.inputActions.UI.Navigate.performed += NavigatePerformed;


    }


    //private void Start()
    //{

        
    //}

    public void PauseGame()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
        //_menu.SetActive(true);
        //_waring.SetActive(false);
        //_background.SetActive(true);
        StartCoroutine(ShowPauseMenu());
    }


    IEnumerator ShowPauseMenu()
    {
        
        yield return Sequence.Create()
            
            .Group(Tween.UIAnchoredPositionY(_topBorderTransform, startValue: _topBorderYPos - 50, endValue: _topBorderYPos, duration: 1.0f, useUnscaledTime:true))

            .Group(Tween.UIAnchoredPositionY(_bottomBorderTransform, startValue: _bottomBorderYPos - 50, endValue: _bottomBorderYPos, duration: 1.0f, useUnscaledTime: true))

            .Group(Tween.Custom(this, 0f, 1f, duration: 0.7f, startDelay: 0.9f, onValueChange: (target, newVal) => target.SetAlpha(target._continueTMP, newVal), useUnscaledTime: true))
            .Group(Tween.UIAnchoredPositionY(_continueTransform, startValue: _continueYPos - 20, endValue: _continueYPos, duration: 0.7f, startDelay: 0.9f, useUnscaledTime: true))

            .Group(Tween.Custom(this, 0f, 1f, duration: 0.7f, startDelay: 0.9f, onValueChange: (target, newVal) => target.SetAlpha(target._quitTMP, newVal), useUnscaledTime: true))
            .Group(Tween.UIAnchoredPositionY(_quitTransform, startValue: _quitYPos - 20, endValue: _quitYPos, duration: 0.7f, startDelay: 0.9f, useUnscaledTime: true))
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
        Debug.Log(context);
        var _input = context.ReadValue<Vector2>();
        if (_input.y > 0 || _input.x > 0) { HandleNavigation(-1); }
        else if (_input.y < 0 || _input.x < 0) { HandleNavigation(1); }
    }
    private void HandleNavigation(int addition)
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (isWarning)
            {
                EventSystem.current.SetSelectedGameObject(buttons[3].gameObject);
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
            }
            
        }
    }

    private void ContinueGame()
    {
        HidePauseMenu();
        gameObject.SetActive(false);
    }

    private void OpenWarning()
    {
        _menu.SetActive(false);
        _waring.SetActive(true);
        isWarning = true;
        EventSystem.current.SetSelectedGameObject(buttons[3].gameObject);
    }


    private void CloseWarning()
    {
        _menu.SetActive(true);
        _waring.SetActive(false);
        isWarning = false;
        EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
    }

    private void QuitToMain()
    {
        StartCoroutine(QuitToMainCoroutine());
    }

    IEnumerator QuitToMainCoroutine()
    {
        HidePauseMenu();
        yield return FadeOut(1.3f);
        SceneManager.LoadScene(sceneName: "Menu");
    }



    IEnumerator FadeOut(float delay)
    {
        yield return Tween.Delay(delay).ToYieldInstruction();
        
    }


    private void HidePauseMenu()
    {
        Sequence.Create()
            .Group(Tween.UIAnchoredPositionY(_topBorderTransform, startValue: _topBorderYPos, endValue: _topBorderYPos -50, duration: 1.0f, useUnscaledTime: true))

            .Group(Tween.UIAnchoredPositionY(_bottomBorderTransform, startValue: _bottomBorderYPos - 50, endValue: _bottomBorderYPos, duration: 1.0f, useUnscaledTime: true))

            .Group(Tween.Custom(this, 1f, 0f, duration: 0.7f, startDelay: 0.9f, onValueChange: (target, newVal) => target.SetAlpha(target._continueTMP, newVal), useUnscaledTime: true))
            .Group(Tween.UIAnchoredPositionY(_continueTransform, startValue: _continueYPos, endValue: _continueYPos -20, duration: 0.7f, startDelay: 0.9f, useUnscaledTime: true))

            .Group(Tween.Custom(this, 1f, 0f, duration: 0.7f, startDelay: 0.9f, onValueChange: (target, newVal) => target.SetAlpha(target._quitTMP, newVal), useUnscaledTime: true))
            .Group(Tween.UIAnchoredPositionY(_quitTransform, startValue: _quitYPos, endValue: _quitYPos -20, duration: 0.7f, startDelay: 0.9f, useUnscaledTime: true))
            .ToYieldInstruction();

        Time.timeScale = 1f;
        _player.controller.ToggleActionMap(_player.controller.inputActions.Player);

    }

    private void SetAlpha(TMP_Text text, float a)
    {
        text.color = new Vector4(text.color.r, text.color.g, text.color.b, a);
    }
}
