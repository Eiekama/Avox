using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuController : MonoBehaviour
{
    public GameObject _pauseMenuCanvasGO;
    public GameObject _normalCanvasGO;

    private bool isPaused;

    private void Start()
    {
        _pauseMenuCanvasGO.SetActive(false);

    }

    private void Updated()
    {
        // if(PlayerInput.MenuOpenCloseInput)
        // {
        //     if(!isPaused){
        //         Pause();
        //     }else{
        //         Unpause();
        //     }
        // }
    }

    public void Pause(){
        isPaused=true;
        Time.timeScale=0f;
        OpenPauseMenu();

    }

    public void Unpause(){
        isPaused=false;
        Time.timeScale=1f;
        CloseAllMenu();
    }
    public void OpenPauseMenu(){
        _pauseMenuCanvasGO.SetActive(true);
        _normalCanvasGO.SetActive(false);
    }
    public void CloseAllMenu(){
        _pauseMenuCanvasGO.SetActive(false);
        _normalCanvasGO.SetActive(true);
    }

    public void OnResumePress(){
        Unpause();
    }
    public void OnEscPress(){
        Pause();
    }

}
