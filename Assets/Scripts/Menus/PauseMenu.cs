using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public GameObject pauseMenu;

	bool isPaused = false;
	OptionsMenu optionsMenu;

	void Start() {
		optionsMenu = GetComponent<OptionsMenu>();
	}
	
    // Update is called once per frame
    void Update()
	{
		if (Input.GetButtonDown("Cancel"))
		{
			if (isPaused)
			{
				Continue();
			}
			else
			{
				Pause();
			}
		}
	}

	void Pause()
	{
		pauseMenu.SetActive(true);
		Time.timeScale = 0f;
        isPaused = true;

		if(optionsMenu) {
			optionsMenu.isActivated = true;
		}
	}

	public void Continue()
	{
		pauseMenu.SetActive(false);
		Time.timeScale = 1f;
        isPaused = false;

		if(optionsMenu) {
			optionsMenu.isActivated = false;
		}
	}

	public void Menu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(0);
	}
}
