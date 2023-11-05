using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    private Button startButton;
    private Button menuButton;
    private Button quitButton;
    public GameObject settingPanel;
    public GameObject cutScene;
    public GameObject Overlay;

    private Image _overlayImage;

    //private bool fadein = false;
    //private bool fadeout = false;
    //private float overlay_alaph;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Call start");
        startButton = transform.Find("StartButton").GetComponent<Button>();
        menuButton = transform.Find("SettingButton").GetComponent<Button>();
        quitButton = transform.Find("QuitButton").GetComponent<Button>();
        settingPanel.SetActive(false);
        Debug.Log("Initial success");

        startButton.onClick.AddListener(StartGame);
        menuButton.onClick.AddListener(OpenMenu);
        quitButton.onClick.AddListener(QuitGame);
        Debug.Log("Add listener success");

        _overlayImage = Overlay.GetComponent<Image>();
        _overlayImage.color = new Color(1, 1, 1, 0);
    }


    //private IEnumerator FadeInAndOut(bool isIn)
    //{

    //    if (isIn)
    //    {

    //        Overlay.SetActive(true);
    //        for (float t = 0; t < 1; t += Time.deltaTime * 10)
    //        {

    //            _overlayImage.color = new Color(1, 1, 1, t);
    //            //yield return null;

    //        }
    //        yield return new WaitForSeconds(1.0f);
    //        Debug.Log("Fade in!");
    //    }
    //    else
    //    {
    //        for (float t = 1; t > 0; t -= Time.deltaTime * 10)
    //        {
    //            Debug.Log("out");
    //            _overlayImage.color = new Color(1, 1, 1, t);
    //            //yield return null;
    //            yield return new WaitForSeconds(1.0f);
    //        }
    //        Debug.Log("Fade out!");
    //        Overlay.SetActive(false);
    //    }

    //    // cutScene.GetComponent<VideoPlayerController>().startVideo();
    //}

    private void Update()
    {
        //if (fadein)
        //{
        //    Overlay.SetActive(true);
        //    if (overlay_alaph < 1)
        //    {
        //        overlay_alaph += Time.deltaTime;
        //        _overlayImage.color = new Color(1, 1, 1, overlay_alaph);
        //    }
        //    else
        //    {
        //        fadein = false;
        //        fadeout = true;
        //        cutScene.GetComponent<VideoPlayerController>().startVideo();
        //        //gameObject.SetActive(false);
        //    }
        //}
        //else if (fadeout)
        //{
        //    if (overlay_alaph > 0)
        //    {
        //        overlay_alaph -= Time.deltaTime;
        //        _overlayImage.color = new Color(1, 1, 1, overlay_alaph);
        //    }
        //    else
        //    {
        //        fadeout = false;
        //        Overlay.SetActive(false);
        //        gameObject.SetActive(false);
        //    }
        //}

    }
    void StartGame()
    {
        Debug.Log("Start");
        //fadein = true;
        //overlay_alaph = 0;

        cutScene.GetComponent<VideoPlayerController>().startVideo();

    }


    void OpenMenu()
    {
        // 显示菜单界面
        gameObject.SetActive(false);
        settingPanel.SetActive(true);
    }

    void QuitGame()
    {
        Debug.Log("QUIT");
        // 退出游戏
        Application.Quit();
    }
}
