using System;
using UnityEngine;
using UnityEngine.UI;

public class HeatIndicatorHint : MonoBehaviour
{
    internal void SetRate(float proportion)
    {
        if (proportion >= 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (proportion <= 0)
        {
            transform.localScale = Vector3.zero;
        }
    }
}