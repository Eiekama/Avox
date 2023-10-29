using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FadeEffect : MonoBehaviour
{
    [SerializeField] private float FadeInTime = 0.5f;

    [SerializeField] private float FadeOutTime = 0.5f;
    public Coroutine Run(string textToType, TMP_Text textLabel)
    {
        textLabel.CrossFadeAlpha(0,0.0f,false);
        return StartCoroutine(fadein(textToType, textLabel));
    }

        public Coroutine Run2(string textToType, TMP_Text textLabel)
    {
        return StartCoroutine(fadeout(textToType, textLabel));
    }

private IEnumerator fadein(string textToType, TMP_Text textLabel)
    {
        textLabel.text = textToType;
        textLabel.CrossFadeAlpha(1,FadeInTime,false);
    yield return null;
    }

private IEnumerator fadeout(string textToType, TMP_Text textLabel)
    {
    textLabel.CrossFadeAlpha(0,FadeOutTime,false);
    yield return null;
    }
}
