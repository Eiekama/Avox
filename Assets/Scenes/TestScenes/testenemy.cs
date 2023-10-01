using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testenemy : AStateManager
{
    public IState currentState;
    public SleepState sleepState = new SleepState();
    public WalkingState walkState = new WalkingState();
 
    void Awake()
    {
        startState = sleepState;
    }
}
