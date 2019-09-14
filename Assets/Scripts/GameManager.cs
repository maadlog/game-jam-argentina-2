using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public float timeToMoveToMenu = 2f;
	public Text scoreText;

	int score = 0;
	GameManager gameManager;
	float menuTimer;

	// Start is called before the first frame update
	void Start()
	{
        // set time to timer
        menuTimer = timeToMoveToMenu;

        // make it singleton
		if (gameManager == null)
		{
			gameManager = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(this);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void UpdateScore(int score)
	{
		this.score += score;
		scoreText.text = this.score.ToString();
	}

	public void ChangeLevel()
	{
		SceneManager.LoadScene("Level2");
	}

	public void LostLevel()
	{
		// player cant move
        
		// go to menu after a while
        if(menuTimer < 0) {
            SceneManager.LoadScene("Menu");
        } else {
            menuTimer -= Time.deltaTime;
        }
	}
}
