using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoController : MonoBehaviour
{
    float speed = 10;
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.position = transform.position + new Vector3 (horizontalInput, 0, 0)*speed*Time.deltaTime;
    }
}
