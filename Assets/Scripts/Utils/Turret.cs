using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
	public float timeToShoot = 2f;
	public float timeToFindNext = 2f;
	public float rotateSpeed = 2f;
	public GameObject bullet;
	public Transform gun;
	public Transform turretTop;

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
			// Rotate towards target
			Vector3 dir = target.position - turretTop.position;
			dir.Normalize();
			float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
			Quaternion desiredRotation = Quaternion.Euler(0, 0, zAngle);
			turretTop.rotation = Quaternion.Slerp(turretTop.rotation, desiredRotation, rotateSpeed * Time.deltaTime);
			
			// Shoot if its time to shoot is 0
			if (timerToShoot < 0)
			{
				// Shoot
				Instantiate(bullet, gun.position, gun.rotation);
				timerToShoot = timeToShoot;
			}
			else
			{
				timerToShoot -= Time.deltaTime;
			}
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
