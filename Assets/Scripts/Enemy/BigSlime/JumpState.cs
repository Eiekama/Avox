using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class JumpState : IState
{
    public BigSlimeManager manager;

    public float speed = 50f;
    public float nextWaypointDistance = 1f;

    float _time = 3f;
    private float _timer;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;

    public void OnEntry()
    {
        path = manager.path;
        seeker = manager.seeker;
        _timer = 0.0f;
        UpdatePath();
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
        Debug.Log("Jump State");

        _timer += Time.deltaTime;

        if (_timer > _time)
        {
            manager.ChangeState(manager.idleState);
            _timer = 0;
        }

        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 position = new Vector2(manager.transform.position.x, manager.transform.position.y);
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - position).normalized;

        Vector2 force = direction * speed; //* Time.deltaTime;

        manager.RB.AddForce(force);

        if (Mathf.Abs(manager.RB.position.y - manager.target.position.y) < 0.1f)
        {
            manager.RB.AddForce(Vector2.up * 800, ForceMode2D.Impulse);
        }

        float distance = Vector2.Distance(manager.RB.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}

