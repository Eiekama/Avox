using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float typewriterSpeed = 50f;
    public Coroutine Run(string textToType, TMP_Text textLabel)
    {
        return StartCoroutine(TypeText(textToType, textLabel));
    }
    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        textLabel.text = string.Empty;
        float T = 0;
        int charIndex = 0;

        while(charIndex < textToType.Length)
        {
            T+= Time.deltaTime*typewriterSpeed;
            charIndex = Mathf.FloorToInt(T);
            charIndex = Mathf.Clamp(charIndex,0 ,textToType.Length);

            textLabel.text = textToType.Substring(0,charIndex);


            yield return null;

        }

        textLabel.text = textToType;
    }
}
