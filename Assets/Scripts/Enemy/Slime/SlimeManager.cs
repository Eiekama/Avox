using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SlimeManager : AStateManager
{
    public readonly DashState dashState = new DashState();
    public readonly DIdleState idleState = new DIdleState();
    public IState nextState;

    public Rigidbody2D RB { get; private set; }
    public Transform target;
    public Seeker seeker;
    public Path path;
    public float dist = 5f;

    public Animator anim { get; private set; }

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        RB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        dashState.manager = this;
        idleState.manager = this;

        startState = idleState;
    }
}
