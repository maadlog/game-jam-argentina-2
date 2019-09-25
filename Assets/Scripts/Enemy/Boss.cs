using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	public float moveSpeed = 1f;
	public float startWaitTime = 2f;
	public float shootWaitTime = 1.5f;
	public Transform[] movePoints = default;
	public GameObject bullet;
	public Transform gun;

	Transform target;
	float waitTimer;
	float shootTimer;

	Animator animator;

	Base baseTarget;

	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponentInChildren<Animator>();
		baseTarget = GameObject.FindObjectOfType<Base>();
		target = movePoints[Random.Range(0, movePoints.Length)];
		waitTimer = startWaitTime;
	}

	// Update is called once per frame
	void Update()
	{
		MoveToFixedPoints();
	}

	void MoveToFixedPoints()
	{
		if (baseTarget != null)
		{
			// Rotate to target
			Vector3 dir = baseTarget.transform.position - transform.position;
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);



			if (Vector2.Distance(transform.position, target.position) > 0.2f)
			{
				animator.SetBool("isMoving", true);
			}

			transform.position =
				Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
			if (Vector2.Distance(transform.position, target.position) < 0.2f)
			{
				animator.SetBool("isMoving", false);
				if (waitTimer <= 0f)
				{
					target = movePoints[Random.Range(0, movePoints.Length)];
					waitTimer = startWaitTime;
					shootTimer = shootWaitTime;
				}
				else
				{
					if (shootTimer <= 0f)
					{
                        var move = 10;
                        var original = transform.rotation.eulerAngles;
                        original.z -= move;
						Instantiate(bullet, gun.transform.position, Quaternion.Euler(original));
                        original.z += move;
                        Instantiate(bullet, gun.transform.position, Quaternion.Euler(original));
                        original.z += move;
                        Instantiate(bullet, gun.transform.position, Quaternion.Euler(original));
                        shootTimer = shootWaitTime;
					}
					else
					{
						shootTimer -= Time.deltaTime;
					}

					waitTimer -= Time.deltaTime;
				}
			}
		}
	}
}
