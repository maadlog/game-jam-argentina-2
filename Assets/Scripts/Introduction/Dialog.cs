using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// This class serves as game manager and dialog coordinator for the "Introduction" scene.
// Please refactor soon to separate concerns and enable reutilization of the Dialog component (TextMeshPro)
public class Dialog : MonoBehaviour
{
	#region Typing:
	public TextMeshProUGUI textDisplay;
	public string[] sentences;
	private int index;
	public float typeSpeed = 0.01f;
	public GameObject continueButton;
	public GameObject GeneralTank;

	#endregion

	#region Audio Managment:
	public List<AudioClip> Typing;
	public AudioClip typingSound;
	private AudioSource audioSource;
    public AudioClip music;
   
	#endregion

	LevelFader levelFader;

	private void Start()
	{
		levelFader = LevelFader.GetInstance(this.gameObject);
		levelFader.Invisible();

		GeneralTankShouldBeSpeaking = true;
		activeSentence = StartCoroutine(Type()); //Inicio la coroutine de escribir

		audioSource = gameObject.GetComponent<AudioSource>(); //Obtengo el audio source del gameobject(en este caso es el textManager)

	}
	private bool GeneralTankIsSpeaking;
	private bool GeneralTankShouldBeSpeaking;
	private void Update()
	{
		if (GeneralTankShouldBeSpeaking && !GeneralTankIsSpeaking)
		{
			GeneralTankIsSpeaking = true;
			GeneralTank.GetComponent<Animator>().Play("Speaking");
            audioSource.clip = music;
            audioSource.Play();
		}

		if (Input.anyKeyDown)
		{
			if (continueButton.activeSelf)
			{
				NextSentence();
			}
			continueButton.SetActive(true);
		}

		if (textDisplay.text == sentences[index]) //si el texto llego al final de los caracteres que tenia la oracion, me detengo
		{
			continueButton.SetActive(true); //Activo el boton(Feature a prueba de ansiosos)

			GeneralTankShouldBeSpeaking = false;
			GeneralTankIsSpeaking = false;
			GeneralTank.GetComponent<Animator>().Play("Idle");
		}
	}


	IEnumerator Type() //Coroutine que escribe caracter por caracter.
	{ //Parte del codigo que escribe

		foreach (char letter in sentences[index].ToCharArray()) //Por cada caracter en las oraciones: 
		{
			#region Escritura: 
			textDisplay.text += letter; // voy agregando de a un caracter al textMeshPro
			yield return new WaitForSeconds(typeSpeed); //Le doy una velocidad de typeo
			#endregion


			#region Reproduccion de sonidos
			int AudioIndex = Random.Range(0, Typing.Count - 1); //Indico un indice aleatorio para que se reproduzcan sonuidos

			typingSound = Typing[AudioIndex];
			audioSource.clip = typingSound;
			audioSource.Play();
			#endregion
		}

	}

	/*Esto hace que cuando este activo el boton y sea presionado, se aumente el indice del array
    de oraciones y pase al siguiente texto
     */
	Coroutine activeSentence;
	public void NextSentence()
	{
		if (index < sentences.Length - 1) //Si el indice es menor que la cantidad de oraciones -1
		{
			if (activeSentence != null)
			{
				StopCoroutine(activeSentence);
			}
			index++; //Sumo uno al indice
			textDisplay.text = ""; //Imprimo un caracter vacio

			GeneralTankShouldBeSpeaking = true; //Hago que comience la animacion del genral Tank Hablando

			activeSentence = StartCoroutine(Type()); //Y comieno la rutina de escribir

			if (index == sentences.Length - 1)
			{
				continueButton.GetComponent<TextMeshProUGUI>().text = "Start!";
			}
		}
		else
		{
			textDisplay.text = ""; //Sino, imprimo un texto vacio
			activeSentence = null;
			continueButton.SetActive(false); //Y no activo el boton todavia.

			EndedPresentation();
		}

	}

	private void EndedPresentation()
	{
		GeneralTankShouldBeSpeaking = false;
		GeneralTankIsSpeaking = false;
		GeneralTank.GetComponent<Animator>().Play("Idle");
		levelFader.FadeIn();
        audioSource.Stop();
	}

	public void FinishedFadeIn()
	{
		SceneManager.LoadScene(2); // Load level 1 (Build index 2)
	}
}
