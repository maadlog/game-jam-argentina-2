using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	Manager manager;

	void Awake() {
		manager = Manager.GetInstance();;
	}

	public void PlayGame(int players)
	{
		manager.Score = 0;
		manager.Players = players;
		SceneManager.LoadScene(1);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}