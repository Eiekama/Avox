using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class JumpStateKinematics : IState
{
    public BigSlimeManager manager;

    float _xspeed;
    float _yspeed;
    float _xdist;
    float _ydist = 5;
    float _time;
    float _gravity;
    private float _timer;

    public void OnEntry()
    {
        Debug.Log("Jump State");

        _gravity = Mathf.Abs(Physics.gravity.y);
        _xdist = manager.target.position.x - manager.RB.position.x;
        _time = Mathf.Sqrt(2 * _ydist / _gravity);
        _xspeed = _xdist / _time;
        _yspeed = _gravity * _time;

        manager.RB.velocity = new Vector2(_xspeed, _yspeed);
    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {
        Debug.Log("Jump State Kinematics");

        _timer += Time.deltaTime;

        if (_timer > 2 * _time)
        {
            _timer = 0;
            manager.RB.velocity = Vector2.zero;
            manager.ChangeState(manager.idleState);
        }

        manager.RB.velocity = new Vector2(_xspeed, manager.RB.velocity.y);

    }
}

