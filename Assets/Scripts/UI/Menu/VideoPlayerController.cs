using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class VideoPlayerController : MonoBehaviour
{

    private VideoPlayer videoPlayer;

    private Text hintText;
    private float hintDuration = 3.0f;
    private float hintTimer = 0.0f;

    private Slider skipSlider;
    private bool isSkiping = false;
    private float skipTime = 3.0f;
    private float currentProgress = 0.0f;



    public GameObject menuPanel;


    // Start is called before the first frame update

    void Start()
    {
        // Get current videoplayer
        videoPlayer = GetComponent<VideoPlayer>();
        hintText = GameObject.Find("HintText").GetComponent<Text>();
        if (hintText == null)
        {
            Debug.LogError("check object name");
        }
        skipSlider = GameObject.Find("skipSlider").GetComponent<Slider>();
        if (skipSlider == null)
        {
            Debug.LogError("Check object name");
        }
        skipSlider.gameObject.SetActive(false);


    }

    // Update is called once per frame

    void Update()
    {

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
        if (isSkiping&&currentProgress < 1.0f)
        {
            currentProgress += Time.deltaTime/ skipTime;
            // limit to 0 - 1
        }
        currentProgress = Mathf.Clamp01(currentProgress);
        // update value of Slider
        skipSlider.value = currentProgress;
    }

    public void skipVideo(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isSkiping = true;
            skipSlider.gameObject.SetActive(true);
        }
        else if (context.performed)
        {
            Debug.Log(context);
            videoPlayer.Stop();
            endReached(videoPlayer); // 触发视频结束事件
        }
        else if (context.canceled) {
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

    public void startVideo()
    {
        // 自动播放视频
        videoPlayer.Play();
        menuPanel.SetActive(false);
    }
    void endReached(VideoPlayer vp)
    {

        // 视频播放结束后的处理，例如加载下一个场景
        // 在这里你可以添加你的场景加载代码
        videoPlayer.enabled = false;
        hideHint();
        hintText.gameObject.SetActive(false);
        SceneManager.LoadScene("Scene");
    }
}
