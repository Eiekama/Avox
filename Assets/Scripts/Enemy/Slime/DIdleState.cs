using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DIdleState : IState
{
    public SlimeManager manager;

    public float nextWaypointDistance = 1f;

    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Path path;
    Seeker seeker;

    // determines when random target is updated
    float _time = 3f;
    public float _timer;
    public float _limit = 5f;
    public Vector2 targetPosition;

    // determines when enemy can change state
    float _dashTime = 0;
    private float _dashTimer;

    public void OnEntry()
    {
        targetPosition = new Vector2(manager.transform.position.x + Random.Range(-_limit, _limit), manager.transform.position.y);
        seeker = manager.seeker;
        _timer = Random.Range(0f, _time);
        _dashTimer = 0.0f;
        UpdatePath();
    }

    public void OnExit()
    {

    }

    public void UpdatePath()
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
        _timer += Time.deltaTime;
        _dashTimer += Time.deltaTime;

        if (_timer > _time)
        {
            targetPosition = new Vector2(manager.transform.position.x + Random.Range(-_limit, _limit), manager.transform.position.y);
            UpdatePath();
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
        
        // determines whether to go to next waypoint
        float distance = Vector2.Distance(manager.RB.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // determines whether state should be changed to dash state
        if (_dashTimer >= _dashTime)
        {
            float dist = manager.RB.position.x - manager.target.position.x;
            float targetDist = manager.RB.position.x - targetPosition.x;

            if (Mathf.Abs(dist) < manager.dist && dist * targetDist > 0)
            {
                manager.ChangeState(manager.dashState);
            }
            _dashTimer = 0;
        }

        
    }
}

