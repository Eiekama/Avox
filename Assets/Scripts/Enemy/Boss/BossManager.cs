using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossManager : AStateManager
{
    public readonly BJumpState jumpState = new BJumpState();
    public readonly BIdleState idleState = new BIdleState();
    public readonly BChargeState chargeState = new BChargeState();

    public Rigidbody2D RB { get; private set; }
    public Transform target;
    public Seeker seeker;
    public Path path;
    public float dist = 10f;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        RB = GetComponent<Rigidbody2D>();
        jumpState.manager = this;
        idleState.manager = this;
        chargeState.manager = this;

        startState = idleState;
    }
}
