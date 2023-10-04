using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class TestMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rb2d.AddForce(Vector2.up * 2f, ForceMode2D.Impulse);
        }
    }
    
    public void run(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<float>() + "Ran");
        transform.Translate(context.ReadValue<float>(), 0, 0);
    }

    
    
}
