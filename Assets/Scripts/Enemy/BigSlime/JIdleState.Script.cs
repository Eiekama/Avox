using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class JIdleState : IState
{
    public BigSlimeManager manager;

    public float speed = 50f;
    public float nextWaypointDistance = 1f;
    public Vector2 targetPosition;

    float _time = 3f;
    private float _timer;

    float _moveTime = 1;
    private float _moveTimer;
    float _limit = 10f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;

    public void OnEntry()
    {
        Debug.Log("Idle State");

        targetPosition = new Vector2(Random.Range(-_limit, _limit), manager.transform.position.y);
        seeker = manager.seeker;
        _timer = 0.0f;
        _moveTimer = 0.0f;
        UpdatePath();
    }

    public void OnExit()
    {

    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(manager.RB.position, targetPosition, OnPathComplete);
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
        Debug.Log("Idle State");

        _timer += Time.deltaTime;
        _moveTimer += Time.deltaTime;

        if (_timer > _time)
        {
            targetPosition = new Vector2(Random.Range(-_limit, _limit), manager.transform.position.y);
            UpdatePath();
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

        Vector2 position = new Vector2(manager.transform.position.x, manager.transform.position.y);
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - position).normalized;

        Vector2 force = direction * speed * Time.deltaTime;

        manager.RB.AddForce(force);


        float distance = Vector2.Distance(manager.RB.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (_moveTimer >= _moveTime)
        {
            float dist = manager.RB.position.x - manager.target.position.x;
            float targetDist = manager.RB.position.x - targetPosition.x;

            if (Mathf.Abs(dist) < manager.dist && dist * targetDist > 0)
            {
                manager.ChangeState(manager.jumpState);
            }
            _moveTimer = 0;
        }


    }
}

