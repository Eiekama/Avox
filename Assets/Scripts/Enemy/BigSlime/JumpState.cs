using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class JumpState : IState
{
    public BigSlimeManager manager;

    public float speed = 7f;
    public float nextWaypointDistance = 1f;

    float _time = 0.5f;
    private float _timer;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;

    public void OnEntry()
    {
        Debug.Log("Jump State");

        // path = manager.path;
        _timer = 0.0f;
        manager.RB.velocity = Vector2.zero;
        seeker = manager.seeker;
        seeker.StartPath(manager.RB.position, manager.target.position, OnPathComplete);
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

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)manager.transform.position).normalized;

        Vector2 force = direction * speed;

        manager.RB.AddForce(force);

        if (Mathf.Abs(manager.RB.position.y - manager.target.position.y) < 0.1f)
        {
            manager.RB.AddForce(Vector2.up * speed * 30);
        }
        

        float distance = Vector2.Distance(manager.RB.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

    }
}

