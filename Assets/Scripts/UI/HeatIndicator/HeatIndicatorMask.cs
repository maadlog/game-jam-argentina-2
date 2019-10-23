using System;
using UnityEngine;
using UnityEngine.UI;

public class HeatIndicatorMask: MonoBehaviour
{
    Color cappedColor = new Color(0.88f, 0.01f, 0.01f);
    Color originalColor;

    Image image;

    void Start()
    {
        image = GetComponent<Image>();
        originalColor = image.color;
    }

    internal void SetRate(float proportion)
    {
        if (proportion >= 1)
        {
            image.color = cappedColor;
        }
        if (proportion <= 0)
        {
            image.color = originalColor;
        }
    }
}
