using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyManager : AStateManager
{
    public readonly MoveLeftState moveLeftState = new MoveLeftState();
    public readonly MoveRightState moveRightState = new MoveRightState();

    private void Awake()
    {
        moveLeftState.manager = this;
        moveRightState.manager = this;

        startState = moveRightState;
    }
}
