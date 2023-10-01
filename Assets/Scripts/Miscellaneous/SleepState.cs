using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepState : IState
{
    Transform myTransform;
    float timeBeforeWalking;
    public void OnEntry(testenemy sm)
    {
        //myTransform = controller.transform;
        Debug.Log("In Sleep State");
        timeBeforeWalking = 5;
    }
    public void OnUpdate(testenemy sm)
    {
        if (timeBeforeWalking < 0)
        {
            Debug.Log("Change to Walking State");
            sm.ChangeState(sm.walkState);
            timeBeforeWalking = 5;
        }
        timeBeforeWalking -= 10*Time.deltaTime;
    }
    public void OnExit(testenemy sm)
    {
        Debug.Log("Bye Sleeping State");
    }
}
