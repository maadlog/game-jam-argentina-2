using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public float timeToMoveToMenu = 2f;
	
	public Text lostText;
	public Text CounterText;

	int score = 0;
	int refugees = 0;
	int max_refugees = 15;
	static GameManager gameManager;
	float menuTimer;
	bool activateFadeOff = false;
	GameObject fadeOff;
	public AudioSource explotionSound;
	public AudioSource enemyDeathSound;
	// Start is called before the first frame update
	void Awake()
	{
		// set time to timer
		menuTimer = timeToMoveToMenu;

		// make it singleton
		if (gameManager == null)
		{
			gameManager = this;
			// DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(this);
		}

		UpdateScore(0);
		lostText.enabled = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (activateFadeOff)
		{

		}
	}

	public static GameManager getGameManager()
	{
		return gameManager;
	}

	public void UpdateScore(int score)
	{
		this.score += score;
	}

	public void ChangeLevel()
	{
		SceneManager.LoadScene(GameObject.FindObjectOfType<NextLevel>().nextLevel);
	}

	public void LostLevel()
	{

		// show message
		lostText.enabled = true;

		// player cant move

		// go to menu after a while
		if (menuTimer < 0)
		{
			SceneManager.LoadScene("Menu");
		}
		else
		{
			menuTimer -= Time.deltaTime;
		}
	}

	public void UpdateCounter(int counter)
	{
		this.refugees += counter;
		CounterText.text = refugees.ToString() + "/" + max_refugees.ToString();
		if (this.refugees >= GameObject.FindObjectOfType<NextLevel>().counterLimit)
		{
			ChangeLevel();
		}
	}

	public void PlaySoundExplosion()
	{
		explotionSound.Play();
	}

	public void PlaySoundEnemyDeath()
	{
		enemyDeathSound.Play();
	}
}
