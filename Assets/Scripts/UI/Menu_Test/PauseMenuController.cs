using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenuController : MonoBehaviour
{
    public static PauseMenuController instance;

    public GameObject[] Buttons;

    public GameObject _pauseMenuCanvasGO;
    public GameObject _normalCanvasGO;

    public GameObject LastSelected { get; set; }
    public int LastSelectedIndex { get; set; }


    private bool isPaused;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        _pauseMenuCanvasGO.SetActive(false);
        _normalCanvasGO.SetActive(true);


        LastSelectedIndex = 0;
    }

    private void Update()
    {
        if (InputManager.instance.NavigationInput.y!=0&&EventSystem.current.currentSelectedGameObject==null)
        {
            EventSystem.current.SetSelectedGameObject(Buttons[LastSelectedIndex]);
        }

    }

    public void OpenOrCloseMenu() {
        if (isPaused)
        {
            Unpause();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        OpenPauseMenu();

    }

    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1f;
        ClosePauseMenu();
    }
    public void OpenPauseMenu()
    {
        _pauseMenuCanvasGO.SetActive(true);
        _normalCanvasGO.SetActive(false);
        //EventSystem.current.SetSelectedGameObject(Buttons[0]);

    }
    public void ClosePauseMenu()
    {
        _pauseMenuCanvasGO.SetActive(false);
        _normalCanvasGO.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnResumePress()
    {
        Unpause();
    }
    public void OnEscPress()
    {
        Pause();
    }
    public void OnBackToMainPress()
    {
        SceneManager.LoadScene(sceneName: "mainMenu");
    }


}
