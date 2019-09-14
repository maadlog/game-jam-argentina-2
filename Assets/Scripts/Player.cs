using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float movementSpeed = 1f;
	public float rotateSpeed = 1f;
	public float shootColdDown = 2f;

	public GameObject gun;
	public GameObject bullet;

	float shootTimer;

	// Start is called before the first frame update
	void Start()
	{
		shootTimer = shootColdDown;
	}

	// Update is called once per frame
	void Update()
	{
		transform.Rotate(0.0f, 0.0f, -Input.GetAxis("Horizontal") * rotateSpeed);
		float moveVertical = Input.GetAxis("Vertical");
		transform.position += moveVertical * transform.right * movementSpeed * Time.deltaTime;

		Shoot();
	}

	void Shoot()
	{
		if (shootTimer < 0)
		{
			if (Input.GetKey(KeyCode.Space))
			{
				Instantiate(bullet, gun.transform.position, transform.rotation);
				shootTimer = shootColdDown;
			}
		}
		else
		{
			shootTimer -= Time.deltaTime;
		}
	}
}
