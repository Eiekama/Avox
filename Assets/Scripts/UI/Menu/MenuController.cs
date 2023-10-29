using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Sprite fullscreenSprite;
    public Sprite windowSprite;
    public Image fullscreenIcon;
    public Slider volume;
    public Text volumeText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startGame()
    {
        SceneManager.LoadScene(sceneName: "collectibleTestScene");
    }
}
