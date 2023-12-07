using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BIdleState : IState
{
    public BossManager manager;

    public float speed = 300f;
    public float nextWaypointDistance = 1f;
    public Vector2 targetPosition;

    float _time = 3f;
    public float _timer;

    float _moveTime;
    private float _moveTimer;
    public float _limit = 10f;

    float _pauseTime = 1f;
    private float _pauseTimer;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;

    public void OnEntry()
    {
        Debug.Log("Idle");

        targetPosition = new Vector2(Random.Range(-_limit, _limit), manager.transform.position.y);
        seeker = manager.seeker;
        _moveTime = Random.Range(2, 4);
        _timer = 0.0f;
        _moveTimer = 0.0f;
        _pauseTimer = 0.5f;
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
        _pauseTimer += Time.deltaTime;

        if (_pauseTimer > _pauseTime)
        {
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

            Vector2 force = direction * speed * Time.deltaTime;

            manager.RB.AddForce(force);

            if (manager.RB.position.y - manager.target.position.y > 0.1f && Mathf.Abs(manager.RB.position.x - manager.target.position.x) > 1f)
            {
                manager.RB.AddForce(Vector2.down * 10);
            }

            float distance = Mathf.Abs(manager.RB.position.x - path.vectorPath[currentWaypoint].x);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }

            if (_moveTimer >= _moveTime)
            {
                float dist = manager.RB.position.x - manager.target.position.x;

                if (Random.Range(-1 + (dist - _limit) / (2 * _limit), 1 + (dist - _limit) / (2 * _limit)) < 0)
                {
                    manager.ChangeState(manager.chargeState);
                }
                else
                {
                    manager.ChangeState(manager.jumpState);
                }
            }
        }
    }
}

