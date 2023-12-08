using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DashState : IState
{
    public SlimeManager manager;

    public float speed = 50f;
    public float nextWaypointDistance = 1f;

    float _time = 3f;
    private float _timer;

    float _pauseTime = 0.5f;
    private float _pauseTimer;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;

    public void OnEntry()
    {
        path = manager.path;
        seeker = manager.seeker;
        _timer = 0.0f;
        _pauseTimer = 0.0f;
        manager.RB.velocity = Vector2.zero;
        UpdatePath();

        manager.anim.SetTrigger("attack");
    }

    public void OnExit()
    {

    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(manager.RB.position, manager.target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public void OnUpdate()
    {
        _timer += Time.deltaTime;
        _pauseTimer += Time.deltaTime;

        if (Mathf.Abs(manager.RB.velocity.x) > 0.1f && Mathf.Sign(manager.transform.localScale.x) != Mathf.Sign(manager.RB.velocity.x))
        {
            Vector3 scale = manager.transform.localScale;
            scale.x = Mathf.Sign(manager.RB.velocity.x);
            manager.transform.localScale = scale;
        }

        if (_pauseTimer > _pauseTime)
        {
            if (_timer > _time)
            {
                manager.ChangeState(manager.idleState);
                _timer = 0;
            }

            if (path == null)
            {
                return;
            }

            else if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)manager.transform.position).normalized;
            Vector2 force = direction * speed;
            manager.RB.AddForce(force);

            float distance = Vector2.Distance(manager.RB.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }  
    }
}