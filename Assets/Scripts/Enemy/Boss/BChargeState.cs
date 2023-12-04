using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BChargeState : IState
{
    public BossManager manager;

    public float speed = 50f;
    public float nextWaypointDistance = 1f;

    float _time = 1.5f;
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
        manager.RB.AddForce(new Vector2(force.x, 0));

        float distance = Vector2.Distance(manager.RB.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

    }
}