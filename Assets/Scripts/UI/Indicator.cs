using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        originalColor = this.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Color cappedColor = new Color(0.88f, 0.01f, 0.01f);
    Color originalColor;
    //Helix shape means that starts at about 60º as the "0"
    // (Pixel perfect, it starts at the 0.18 fill amount)
    // If 360 -> 1, then x -> 0.18 -> x = 64.8

    // Full Shape -> 2PI
    // Partial -> (2PI - 64.8*Math.Deg2Rad)

    // With prop = 0 => 64.8f * Mathf.Deg2Rad / 2PI = 0 * fixingConst + 64.8f * Mathf.Deg2Rad / 2PI
    // with prop = 1 => 1 = 1 * MyConst + 64.8f * Mathf.Deg2Rad / 2PI
    //  =>> fixingConstant = 1 - (64.8f * Mathf.Deg2Rad/2pi)
    // proportionFixed = proportion * (1 - fixingConstant) + fixingConstant;
    float fixingConstant = 64.8f * Mathf.Deg2Rad / (Mathf.PI * 2);

    internal void SetRate(float proportion)
    {
        if (proportion >= 1)
        {
            this.GetComponent<Image>().color = cappedColor;
        }
        if (proportion <= 0)
        {
            this.GetComponent<Image>().color = originalColor;
        }

        var proportionFixed = proportion * (1 - fixingConstant) + fixingConstant;

        this.GetComponent<Image>().fillAmount = proportionFixed;
    }

}
