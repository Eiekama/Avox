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
        // 获取现有的VideoPlayer组件
        videoPlayer = GetComponent<VideoPlayer>();
        hintText = GameObject.Find("HintText").GetComponent<Text>();
        if (hintText == null)
        {
            Debug.LogError("找不到提示文本对象，请确保对象名称正确");
        }
        skipSlider = GameObject.Find("skipSlider").GetComponent<Slider>();
        if (skipSlider == null)
        {
            Debug.LogError("找不到提示文本对象，请确保对象名称正确");
        }
        skipSlider.gameObject.SetActive(false);

    }

    // Update is called once per frame

    void Update()
    {
        //// 检测空格键并提前结束视频
        //if (videoStarted && Input.GetKeyDown(KeyCode.Space))
        //{
        //    videoPlayer.Stop();
        //    videoStarted = false;
        //    EndReached(videoPlayer); // 触发视频结束事件
        //}
        // 检测除空格键以外的按键

        //if (videoStarted && Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Space))
        //{
        //    ShowHint();
        //}
        // 更新提示计时器

        if (hintTimer > 0)
        {
            hintTimer -= Time.deltaTime;
            if (hintTimer <= 0)
            {
                hideHint();
            }
        }

        // 在Update函数中更新进度条的值
        //清空
        if (!isSkiping)
        {
            currentProgress = 0;
        }
        if (isSkiping&&currentProgress < 1.0f)
        {
            currentProgress += Time.deltaTime/ skipTime;
            // 限制进度在0到1之间
        }
        currentProgress = Mathf.Clamp01(currentProgress);
        // 更新进度条的值
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
