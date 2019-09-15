using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float movementSpeed = 1f;
	public float rotateSpeed = 1f;
	public float shootColdDown = 2f;

	public GameObject bullet;

	float shootTimer;
	Animator cameraAnimator;
	Animator animator;

	// Start is called before the first frame update
	void Start()
	{
		cameraAnimator = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
		animator = GetComponent<Animator>();
		shootTimer = shootColdDown;
	}

	// Update is called once per frame
	void Update()
	{
		Movement();

		Shoot();
	}

	void Movement()
	{
		// get input
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		if (horizontal != 0 || vertical != 0)
		{
			// set move boolean to state moving
			animator.SetBool("isMoving", true);

			// handle rotation
			transform.Rotate(0.0f, 0.0f, -horizontal * rotateSpeed);

			// handle foward or backwards
			float moveVertical = vertical;
			transform.position += moveVertical * transform.right * movementSpeed * Time.deltaTime;
		}
		else
		{
			// set move boolean to state idle
			animator.SetBool("isMoving", false);
		}

	}

	float timeShooting = 0f;
	float timeShootingLimit = 1f;
    float staggeredCooldown = 2f;
    bool isStaggered = false;
	void Shoot()
	{
		if (shootTimer < 0)
		{
			if (Input.GetKey(KeyCode.Space) && !isStaggered)
			{
				timeShooting += Time.deltaTime;
				if (timeShooting > timeShootingLimit)
				{
					timeShooting = timeShootingLimit;
                    shootColdDown = staggeredCooldown;
				}

				var sign = (int)Math.Round(UnityEngine.Random.Range(-1f, 1f), 0);
				
				float proportion = timeShooting / timeShootingLimit;

                if (proportion > 0.4)
				{
					cameraAnimator.Play("Shoot");
				}
                
				foreach (GameObject gun in GameObject.FindGameObjectsWithTag("Gun"))
				{
                    var intDisplace = UnityEngine.Random.Range(0f, .1f) * sign;
                    var disp = intDisplace * proportion;
                    Debug.Log(disp);
                    var displace = gun.transform.TransformVector(new Vector3(0, disp, 0));
					var trasn = gun.transform.position + displace;
					Instantiate(bullet, trasn, transform.rotation);
				}

				shootTimer = shootColdDown;
			}
			else
			{
                timeShooting -= Time.deltaTime;
                if (timeShooting <= 0)
                {
                    timeShooting = 0;
                }
            }
		}
		else
		{
			shootTimer -= Time.deltaTime;
		}
	}

}
