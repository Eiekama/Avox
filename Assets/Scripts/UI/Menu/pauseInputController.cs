using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class pauseInputController : MonoBehaviour
{

    public bool MenuOpenCloseInput {get; private set;}

    private PlayerInput _playerInput;

    private InputAction _menuOpenCloseAction;

    private void Awake()
    {

        _playerInput=GetComponent<PlayerInput>();
        _menuOpenCloseAction=_playerInput.actions["MenuOpenClose"];
    }
    
    private void Update()
    {
        MenuOpenCloseInput=_menuOpenCloseAction.WasPressedThisFrame();
    }
}
