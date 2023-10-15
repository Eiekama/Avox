//credit: https://gamedevbeginner.com/interfaces-in-unity/#interface_state_machine

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AStateManager : MonoBehaviour
{
    public IState currentState { get; protected set; }

    public IState startState { get; protected set; }

    // remember to add public readonly variables for all relevant states when
    // implementing inherited classes

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = newState;
        currentState.OnEntry();
    }
}
