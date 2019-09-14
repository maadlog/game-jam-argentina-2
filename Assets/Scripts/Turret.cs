using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
	Transform target;
    float timerToFindNext;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (target != null)
		{
			// Shoot
		}
		else
		{
			if (timerToFindNext < 0)
			{

			}
			else
			{

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
