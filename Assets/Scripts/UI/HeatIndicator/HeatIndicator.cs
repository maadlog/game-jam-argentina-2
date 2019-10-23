using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatIndicator : MonoBehaviour
{
    public enum Position
    {
        Left, Right
    }
    HeatIndicatorMask mask;
    HeatIndicatorHint hint;
    HeatIndicatorArrow arrow;
    // Start is called before the first frame update
    void Start()
    {
        mask = GetComponentInChildren<HeatIndicatorMask>();
        hint = GetComponentInChildren<HeatIndicatorHint>();
        arrow = GetComponentInChildren<HeatIndicatorArrow>();
    }

    internal void SetRate(float proportion)
    {
        mask.SetRate(proportion);
        hint.SetRate(proportion);
        arrow.SetRate(proportion);
        
    }

}
