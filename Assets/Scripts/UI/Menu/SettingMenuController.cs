using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenuController : MonoBehaviour
{
    public GameObject mainPanel;
    public AudioSource audioSource;
    //private Slider volumeSlider;

    private Button backToMainButton;
    private Toggle fullscreenToggle;
    private Slider volumeSlider;
    private Dropdown resolutionDropdown;

    // Start is called before the first frame update
    void Start()
    {
        backToMainButton= transform.Find("BackToMainButton").GetComponent<Button>();
        volumeSlider = transform.Find("VolumeSlider").GetComponent<Slider>();
        fullscreenToggle = transform.Find("FullScreenToggle").GetComponent<Toggle>();
        resolutionDropdown = transform.Find("ResolutionDropDown").GetComponent<Dropdown>();
        //start with false status


        backToMainButton.onClick.AddListener(BackToMain);

        volumeSlider.value = audioSource.volume;

        // 检查当前全屏状态并设置Toggle状态
        fullscreenToggle.isOn = false;
        fullscreenToggle.isOn = Screen.fullScreen;

        // 获取所有可用的分辨率选项
        Resolution[] resolutions = Screen.resolutions;

        // 清空Dropdown的选项列表
        resolutionDropdown.ClearOptions();

        // 创建分辨率选项列表
        List<Dropdown.OptionData> resolutionOptions = new List<Dropdown.OptionData>();
        foreach (Resolution resolution in resolutions)
        {
            string optionText = resolution.width + "x" + resolution.height;
            resolutionOptions.Add(new Dropdown.OptionData(optionText));
        }

        // 将分辨率选项添加到Dropdown中
        resolutionDropdown.AddOptions(resolutionOptions);

        gameObject.SetActive(false);
    }

    // Update is called once per frame

    void Update()
    {
        
    }

    void BackToMain()
    {
        Debug.Log("Back to main");
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

    public void SetResolution(int resolutionIndex)
    {
        // 获取选择的分辨率
        Resolution[] resolutions = Screen.resolutions;
        if (resolutionIndex >= 0 && resolutionIndex < resolutions.Length)
        {
            Resolution selectedResolution = resolutions[resolutionIndex];

            // 设置游戏分辨率
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        }
    }

    public void ToggleFullscreen()
    {
        // 切换全屏状态
        Screen.fullScreen = fullscreenToggle.isOn;
    }

}
