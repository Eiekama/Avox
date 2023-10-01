using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : IState
{
    Transform myTransform;
    float timeBeforeSleep;
    public void OnEntry(testenemy sm)
    {
        //myTransform = controller.transform;
        Debug.Log("In Walking State");
        timeBeforeSleep = 5;
    }
    public void OnUpdate(testenemy sm)
    {
        if(timeBeforeSleep < 0)
        {
            Debug.Log("Change to Sleep State");
            sm.ChangeState(sm.sleepState);
            timeBeforeSleep = 5;
        }
        timeBeforeSleep -= 10 * Time.deltaTime;
        //myTransform.position.x = myTransform.position.x + 10; 
    }
    public void OnExit(testenemy sm)
    {
        Debug.Log("Bye Walking State");
    }
}
