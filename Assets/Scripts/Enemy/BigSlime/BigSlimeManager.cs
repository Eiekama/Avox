using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BigSlimeManager : AStateManager
{
    public readonly JumpStateKinematics jumpState = new JumpStateKinematics();
    public readonly JIdleState idleState = new JIdleState();

    public Rigidbody2D RB { get; private set; }
    public Transform target;
    public Seeker seeker;
    public Path path;
    public float dist = 10f;

    public Animator anim { get; private set; }

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        RB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpState.manager = this;
        idleState.manager = this;

        startState = idleState;
    }
}
