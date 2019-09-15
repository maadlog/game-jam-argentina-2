using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uivida : MonoBehaviour
{
    public float porcentaje;
    public Image vida;
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        vida.fillAmount = porcentaje / 100;
    }
}
