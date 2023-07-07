using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private IMovementService _movement;

    private Vector2 _moveInput;

    private void Awake()
    {
        _movement = ServiceLocator.Instance.GetService<IMovementService>();
        _movement.RB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");
    }
}
