using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public interface ICanDie
{
	void OnDie();
}

public class OnCollisionDie : MonoBehaviour
{
	public ParticleSystem enemyHit;
	GameManager gameManager;
	AudioSource audioSource;

	// Start is called before the first frame update
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		gameManager = GameManager.getGameManager();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// collision with base
		if (collision.CompareTag("Base"))
		{
			EnemyDeath();
			GameObject.FindObjectOfType<Base>().GetHit(10);
		}

		// collision with bullet
		if (collision.CompareTag("Bullet"))
		{
			gameManager.UpdateScore(1);
			EnemyDeath();
			GameObject.Destroy(collision.gameObject);
		}
	}

	private void EnemyDeath()
	{
		Instantiate(enemyHit, transform.position, Quaternion.identity);
		gameManager.PlaySoundEnemyDeath();
		GameObject.Destroy(this.gameObject);

	}
}
