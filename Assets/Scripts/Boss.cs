using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float moveSpeed = 1f;
	public float startWaitTime = 2f;
	public bool fixedMovement = default;
	public Transform[] movePoints = default;
    public GameObject bullet;
    public Transform gun;

	Transform target;
	float waitTime;

    Animator animator;

    Base baseTarget;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        baseTarget = GameObject.FindObjectOfType<Base>(); 
        target = movePoints[Random.Range(0, fixedMovement ? movePoints.Length : 0)];
		waitTime = startWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToFixedPoints();
    }

    void MoveToFixedPoints()
	{
        // Rotate to target
		Vector3 dir = baseTarget.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Vector2.Distance(transform.position, target.position) > 0.2f) {
            animator.SetBool("isMoving", true);
        }

		transform.position = 
			Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
		if (Vector2.Distance(transform.position, target.position) < 0.2f)
		{
            animator.SetBool("isMoving", false);
			if (waitTime <= 0f)
			{
				target = movePoints[Random.Range(0, movePoints.Length)];
				waitTime = startWaitTime;
			}
			else
			{
                Instantiate(bullet, gun.transform.position, transform.rotation);
				waitTime -= Time.deltaTime;
			}
		}
	}
}
