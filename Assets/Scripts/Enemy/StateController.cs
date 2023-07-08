//credit: https://gamedevbeginner.com/interfaces-in-unity/#interface_state_machine

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateController : MonoBehaviour
{
    protected IState currentState;

    protected void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        currentState.OnEntry(this);
    }

    protected virtual void Update()
    {
        currentState.OnUpdate(this);
    }
}
