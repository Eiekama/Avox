using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using PrimeTween;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] PlayerInstance _player;

    public static PauseMenuController instance;

    [HideInInspector] public int lastSelectedIndex = 0;

    public ButtonInstance[] buttons { get; private set; }

    [SerializeField] GameObject _menu;

    [SerializeField] GameObject _warning;

    [SerializeField] GameObject _topBorder_left;
    [SerializeField] GameObject _topBorder_circle;
    [SerializeField] GameObject _topBorder_right;
    private Image _topBorder_left_img;
    private Image _topBorder_right_img;
    private Image _topBorder_circle_img;

    [SerializeField] GameObject _bottomBorder_left;
    [SerializeField] GameObject _bottomBorder_right;
    private Image _bottomBorder_left_img;
    private Image _bottomBorder_right_img;


    [SerializeField] GameObject _continueButton;
    private TMP_Text _continueTMP;
    private RectTransform _continueTransform;
    private float _continueYPos;
    [SerializeField] GameObject _quitButton;
    private TMP_Text _quitTMP;
    private RectTransform _quitTransform;
    private float _quitYPos;

    private bool isWarning = false;

    [SerializeField] GameObject _topBorder_left_warning;
    [SerializeField] GameObject _topBorder_circle_warning;
    [SerializeField] GameObject _topBorder_right_warning;
    private Image _topBorder_left_warning_img;
    private Image _topBorder_right_warning_img;
    private Image _topBorder_circle_warning_img;

    [SerializeField] GameObject _bottomBorder_left_warning;
    [SerializeField] GameObject _bottomBorder_right_warning;
    private Image _bottomBorder_left_warning_img;
    private Image _bottomBorder_right_warning_img;

    [SerializeField] GameObject _warningText;
    private TMP_Text _warningTMP;
    private RectTransform _warningTransform;
    private float _warningYPos;

    [SerializeField] GameObject _yesButton;
    private TMP_Text _yesTMP;
    private RectTransform _yesTransform;
    private float _yesYPos;
    [SerializeField] GameObject _noButton;
    private TMP_Text _noTMP;
    private RectTransform _noTransform;
    private float _noYPos;



    [SerializeField] GameObject _background;
    private Image _background_img;



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

        _background_img = _background.GetComponent<Image>();

        _topBorder_left_img = _topBorder_left.GetComponent<Image>();
        _topBorder_circle_img = _topBorder_circle.GetComponent<Image>();
        _topBorder_right_img = _topBorder_right.GetComponent<Image>();


        _bottomBorder_left_img = _bottomBorder_left.GetComponent<Image>();
        _bottomBorder_right_img = _bottomBorder_right.GetComponent<Image>();

        _continueTMP = _continueButton.GetComponentInChildren<TMP_Text>();
        _continueTransform = _continueButton.GetComponent<RectTransform>();
        _continueYPos = _continueTransform.anchoredPosition.y;

        _quitTMP = _quitButton.GetComponentInChildren<TMP_Text>();
        _quitTransform = _quitButton.GetComponent<RectTransform>();
        _quitYPos = _quitTransform.anchoredPosition.y;

        _topBorder_left_warning_img = _topBorder_left_warning.GetComponent<Image>();
        _topBorder_circle_warning_img = _topBorder_circle_warning.GetComponent<Image>();
        _topBorder_right_warning_img = _topBorder_right_warning.GetComponent<Image>();


        _bottomBorder_left_warning_img = _bottomBorder_left_warning.GetComponent<Image>();
        _bottomBorder_right_warning_img = _bottomBorder_right_warning.GetComponent<Image>();

        _warningTMP = _warningText.GetComponent<TMP_Text>();
        _warningTransform = _warningText.GetComponent<RectTransform>();
        _warningYPos = _warningTransform.anchoredPosition.y;

        _yesTMP = _yesButton.GetComponentInChildren<TMP_Text>();
        _yesTransform = _yesButton.GetComponent<RectTransform>();
        _yesYPos = _yesTransform.anchoredPosition.y;

        _noTMP = _noButton.GetComponentInChildren<TMP_Text>();
        _noTransform = _noButton.GetComponent<RectTransform>();
        _noYPos = _noTransform.anchoredPosition.y;

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
        //_warning.SetActive(false);
        //_background.SetActive(true);
        Tween.Custom(this, 0f, 0.7f, duration: 0.6f, onValueChange: (target, newVal) => target.SetImageAlpha(target._background_img, newVal), useUnscaledTime: true);
        StartCoroutine(ShowPauseMenu());
    }


    IEnumerator ShowPauseMenu()
    {

        yield return Sequence.Create()

            .Group(Tween.Custom(this, 0f, 1f, duration: 0.9f, startDelay: 0.6f, onValueChange: (target, newVal) => target.SetImageAlpha(target._topBorder_circle_img, newVal), useUnscaledTime: true))

            .Group(Tween.Custom(this, 0f, 1f, duration: 0.7f, startDelay: 0.9f, onValueChange: (target, newVal) => target.SetFillAmount(target._topBorder_left_img, newVal), useUnscaledTime: true))
            .Group(Tween.Custom(this, 0f, 1f, duration: 0.7f, startDelay: 0.9f, onValueChange: (target, newVal) => target.SetFillAmount(target._topBorder_right_img, newVal), useUnscaledTime: true))

            .Group(Tween.Custom(this, 0f, 1f, duration: 0.7f, startDelay: 0.9f, onValueChange: (target, newVal) => target.SetFillAmount(target._bottomBorder_left_img, newVal), useUnscaledTime: true))
            .Group(Tween.Custom(this, 0f, 1f, duration: 0.7f, startDelay: 0.9f, onValueChange: (target, newVal) => target.SetFillAmount(target._bottomBorder_right_img, newVal), useUnscaledTime: true))


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
        StartCoroutine(ContinueGameCoroutine());
    }

    IEnumerator ContinueGameCoroutine()
    {
        Tween.Custom(this, 0.7f, 0f, duration: 0.6f, startDelay: 0.6f, onValueChange: (target, newVal) => target.SetImageAlpha(target._background_img, newVal), useUnscaledTime: true);
        StartCoroutine(HidePauseMenu());

        Time.timeScale = 1f;
        _player.controller.ToggleActionMap(_player.controller.inputActions.Player);
        yield return FadeOut(1.5f);
        gameObject.SetActive(false);
    }

    private void OpenWarning()
    {
        StartCoroutine(OpenwarningCoroutine());
    }

    IEnumerator OpenwarningCoroutine()
    {
        StartCoroutine(HidePauseMenu());
        FadeOut(1.6f);
        yield return StartCoroutine(ShowWarningMenu());
    }

    IEnumerator ShowWarningMenu()
    {
        _warning.SetActive(true);
        yield return Sequence.Create()

            .Group(Tween.Custom(this, 0f, 1f, duration: 0.9f, startDelay: 0.6f, onValueChange: (target, newVal) => target.SetImageAlpha(target._topBorder_circle_warning_img, newVal), useUnscaledTime: true))

            .Group(Tween.Custom(this, 0f, 1f, duration: 0.7f, startDelay: 0.9f, onValueChange: (target, newVal) => target.SetFillAmount(target._topBorder_left_warning_img, newVal), useUnscaledTime: true))
            .Group(Tween.Custom(this, 0f, 1f, duration: 0.7f, startDelay: 0.9f, onValueChange: (target, newVal) => target.SetFillAmount(target._topBorder_right_warning_img, newVal), useUnscaledTime: true))

            .Group(Tween.Custom(this, 0f, 1f, duration: 0.7f, startDelay: 0.9f, onValueChange: (target, newVal) => target.SetFillAmount(target._bottomBorder_left_warning_img, newVal), useUnscaledTime: true))
            .Group(Tween.Custom(this, 0f, 1f, duration: 0.7f, startDelay: 0.9f, onValueChange: (target, newVal) => target.SetFillAmount(target._bottomBorder_right_warning_img, newVal), useUnscaledTime: true))

            .Group(Tween.Custom(this, 0f, 1f, duration: 0.7f, startDelay: 0.9f, onValueChange: (target, newVal) => target.SetAlpha(target._warningTMP, newVal), useUnscaledTime: true))
            .Group(Tween.UIAnchoredPositionY(_warningTransform, startValue: _warningYPos - 20, endValue: _warningYPos, duration: 0.7f, startDelay: 0.9f, useUnscaledTime: true))

            .Group(Tween.Custom(this, 0f, 1f, duration: 0.7f, startDelay: 0.9f, onValueChange: (target, newVal) => target.SetAlpha(target._yesTMP, newVal), useUnscaledTime: true))
            .Group(Tween.UIAnchoredPositionY(_yesTransform, startValue: _yesYPos - 20, endValue: _yesYPos, duration: 0.7f, startDelay: 0.9f, useUnscaledTime: true))

            .Group(Tween.Custom(this, 0f, 1f, duration: 0.7f, startDelay: 0.9f, onValueChange: (target, newVal) => target.SetAlpha(target._noTMP, newVal), useUnscaledTime: true))
            .Group(Tween.UIAnchoredPositionY(_noTransform, startValue: _noYPos - 20, endValue: _noYPos, duration: 0.7f, startDelay: 0.9f, useUnscaledTime: true))
            .ToYieldInstruction();

        lastSelectedIndex = 3;
        StartCoroutine(SetSelectedAfterOneFrame(3));
        isWarning = true;
        _menu.SetActive(false);

    }


    private void CloseWarning()
    {
        StartCoroutine(ClosewarningCoroutine());

    }
    IEnumerator ClosewarningCoroutine()
    {
        StartCoroutine(HideWarningMenu());
        FadeOut(1.6f);
        yield return StartCoroutine(ShowPauseMenu());
    }

    IEnumerator HideWarningMenu()
    {
        _menu.SetActive(true);

        yield return Sequence.Create()

            .Group(Tween.Custom(this, 1f, 0f, duration: 0.9f, startDelay: 0.6f, onValueChange: (target, newVal) => target.SetImageAlpha(target._topBorder_circle_warning_img, newVal), useUnscaledTime: true))


            .Group(Tween.Custom(this, 1f, 0f, duration: 0.7f, onValueChange: (target, newVal) => target.SetFillAmount(target._topBorder_left_warning_img, newVal), useUnscaledTime: true))
            .Group(Tween.Custom(this, 1f, 0f, duration: 0.7f, onValueChange: (target, newVal) => target.SetFillAmount(target._topBorder_right_warning_img, newVal), useUnscaledTime: true))

            .Group(Tween.Custom(this, 1f, 0f, duration: 0.7f, startDelay: 0.1f, onValueChange: (target, newVal) => target.SetFillAmount(target._bottomBorder_left_warning_img, newVal), useUnscaledTime: true))
            .Group(Tween.Custom(this, 1f, 0f, duration: 0.7f, startDelay: 0.1f, onValueChange: (target, newVal) => target.SetFillAmount(target._bottomBorder_right_warning_img, newVal), useUnscaledTime: true))

            .Group(Tween.Custom(this, 1f, 0f, duration: 0.7f, startDelay: 0.1f, onValueChange: (target, newVal) => target.SetAlpha(target._warningTMP, newVal), useUnscaledTime: true))
            .Group(Tween.UIAnchoredPositionY(_warningTransform, startValue: _warningYPos, endValue: _warningYPos - 20, duration: 0.7f, startDelay: 0.9f, useUnscaledTime: true))

            .Group(Tween.Custom(this, 1f, 0f, duration: 0.7f, startDelay: 0.1f, onValueChange: (target, newVal) => target.SetAlpha(target._yesTMP, newVal), useUnscaledTime: true))
            .Group(Tween.UIAnchoredPositionY(_yesTransform, startValue: _yesYPos, endValue: _yesYPos - 20, duration: 0.7f, startDelay: 0.9f, useUnscaledTime: true))

            .Group(Tween.Custom(this, 1f, 0f, duration: 0.7f, startDelay: 0.1f, onValueChange: (target, newVal) => target.SetAlpha(target._noTMP, newVal), useUnscaledTime: true))
            .Group(Tween.UIAnchoredPositionY(_noTransform, startValue: _noYPos, endValue: _noYPos - 20, duration: 0.7f, startDelay: 0.9f, useUnscaledTime: true))
            .ToYieldInstruction();

        lastSelectedIndex = 0;
        StartCoroutine(SetSelectedAfterOneFrame(0));
        isWarning = false;
        _warning.SetActive(false);
    }

    private void QuitToMain()
    {
        Sequence.Create()

            .Group(Tween.Custom(this, 1f, 0f, duration: 0.9f, startDelay: 0.6f, onValueChange: (target, newVal) => target.SetImageAlpha(target._topBorder_circle_warning_img, newVal), useUnscaledTime: true))


            .Group(Tween.Custom(this, 1f, 0f, duration: 0.7f, onValueChange: (target, newVal) => target.SetFillAmount(target._topBorder_left_warning_img, newVal), useUnscaledTime: true))
            .Group(Tween.Custom(this, 1f, 0f, duration: 0.7f, onValueChange: (target, newVal) => target.SetFillAmount(target._topBorder_right_warning_img, newVal), useUnscaledTime: true))

            .Group(Tween.Custom(this, 1f, 0f, duration: 0.7f, startDelay: 0.1f, onValueChange: (target, newVal) => target.SetFillAmount(target._bottomBorder_left_warning_img, newVal), useUnscaledTime: true))
            .Group(Tween.Custom(this, 1f, 0f, duration: 0.7f, startDelay: 0.1f, onValueChange: (target, newVal) => target.SetFillAmount(target._bottomBorder_right_warning_img, newVal), useUnscaledTime: true))

            .Group(Tween.Custom(this, 1f, 0f, duration: 0.7f, startDelay: 0.1f, onValueChange: (target, newVal) => target.SetAlpha(target._warningTMP, newVal), useUnscaledTime: true))
            .Group(Tween.UIAnchoredPositionY(_warningTransform, startValue: _warningYPos, endValue: _warningYPos - 20, duration: 0.7f, startDelay: 0.9f, useUnscaledTime: true))

            .Group(Tween.Custom(this, 1f, 0f, duration: 0.7f, startDelay: 0.1f, onValueChange: (target, newVal) => target.SetAlpha(target._yesTMP, newVal), useUnscaledTime: true))
            .Group(Tween.UIAnchoredPositionY(_yesTransform, startValue: _yesYPos, endValue: _yesYPos - 20, duration: 0.7f, startDelay: 0.9f, useUnscaledTime: true))

            .Group(Tween.Custom(this, 1f, 0f, duration: 0.7f, startDelay: 0.1f, onValueChange: (target, newVal) => target.SetAlpha(target._noTMP, newVal), useUnscaledTime: true))
            .Group(Tween.UIAnchoredPositionY(_noTransform, startValue: _noYPos, endValue: _noYPos - 20, duration: 0.7f, startDelay: 0.9f, useUnscaledTime: true))
            .ChainCallback(() => {
                Time.timeScale = 1f;
                SceneManager.LoadScene(sceneName: "Menu");
            });
    }

    //private void onlyBackground()
    //{

    //}

    //IEnumerator QuitToMainCoroutine()
    //{
    //    yield return FadeOut(1.3f);
    //    Debug.Log("1");
    //    SceneManager.LoadScene(sceneName: "Menu");
    //}



    IEnumerator FadeOut(float delay)
    {
        yield return Tween.Delay(delay).ToYieldInstruction();

    }


    IEnumerator HidePauseMenu()
    {
        yield return Sequence.Create()


            .Group(Tween.Custom(this, 1f, 0f, duration: 0.9f, startDelay: 0.6f, onValueChange: (target, newVal) => target.SetImageAlpha(target._topBorder_circle_img, newVal), useUnscaledTime: true))


            .Group(Tween.Custom(this, 1f, 0f, duration: 0.7f, onValueChange: (target, newVal) => target.SetFillAmount(target._topBorder_left_img, newVal), useUnscaledTime: true))
            .Group(Tween.Custom(this, 1f, 0f, duration: 0.7f, onValueChange: (target, newVal) => target.SetFillAmount(target._topBorder_right_img, newVal), useUnscaledTime: true))

            .Group(Tween.Custom(this, 1f, 0f, duration: 0.7f, startDelay: 0.1f, onValueChange: (target, newVal) => target.SetFillAmount(target._bottomBorder_left_img, newVal), useUnscaledTime: true))
            .Group(Tween.Custom(this, 1f, 0f, duration: 0.7f, startDelay: 0.1f, onValueChange: (target, newVal) => target.SetFillAmount(target._bottomBorder_right_img, newVal), useUnscaledTime: true))

            .Group(Tween.Custom(this, 1f, 0f, duration: 0.7f, startDelay: 0.1f, onValueChange: (target, newVal) => target.SetAlpha(target._continueTMP, newVal), useUnscaledTime: true))
            .Group(Tween.UIAnchoredPositionY(_continueTransform, startValue: _continueYPos, endValue: _continueYPos - 20, duration: 0.7f, startDelay: 0.9f, useUnscaledTime: true))

            .Group(Tween.Custom(this, 1f, 0f, duration: 0.7f, startDelay: 0.1f, onValueChange: (target, newVal) => target.SetAlpha(target._quitTMP, newVal), useUnscaledTime: true))
            .Group(Tween.UIAnchoredPositionY(_quitTransform, startValue: _quitYPos, endValue: _quitYPos - 20, duration: 0.7f, startDelay: 0.9f, useUnscaledTime: true))
            .ToYieldInstruction();
    }



    private void SetAlpha(TMP_Text text, float a)
    {
        text.color = new Vector4(text.color.r, text.color.g, text.color.b, a);
    }

    private void SetFillAmount(Image image, float f)
    {
        image.fillAmount = f;
    }

    private void SetImageAlpha(Image image, float a)
    {
        image.color = new Vector4(image.color.r, image.color.g, image.color.b, a);
    }
}
