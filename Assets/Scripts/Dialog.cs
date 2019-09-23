using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialog : MonoBehaviour
{
    #region Typing:
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typeSpeed = 0.01f;
    public GameObject continueButton;
    public GameObject GeneralTank;

   // public Animatior talking;
   
    #endregion

    #region Audio Managment:
    public List<AudioClip> Typing;
    public AudioClip typingSound;
    private AudioSource audioSource;
    #endregion
    private void Start()
    {

        StartCoroutine(Type()); //Inicio la coroutine de escribir
       
        audioSource = gameObject.GetComponent<AudioSource>(); //Obtengo el audio source del gameobject(en este caso es el textManager)
        
    }
    private void Update() 
    {
        if(textDisplay.text == sentences[index]) //si el texto llego al final de los caracteres que tenia la oracion, me detengo
        {
            continueButton.SetActive(true); //Activo el boton(Feature a prueba de ansiosos)
           GeneralTank.gameObject.GetComponent<Animator>().enabled = false; //Detengo la animacion(Esto lo para en cualquier frame, hay que cambiarlo)
        } 
    }
    IEnumerator Type() //Coroutine que escribe caracter por caracter.
    { //Parte del codigo que escribe
          GeneralTank.gameObject.GetComponent<Animator>().enabled = true; //Hago que comience la animacion del genral Tank Hablando

        foreach (char letter in sentences[index].ToCharArray()) //Por cada caracter en las oraciones: 
        {
            #region Escritura: 
            textDisplay.text += letter; // voy agregando de a un caracter al textMeshPro
            yield return new WaitForSeconds(typeSpeed); //Le doy una velocidad de typeo

            #endregion

            
            #region Reproduccion de sonidos
         int AudioIndex = Random.Range(0, Typing.Count-1); //Indico un indice aleatorio para que se reproduzcan sonuidos

         typingSound = Typing[AudioIndex];
         audioSource.clip = typingSound;
         audioSource.Play();
        #endregion
            
        }
 
    }
    public void NextSentence()
    {  
        if(index < sentences.Length -1)
        {
            GeneralTank.gameObject.GetComponent<Animator>().PlayInFixedTime("general", 0);
            
            index++;
             textDisplay.text = "";
             StartCoroutine(Type());

        }

        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);

        }
       
    }

    
}
