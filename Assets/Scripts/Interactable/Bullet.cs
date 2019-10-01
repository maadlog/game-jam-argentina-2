using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float bulletSpeed = 1f;
	public float bulletLifeTime = 5f;

	// Start is called before the first frame update
	void Start()
	{
		Destroy(gameObject, bulletLifeTime);
	}

	// Update is called once per frame
	void Update()
	{
		transform.position += transform.right * bulletSpeed * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("EnemyBullet") && !gameObject.CompareTag("EnemyBullet"))
		{
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}
}
