using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenuController : MonoBehaviour
{
    public GameObject mainPanel;
    //private Slider volumeSlider;

    private Button backToMainButton;
    // Start is called before the first frame update
    void Start()
    {
        backToMainButton= transform.Find("BackToMainButton").GetComponent<Button>();
        //start with false status


        backToMainButton.onClick.AddListener(BackToMain);

        gameObject.SetActive(false);
    }

    // Update is called once per frame

    void Update()
    {
        
    }
    void BackToMain()
    {
        mainPanel.SetActive(true);
        gameObject.SetActive(false);
    }
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;

        // 保存音量设置
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }
}
