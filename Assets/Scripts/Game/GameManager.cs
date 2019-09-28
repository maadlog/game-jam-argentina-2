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
	bool activateFadeOff = false;
	GameObject fadeOff;
	public AudioSource explotionSound;
	public AudioSource enemyDeathSound;
	int kills;
	public GameObject LevelCompleted;
	public bool isInGame;
   bool end;
    float timer = 0.8f;
    // Start is called before the first frame update
    void Awake()
	{
        score = 0;

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

		UpdateScore(0);
		lostText.enabled = false;

		lostPanel.SetActive(false);
      
    }

	// Update is called once per frame
	void Update()
	{
		if (isInGame == false)
		{ Transiciones(); }
		if (activateFadeOff)
		{

		}

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
		this.score += score;
	}

	public void ChangeLevel()
	{
		SceneManager.LoadScene(GameObject.FindObjectOfType<NextLevel>().nextLevel);
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
        end =true;
		
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
        score = PlayerPrefs.GetInt("Score");
        score = (refugees * 18) + (kills * 26);
        PlayerPrefs.SetInt("Score", score);
        LevelCompleted.GetComponent<LevelCompleted>().ShowKills(kills);
		LevelCompleted.GetComponent<LevelCompleted>().ShowRefugees(refugees);
		LevelCompleted.GetComponent<LevelCompleted>().ShowScore(score);
	}

	void Transiciones()
	{
		Time.timeScale = 0f;
	}
	void Retomar()
	{
		Time.timeScale = 1f;
	}


}
