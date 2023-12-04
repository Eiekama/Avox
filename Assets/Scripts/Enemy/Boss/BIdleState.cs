using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BIdleState : IState
{
    public BossManager manager;

    public float speed = 300f;

    float _moveTime = 1;
    private float _moveTimer;
    public float _limit = 10f;

    Path path;
    Seeker seeker;

    public void OnEntry()
    {
        _moveTimer = 0.0f;
    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {
        _moveTimer += Time.deltaTime;

        if (_moveTimer >= _moveTime)
        {
            if (Random.Range(-1, 1) < 0)
            {
                manager.ChangeState(manager.jumpState);
            }
            else
            {
                manager.ChangeState(manager.chargeState);
            }
        }


    }
}

