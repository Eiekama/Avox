/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JRightState : IState
{
    public BigSlimeManager manager;

    [SerializeField] float _speed = 1.0f;
    [SerializeField] float _time = 3.0f;
    private float _timer;

    public void OnEntry()
    {
        _timer = 0.0f;
    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {
        manager.transform.position = manager.transform.position + _speed * Time.deltaTime * Vector3.right;
        _timer += Time.deltaTime;

        float dist = manager.target.position.x - manager.RB.position.x;

        if (0 < dist && dist < manager.dist)
        {
            manager.ChangeState(manager.jumpState);
        }

        if (_timer > _time)
        {
            manager.ChangeState(manager.moveLeftState);
        }
    }
}
*/
