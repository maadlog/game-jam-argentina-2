using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public float timeToMoveToMenu = 2f;

	public Text lostText;
	public GameObject lostPanel;

	public Text CounterText;

	int score = 0;
	int refugees = 0;
	int max_refugees = 15;
	static GameManager gameManager;
	float menuTimer;
	public AudioSource explotionSound;
	public AudioSource enemyDeathSound;
	int kills;
	public GameObject LevelCompleted;
	public bool isInGame;
	bool end;
	float timer = 0.8f;

	public GameObject BossPrefab;

	Manager manager;
	public Transform[] playerSpawnPoints;
	public GameObject playerPrefab;

	LevelFader levelFader;

	// Start is called before the first frame update
	void Awake()
	{
		Retomar();
		isInGame = true;
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

		lostText.enabled = false;

		lostPanel.SetActive(false);

		manager = Manager.GetInstance();
		CreatePlayers();
	}

	void Start() {
		levelFader = LevelFader.GetInstance(this.gameObject);
		levelFader.FadeOut();
	}
	public void FinishedFadeOut() {
		//Do nothing
	}

	// Update is called once per frame
	void Update()
	{
		if (isInGame == false)
		{ Transiciones(); }

		if (end == true)
		{
			timer -= Time.deltaTime;
			if (timer <= 0)
			{
				GameOver();
			}

		}
	}

	public static GameManager getGameManager()
	{
		return gameManager;
	}

	public void UpdateScore(int score)
	{
		this.score = PlayerPrefs.GetInt("Score");
		this.score += score;
		PlayerPrefs.SetInt("Score", this.score);
	}

	void GameOver()
	{
		isInGame = false;
		// show message
		lostText.enabled = true;
		lostPanel.SetActive(true);
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

	public void LostLevel()
	{
		lostText.enabled = true;
		end = true;

	}

	public void UpdateCounter(int counter)
	{
		this.refugees += counter;

		CounterText.text = refugees.ToString() + "/" + max_refugees.ToString();
		if (this.refugees >= GameObject.FindObjectOfType<NextLevel>().counterLimit)
		{
			//ChangeLevel();
			isInGame = false;
			LevelCompleted.GetComponent<LevelCompleted>().WinLevel();
			CalcularScore();
		}
	}

	public void Win()
	{
		isInGame = false;
		LevelCompleted.GetComponent<LevelCompleted>().WinLevel();

	}
	int bosses = 1;
	public void RegisterBoss()
	{
		bosses++;
	}

	public void BossDefeated()
	{
		bosses--;
		if (bosses <= 0)
		{
			Win();
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

	public void KillsPlayer()
	{
		kills++;
	}

	public void CalcularScore()
	{
		UpdateScore((refugees * 18) + (kills * 26));
		LevelCompleted.GetComponent<LevelCompleted>().ShowKills(kills);
		LevelCompleted.GetComponent<LevelCompleted>().ShowRefugees(refugees);
		LevelCompleted.GetComponent<LevelCompleted>().ShowScore(score);

	}

	void Transiciones()
	{
		Time.timeScale = 0f;
	}
	public void Retomar()
	{
		Time.timeScale = 1f;
	}

	void CreatePlayers()
	{
		for (int i = 0; i < manager.Players; i++)
		{
			GameObject player = Instantiate(playerPrefab, playerSpawnPoints[i].position, Quaternion.identity);
			Player playerComponent = player.GetComponent<Player>();
			playerComponent.SetControllerNumber(i + 1);
			
			if (i == 1)
			{
				playerComponent.gunIndicator = MainCanvas.GetInstance().CreateIndicator(HeatIndicator.Position.Right);
				player.GetComponentInChildren<SpriteRenderer>().color = Color.cyan;
			} else {
				playerComponent.gunIndicator = MainCanvas.GetInstance().CreateIndicator(HeatIndicator.Position.Left);
			}
		}
	}
}
