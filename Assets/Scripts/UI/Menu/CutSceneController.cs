using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutSceneController : MonoBehaviour
{
    public Text hintText;
    private float hintDuration = 3.0f;
    private float hintTimer = 0.0f;

    public Slider skipSlider;
    private bool isSkiping = false;
    private float skipTime = 3.0f;
    private float currentProgress = 0.0f;

    void Start()
    {
        //Initialize slider and hint invisiable
        hintText.gameObject.SetActive(false);
        skipSlider.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        if (hintTimer > 0)
        {
            hintTimer -= Time.deltaTime;
            if (hintTimer <= 0)
            {
                hideHint();
            }
        }

        // Update value of Slider
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

    public void SkipCutScene(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isSkiping = true;
            skipSlider.gameObject.SetActive(true);
        }
        else if (context.performed)
        {
            Debug.Log("Skip");
            SceneManager.LoadScene("Room1");
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
            hintText.text = "Hold Space to skip";
            hintText.gameObject.SetActive(true);
            hintTimer = hintDuration;
        }
    }

    void hideHint()
    {
        hintText.gameObject.SetActive(false);
    }
}
