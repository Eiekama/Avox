using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BChargeState : IState
{
    public BossManager manager;

    public float speed = 5f;
    public float nextWaypointDistance = 1f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    float _initialPlayerPos;
    float _fixedDist = 2;
    bool right;

    float _time = 3f;
    private float _timer;

    float _pauseTime = 1f;
    private float _pauseTimer;

    Seeker seeker;

    public void OnEntry()
    {
        manager.RB.velocity = Vector2.zero;

        path = manager.path;
        seeker = manager.seeker;

        _pauseTimer = 0.0f;
        _timer = 0.0f;

        _initialPlayerPos = manager.target.position.x;
        float dist = manager.target.position.x - manager.RB.position.x;

        if (dist < 0)
        {
            right = false;
        }
        else
        {
            right = true;
        }

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
        _pauseTimer += Time.deltaTime;

        if (_pauseTimer > _pauseTime)
        {
            _timer += Time.deltaTime;

            if ((right && manager.RB.position.x > _initialPlayerPos + _fixedDist)
             || (!right && manager.RB.position.x < _initialPlayerPos - _fixedDist)
             || _timer > _time)
            {
                manager.RB.velocity = Vector2.zero;
                manager.ChangeState(manager.idleState);
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
            Vector2 force = (new Vector2(direction.x, 0)).normalized * speed;

            if ((right && manager.RB.position.x < _initialPlayerPos)
             || (!right && manager.RB.position.x > _initialPlayerPos))
            {
                manager.RB.AddForce(force);
            }

            float distance = Vector2.Distance(manager.RB.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }

    }
}