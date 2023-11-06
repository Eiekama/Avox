using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public Vector2 NavigationInput { get; set; }

    private InputAction _navigationAction;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _navigationAction= GetComponent<PlayerInput>().actions["Navigate"];

    }

    private void Update()
    {
        NavigationInput = _navigationAction.ReadValue<Vector2>();
    }
 
}
