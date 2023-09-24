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
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Call start");
        startButton = transform.Find("StartButton").GetComponent<Button>();
        menuButton = transform.Find("SettingButton").GetComponent<Button>();
        quitButton = transform.Find("QuitButton").GetComponent<Button>();
        Debug.Log("Initial success");

        startButton.onClick.AddListener(StartGame);
        menuButton.onClick.AddListener(OpenMenu);
        quitButton.onClick.AddListener(QuitGame);
        Debug.Log("Add listener success");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void StartGame()
    {
        Debug.Log("JUMP");
        // 跳转到指定的场景
        SceneManager.LoadScene("Scene");
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
