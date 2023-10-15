using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenuController : MonoBehaviour
{
    private GameObject _pauseMenuCanvasGO;
    private GameObject _normalCanvasGO;

    private GameObject _selectedFirst;

    private GameObject _resumeButton;
    private GameObject _backToMainButton;

    private bool isPaused;

    private void Start()
    {
        _pauseMenuCanvasGO = GameObject.Find("pauseMenuCanvas") ;
        _normalCanvasGO = GameObject.Find("normalCanvas");

        _resumeButton = GameObject.Find("ResumeButton");
        _selectedFirst = _resumeButton;
        _backToMainButton = GameObject.Find("BackToMainButton");



        _pauseMenuCanvasGO.SetActive(false);
        _normalCanvasGO.SetActive(true);
    }

    public void OpenCloseMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
    }

    public void Pause(){
        isPaused=true;
        Time.timeScale=0f;
        OpenPauseMenu();

    }

    public void Unpause(){
        isPaused=false;
        Time.timeScale=1f;
        ClosePauseMenu();
    }
    public void OpenPauseMenu(){
        _pauseMenuCanvasGO.SetActive(true);
        _normalCanvasGO.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_selectedFirst);

    }
    public void ClosePauseMenu(){
        _pauseMenuCanvasGO.SetActive(false);
        _normalCanvasGO.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnResumePress(){
        Unpause();
    }
    public void OnEscPress(){
        Pause();
    }
    public void OnBackToMainPress() {
        SceneManager.LoadScene(sceneName: "mainMenu");
    }
    public void onResumeOver()
    {
        EventSystem.current.SetSelectedGameObject(_resumeButton);
    }
    public void onBackToMainOver()
    {
        EventSystem.current.SetSelectedGameObject(_backToMainButton);
    }
    

}
