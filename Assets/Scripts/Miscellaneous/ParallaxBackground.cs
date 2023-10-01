using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Tooltip("The strength of the parallax effect, [0,1).\n0 will have no effect.\n1 will lock the background to the camera.")]
    public Vector2 effectScale;

    Vector3 _origin;

    // Start is called before the first frame update
    void Start()
    {
        // align based on starting position
        Vector3 camPos = Camera.main.transform.position;

        _origin = new Vector3(
            (transform.position.x - effectScale.x * camPos.x) / (1 - effectScale.x),
            (transform.position.y - effectScale.y * camPos.y) / (1 - effectScale.y)
        );
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 camPos = Camera.main.transform.position;
        Vector3 posOffset = camPos - _origin;

        transform.position = _origin + new Vector3(posOffset.x * effectScale.x, posOffset.y * effectScale.y);
    }
}