using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class fade : MonoBehaviour
{
    public GameObject button; //objeto boton al que apunta
    public typing tecleo; //Objeto de la clase typewriter

    // Start is called before the first frame update
    void Start()
    {
       
    }

    private bool flag;
    // Update is called once per frame
    void Update()
    {
        
        flag = tecleo.finish;
        
            
        button.SetActive(flag); //La opacidad del boton es el maximo
     
    }
}
