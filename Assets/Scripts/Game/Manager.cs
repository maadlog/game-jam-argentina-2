using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
	private static Manager manager = null;

	public int Score { get; set; }
	public int HighScore { get; set; }
	public int Players { get; set; }

	void Awake()
	{
		if (manager == null)
		{
			manager = this;
			DontDestroyOnLoad(gameObject);

			InitialSettings();
		}
		else
		{
			if (manager != this)
			{
				Destroy(gameObject);
			}
		}
	}

	void InitialSettings()
	{
		Score = 0;
		HighScore = 0;
		Players = 1;
        PlayerPrefs.SetInt("Score0", 0);
        PlayerPrefs.SetInt("Score1", 0);
    }

	public static Manager GetInstance()
	{
		return manager;
	}
}
