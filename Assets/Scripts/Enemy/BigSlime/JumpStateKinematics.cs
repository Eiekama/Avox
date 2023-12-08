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

    float _pauseTime = 0.67f;
    float _pauseTimer;
    bool jumped;

    public void OnEntry()
    {
        Debug.Log("Jump State");

        _gravity = Mathf.Abs(Physics.gravity.y);
        _xdist = (manager.target.position.x - manager.RB.position.x) * 0.7f;
        _time = Mathf.Sqrt(2 * _ydist / _gravity);
        _xspeed = _xdist / _time;
        _yspeed = _gravity * _time;

        _timer = 0;
        _pauseTimer = 0;

        manager.RB.velocity = Vector2.zero;
        jumped = false;

        manager.anim.SetTrigger("attack");
    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {
        manager.anim.SetFloat("xSpeed", Mathf.Abs(manager.RB.velocity.x));
        manager.anim.SetFloat("yVelocity", manager.RB.velocity.y);

        if (Mathf.Abs(manager.RB.velocity.x) > 0.1f && Mathf.Sign(manager.transform.localScale.x) != Mathf.Sign(manager.RB.velocity.x))
        {
            Vector3 scale = manager.transform.localScale;
            scale.x = 1.3182f * Mathf.Sign(manager.RB.velocity.x);
            manager.transform.localScale = scale;
        }

        _pauseTimer += Time.deltaTime;

        if (_pauseTimer > _pauseTime)
        {
            _timer += Time.deltaTime;

            if (jumped)
            {
                if (_timer > 2 * _time)
                {
                    manager.RB.velocity = Vector2.zero;
                    manager.ChangeState(manager.idleState);
                }

                manager.RB.velocity = new Vector2(_xspeed, manager.RB.velocity.y);
            }
            else
            {
                manager.RB.velocity = new Vector2(_xspeed, _yspeed);
                jumped = true;
            }   
        }

    }
}

