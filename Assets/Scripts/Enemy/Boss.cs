
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	public float moveSpeed = 1f;
	public float startWaitTime = 2f;

    private bool emerging = true;
    private bool moving = true;

    internal void PortalActive()
    {
        //If emerging or not, play animation and stop shooting
        if (emerging)
        {
            idleTimer = idleTimerBase;
            this.animator.Play("Emerge");
        } else
        {
            this.animator.Play("Retire");
        }

        moving = false;
        this.GetComponent<TakeDamage>().invulnerable = true;
    }

    public float shootWaitTime = 1.5f;
    public float idleTimer;
    public float idleTimerBase = 0.1f;
    internal void PortalClosed()
    {
        //If emerging, start shooting
        if (emerging)
        {
            moving = true;
            this.GetComponent<TakeDamage>().invulnerable = false;
        } else
        {
            //this.Teleport();
            this.transform.position = movePoints[Random.Range(0, movePoints.Length)].position;
            this.animator.Play("Invisible");
            _portal.Appear(this.transform);
            idleTimer = idleTimerBase;
            emerging = true;
        }
    }

    public Transform[] movePoints = default;
	public GameObject bullet;
	public Transform gun;
    public GameObject bossPortal;
    private BossPortal _portal;

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

        _portal = bossPortal.GetComponent<BossPortal>();
        _portal.Handle(this);
        _portal.Appear(this.transform);
        idleTimer = idleTimerBase;

        target = movePoints[Random.Range(0, movePoints.Length)];
		waitTimer = startWaitTime;
	}

	// Update is called once per frame
	void Update()
	{
		MoveToFixedPoints();
	}

    bool phase2 = false;
	void MoveToFixedPoints()
	{
        
		if (baseTarget != null)
		{
			// Rotate to target
			Vector3 dir = baseTarget.transform.position - transform.position;
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (!moving)
            {
                return;
            }
            idleTimer -= Time.deltaTime;
            if (idleTimer > 0)
            {
                return;
            }
            if (idleTimer < -10) { idleTimer = -10; }

            if (this.GetComponent<TakeDamage>().health <= this.GetComponent<TakeDamage>().startHealth / 2 && !phase2)
            {
                phase2 = true;
                emerging = false;
                _portal.Appear(this.transform);
                idleTimer = idleTimerBase;
                return;
            }

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
