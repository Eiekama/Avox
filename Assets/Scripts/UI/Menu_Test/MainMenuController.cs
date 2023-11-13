using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController instance;

    public GameObject[] Buttons;

    public GameObject _mainMenuCanvasGO;
    public GameObject _settingCanvasGO;

    public GameObject LastSelected { get; set; }
    public int LastSelectedIndex { get; set; }


    //private bool isSetting;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        _mainMenuCanvasGO.SetActive(true);
        _settingCanvasGO.SetActive(false);


        LastSelectedIndex = 0;
    }

    private void Update()
    {
        if (InputManager.instance.NavigationInput.y != 0 && EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(Buttons[LastSelectedIndex]);
        }
    }
}
