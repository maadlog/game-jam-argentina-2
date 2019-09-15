using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class typing : MonoBehaviour
{
    public bool finish;
    public TextMeshProUGUI textDisplay;

    public string[] sentences;
    private int index;
    public float typeSpeed = 0.1f;

    public GameObject button;

    private void Start() 
    {
        StartCoroutine(Type());
        
    }
     private void Update() {
        if(textDisplay.text == sentences{index})
        {
            button.SetActive(true);
        }
    }

    public IEnumerator Type(){

        //yield return new WaitForSeconds(2);

        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typeSpeed);

        }

        
    }

    public void nextScentence()
    {
        if(index < sentences.Length-1){ //Si el conteo de caracteres del array ya llego a su maximo:
            index++; //Sumo uno al indice

            textDisplay.text = ""; //Imprimo un caracter vacio
            StartCoroutine(Type()); //Iniciar la corutina que typea
            finish = false; //Flag de que aparezca el boton, esto no funciona
            Debug.Log("Entre!");
        }

        else 
        {
            textDisplay.text = "";
            finish = true;
        }

    }
}
