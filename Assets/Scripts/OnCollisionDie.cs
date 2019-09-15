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

	// Start is called before the first frame update
	void Start()
	{
		gameManager = GameManager.getGameManager();
	}

	// Update is called once per frame
	void Update()
	{

	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
        Instantiate(enemyHit, transform.position, Quaternion.identity);
        
		// collision with base
		if (collision.CompareTag("Base"))
		{
			this.GetComponent<EnemyMovement>().stopped = true;
			GameObject.Destroy(this.gameObject);
			GameObject.FindObjectOfType<Base>().GetHit(10);
		}

		// collision with bullet
		if (collision.CompareTag("Bullet"))
		{
			gameManager.UpdateScore(1);
			GameObject.Destroy(this.gameObject);
			GameObject.Destroy(collision.gameObject);
		}
	}

}
