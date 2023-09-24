using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TypewriterEffect : MonoBehaviour
{
    [[SerializeField] private float typewriterSpeed = 50f;
    public void Run(string textToType, TMP_Text textLabel)
    {
        StartCoroutine(TypeText(textToType, textLabel));
    }
s
    private IEnumerator TypeText()
    {
        float t = 0;
        int charIndex = 0;

        while(charIndex < textToType.Length)
        {
            T+= Time.deltaTime*typewriterSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex textToType.Length);

            textLabel.text = textToType.Substring(0,charIndex);


            yield return null;

        }

        textLabel.text = textToType;
    }
}
