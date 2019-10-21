using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    public GameObject mask;
    public GameObject hint;
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

    internal void SetRate(float proportion)
    {
        if (proportion >= 1)
        {
            mask.GetComponent<Image>().color = cappedColor;
            hint.transform.localScale = new Vector3(1, 1, 1);
        }
        if (proportion <= 0)
        {
            mask.GetComponent<Image>().color = originalColor;
            hint.transform.localScale = Vector3.zero;
        }

        var eulerAngle = proportion * -Mathf.PI;

        this.GetComponent<Image>().transform.rotation = Quaternion.Euler(0, 0, eulerAngle * Mathf.Rad2Deg);
    }

}
