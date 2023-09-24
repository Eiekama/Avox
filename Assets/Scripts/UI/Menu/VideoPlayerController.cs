using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoPlayerController : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private bool videoStarted = false;

    private Text hintText;
    private float hintDuration = 3.0f;
    private float hintTimer = 0.0f;

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
        // 自动播放视频
        videoPlayer.Play();
        videoStarted = true;

        menuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 检测空格键并提前结束视频
        if (videoStarted && Input.GetKeyDown(KeyCode.Space))
        {
            videoPlayer.Stop();
            videoStarted = false;
            EndReached(videoPlayer); // 触发视频结束事件
        }
        // 检测除空格键以外的按键
        if (videoStarted && Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Space))
        {
            
            ShowHint();
        }
        // 更新提示计时器
        if (hintTimer > 0)
        {
            hintTimer -= Time.deltaTime;
            if (hintTimer <= 0)
            {
                HideHint();
            }
        }
    }
    void ShowHint()
    {
        Debug.Log("Hint");
        hintText.text = "Press Space to skip";
        hintText.gameObject.SetActive(true);
        hintTimer = hintDuration;
    }

    void HideHint()
    {
        hintText.gameObject.SetActive(false);
    }
    void EndReached(VideoPlayer vp)
    {

        // 视频播放结束后的处理，例如加载下一个场景
        // 在这里你可以添加你的场景加载代码
        videoPlayer.enabled = false;
        HideHint();
        hintText.gameObject.SetActive(false);

        menuPanel.SetActive(true);
    }
    void 
}
