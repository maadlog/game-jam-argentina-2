using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
	GameManager gameManager;
    float lifetime = 25f;
	// Start is called before the first frame update
	void Start()
	{
		gameManager = GameManager.getGameManager();
	}

	// Update is called once per frame
	void Update()
	{
        this.lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
	}


    private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.CompareTag("Player"))
		{
			gameManager.UpdateCounter(1);
            Destroy(gameObject);
		}

	}

}
