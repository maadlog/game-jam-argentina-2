using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float movementSpeed = 1f;
	public float rotateSpeed = 1f;
	public float shootColdDown = 2f;

    public Transform[] guns;
	public GameObject gunLeft;
	public GameObject bullet;

	float shootTimer;
	Animator animator;
	// Start is called before the first frame update
	void Start()
	{
		animator = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
		shootTimer = shootColdDown;
	}

	// Update is called once per frame
	void Update()
	{
        Movement();

		Shoot();
	}

    void Movement() {
        // Handle rotation
		transform.Rotate(0.0f, 0.0f, -Input.GetAxis("Horizontal") * rotateSpeed);

        // Handle foward or backwards
		float moveVertical = Input.GetAxis("Vertical");
		transform.position += moveVertical * transform.right * movementSpeed * Time.deltaTime;
    }

	float timeShooting = 0f;
	float timeShootingLimit = 1f;
	void Shoot()
	{
		if (shootTimer < 0)
		{
			if (Input.GetKey(KeyCode.Space))
			{
				timeShooting += Time.deltaTime;
				if (timeShooting > timeShootingLimit)
				{
					timeShooting = timeShootingLimit;
				}

				var sign = (int)Math.Round(UnityEngine.Random.Range(-0.4f, 0.4f), 0);
				var intDisplace = UnityEngine.Random.Range(0f, 2f) * sign;

				float proportion = timeShooting / timeShootingLimit;

				var disp = intDisplace * proportion;

				if (proportion > 0.2)
				{
					animator.Play("Shoot");
				}

                foreach (Transform gun in guns)
                {
                    var displace = gun.transform.TransformVector(new Vector3(0, disp, 0));
				    var trasn = gun.transform.position + displace;
				    Instantiate(bullet, trasn, transform.rotation);
                }

				// var displace = gunLeft.transform.TransformVector(new Vector3(0, disp, 0));
				// var trasn = gunLeft.transform.position + displace;
                // Instantiate(bullet, trasn, transform.rotation);
				shootTimer = shootColdDown;
			}
			else
			{
				timeShooting = 0f;
			}
		}
		else
		{
			shootTimer -= Time.deltaTime;
		}
	}

}
