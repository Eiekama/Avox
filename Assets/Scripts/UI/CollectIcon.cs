using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PrimeTween;

public class CollectIcon : MonoBehaviour
{
    public TMP_Text[] texts;
    public SpriteRenderer[] images;

    Vector3 origin;

    private void Start()
    {
        origin = transform.position;
        ChangeAppearance(false, 0);
    }

    public void ChangeAppearance(bool show, float duration = 0.25f)
    {
        foreach (TMP_Text t in texts)
            Sequence.Create().Group(Tween.Alpha(t, startValue: show ? 0 : 1, endValue: show ? 1 : 0, duration: duration));

        foreach (SpriteRenderer t in images)
            Sequence.Create().Group(Tween.Alpha(t, startValue: show ? 0 : 1, endValue: show ? 1 : 0, duration: duration));

        Sequence.Create().Group(Tween.Position(transform,
            startValue: show ? origin + Vector3.up * 1.5f : origin,
            endValue: !show ? origin + Vector3.up * 1.5f : origin,
            duration: duration));
    }
}
