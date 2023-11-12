using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class CastRollController : MonoBehaviour
{
    public GameObject castText;
    public Canvas canvas;

    public float scrollSpeed = 100f;

    private float canvasWidth;
    private float canvasHeight;
    private Vector2 initialPosition;

    public Text hintText;
    private float hintDuration = 3.0f;
    private float hintTimer = 0.0f;

    public Slider skipSlider;
    private bool isSkiping = false;
    private float skipTime = 3.0f;
    private float currentProgress = 0.0f;

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

        hintText.gameObject.SetActive(false);
        skipSlider.gameObject.SetActive(false);
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
            Debug.Log("Skip");
            //SceneManager.LoadScene("mainMenu");
        }


        if (hintTimer > 0)
        {
            hintTimer -= Time.deltaTime;
            if (hintTimer <= 0)
            {
                hideHint();
            }
        }

        // 在Update函数中更新进度条的值
        if (!isSkiping)
        {
            currentProgress = 0;
        }
        if (isSkiping && currentProgress < 1.0f)
        {
            currentProgress += Time.deltaTime / skipTime;
            // limit to 0 - 1
        }
        currentProgress = Mathf.Clamp01(currentProgress);
        // update value of Slider
        skipSlider.value = currentProgress;

    }
    public void EndCastRoll(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isSkiping = true;
            skipSlider.gameObject.SetActive(true);
        }
        else if (context.performed)
        {
            Debug.Log("Skip");
            //SceneManager.LoadScene("mainMenu");
            // 触发视频结束事件
        }
        else if (context.canceled)
        {
            isSkiping = false;
            skipSlider.gameObject.SetActive(false);
        }
    }

    public void showHint()
    {
        if (hintText != null)
        {
            hintText.text = "Press Space to skip";
            hintText.gameObject.SetActive(true);
            hintTimer = hintDuration;
        }
    }

    void hideHint()
    {
        hintText.gameObject.SetActive(false);
    }
}
