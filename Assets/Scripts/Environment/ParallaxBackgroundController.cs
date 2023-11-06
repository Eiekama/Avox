using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackgroundController : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxBackgroundElement
    {
        public GameObject element;
        [Tooltip("The strength of the parallax effect, [0,1).\n0 will have no effect.\n1 will lock the background to the camera.")]
        public Vector2 effectScale;

        [HideInInspector]
        public Vector3 origin;
    }

    public List<ParallaxBackgroundElement> parallaxBackgroundElements;

    // Start is called before the first frame update
    void Start()
    {
        // align based on starting position
        Vector3 camPos = Camera.main.transform.position;

        foreach (ParallaxBackgroundElement e in parallaxBackgroundElements)
        {
            e.origin = new Vector3(
                (e.element.transform.position.x - e.effectScale.x * camPos.x) / (1 - e.effectScale.x),
                (e.element.transform.position.y - e.effectScale.y * camPos.y) / (1 - e.effectScale.y)
            );
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 camPos = Camera.main.transform.position;

        foreach (ParallaxBackgroundElement e in parallaxBackgroundElements)
        {
            Vector3 posOffset = camPos - e.origin;

            e.element.transform.position = e.origin + new Vector3(
                posOffset.x * e.effectScale.x, posOffset.y * e.effectScale.y);
        }
    }
}