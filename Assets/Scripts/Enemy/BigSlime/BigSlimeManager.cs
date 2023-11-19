using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BigSlimeManager : AStateManager
{
    public readonly JumpState jumpState = new JumpState();
    public readonly JIdleState idleState = new JIdleState();

    public Rigidbody2D RB { get; private set; }
    public Transform target;
    public Seeker seeker;
    public Path path;
    public float dist = 3f;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        RB = GetComponent<Rigidbody2D>();
        jumpState.manager = this;
        idleState.manager = this;

        startState = idleState;
    }
}
