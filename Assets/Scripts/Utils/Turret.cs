using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
	public float timeToShoot = 2f;
	public float timeToFindNext = 2f;
	public GameObject bullet;
	public Transform gun;

	Transform target;
    float timerToFindNext;
	float timerToShoot;

	// Start is called before the first frame update
	void Start()
	{
		timerToFindNext = timeToFindNext;
	}

	// Update is called once per frame
	void Update()
	{
		if (target != null)
		{
			// Shoot
			Instantiate(bullet, gun.position, transform.rotation);
		}
		else
		{
			if (timerToFindNext < 0)
			{
				FindNearestEnemy();
				timerToFindNext = timeToFindNext;
				timerToShoot = timeToShoot;
			}
			else
			{
				timerToFindNext -= Time.deltaTime;
			}
		}
	}

	void FindNearestEnemy()
	{
		float enemyMinDistance = float.MaxValue;
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		for (int i = 0; i < enemies.Length; i++)
		{
			float enemyDisctance = Vector2.Distance(transform.position, enemies[i].transform.position);
			if (enemyDisctance < enemyMinDistance)
			{
				target = enemies[i].transform;
				enemyMinDistance = enemyDisctance;
			}
		}
	}
}
