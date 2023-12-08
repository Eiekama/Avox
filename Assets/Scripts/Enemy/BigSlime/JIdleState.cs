using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class JIdleState : IState
{
    public BigSlimeManager manager;

    public float speed = 100f;
    public float nextWaypointDistance = 1f;
    public Vector2 targetPosition;

    float _time = 3f;
    public float _timer;

    float _moveTime = 0;
    private float _moveTimer;
    public float _limit = 10f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;

    public void OnEntry()
    {
        seeker = manager.seeker;
        _timer = Random.Range(0f, _time);
        _moveTimer = 0.0f;
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
        manager.anim.SetFloat("xSpeed", Mathf.Abs(manager.RB.velocity.x));
        manager.anim.SetFloat("yVelocity", manager.RB.velocity.y);

        if (Mathf.Abs(manager.RB.velocity.x) > 0.1f && Mathf.Sign(manager.transform.localScale.x) != Mathf.Sign(manager.RB.velocity.x))
        {
            Vector3 scale = manager.transform.localScale;
            scale.x = 1.3182f * Mathf.Sign(manager.RB.velocity.x);
            manager.transform.localScale = scale;
        }

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
            float ydist = manager.RB.position.y - manager.target.position.y;
            float targetDist = manager.RB.position.x - targetPosition.x;

            if (Mathf.Abs(dist) < manager.dist && Mathf.Abs(ydist) < 2f && dist * targetDist >= 0)
            {
                manager.ChangeState(manager.jumpState);
                _moveTimer = 0;
            }
        }


    }
}

