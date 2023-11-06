using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CastRollController : MonoBehaviour
{
    public GameObject castText;
    public Canvas canvas;

    public float scrollSpeed = 100f;

    private float canvasWidth;
    private float canvasHeight;
    private Vector2 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        // 获取 Canvas 的 RectTransform 组件
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        // 获取 Canvas 的宽度和高度
        canvasWidth = canvasRect.sizeDelta.x;
        canvasHeight = canvasRect.sizeDelta.y;

        initialPosition = new Vector2(canvasWidth/2, -canvasHeight/2);
        castText.transform.position = new Vector3(initialPosition.x, initialPosition.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (initialPosition.y< canvasHeight/2+castText.GetComponent<RectTransform > ().sizeDelta.y)
        {
            initialPosition.y += scrollSpeed * Time.deltaTime;
            castText.transform.position = new Vector3(initialPosition.x, initialPosition.y, 0);
        }
        else
        {
            Debug.Log("end");
            //SceneManager.LoadScene("mainMenu");
        }
    }
}
