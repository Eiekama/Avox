using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    public GameObject[] Buttons;

    public GameObject LastSelected { get; set; }
    public int LastSelectedIndex { get; set; }

    private InputAction _navigationAction;
    private Vector2 NavigationInput { get; set; }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        _navigationAction = GetComponent<PlayerInput>().actions["Navigate"];
    }

    private void Start()
    {
        LastSelectedIndex = 0;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(sceneName: "IntroCutScene");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }


    private void FixedUpdate()
    {
        if (NavigationInput.y != 0 && EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(Buttons[LastSelectedIndex]);
        }
        NavigationInput = _navigationAction.ReadValue<Vector2>();
    }
}
