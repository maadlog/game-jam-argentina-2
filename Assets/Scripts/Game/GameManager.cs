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
	int score2 = 0;
	int refugees = 0;
	int max_refugees;
	static GameManager gameManager;
	float menuTimer;
	public AudioSource explotionSound;
	public AudioSource enemyDeathSound;
	public GameObject LevelCompleted;
	public bool isInGame;
	bool end;
	float timer = 0.8f;

	public GameObject BossPrefab;

	Manager manager;
	public Transform[] playerSpawnPoints;
	public GameObject playerPrefab;

	LevelFader levelFader;
	public MainCanvas mainCanvas;
   public  NextLevel nextlvl;

    GameObject[] players = new GameObject[2];
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

        if (nextlvl != null)
        {
            max_refugees = nextlvl.counterLimit;
        }
        

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

    public void UpdateScore(int jugador, int score)
    {
        
            players[jugador].GetComponent<Player>().UpdateScore(score);
 
        //this.score = PlayerPrefs.GetInt("Score");
        //this.score += score;
        //PlayerPrefs.SetInt("Score", this.score);

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

    public void KillsPlayer(int jugador)
    {
       players[jugador].GetComponent<Player>().Kill();
    }

    public void CalcularScore()
	{
        int kills;
        int score;
        //UpdateScore(1,(refugees * 18) + (kills * 26));
        if (players[1] != null)
        {
            kills = players[1].GetComponent<Player>().kills;
            players[1].GetComponent<Player>().UpdateScore( (refugees * 2) + (players[1].GetComponent<Player>().kills * 3));
            players[0].GetComponent<Player>().UpdateScore( (refugees * 2) + (players[0].GetComponent<Player>().kills * 3));
            score = players[1].GetComponent<Player>().Score;
        }
        else { kills = 0;
            players[0].GetComponent<Player>().UpdateScore((refugees * 2) + (players[0].GetComponent<Player>().kills * 3));
            score = 0;

        }
       
        

        LevelCompleted.GetComponent<LevelCompleted>().ShowKills(players[0].GetComponent<Player>().kills, kills);
        LevelCompleted.GetComponent<LevelCompleted>().ShowRefugees(refugees);
        LevelCompleted.GetComponent<LevelCompleted>().ShowScore(players[0].GetComponent<Player>().Score, score);

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
            players[i] = player;
            player.GetComponent<Player>().PLayerNumber = i;
            Player playerComponent = player.GetComponent<Player>();
            playerComponent.SetControllerNumber(i + 1);
         

            if (i == 1)
			{
				playerComponent.gunIndicator = mainCanvas.CreateIndicator(HeatIndicator.Position.Right);
				player.GetComponentInChildren<SpriteRenderer>().color = Color.cyan;
			} else {
				playerComponent.gunIndicator = mainCanvas.CreateIndicator(HeatIndicator.Position.Left);
			}

            
        }
	}
}
