
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	public float moveSpeed = 1f;
	public float startWaitTime = 2f;

    private bool emerging = true;
    private bool moving = true;
    private bool waitingPortal;
    private bool waitinClose;

    internal void PortalActive()
    {
        //If emerging or not, play animation and stop shooting
        if (emerging)
        {
            this.animator.Play("Emerge");
        } else
        {
            this.animator.Play("Retire");
        }

        moving = false;
        this.GetComponent<TakeDamage>().invulnerable = true;
    }

    public float shootWaitTime = 1.5f;
    
    internal void PortalClosed()
    {
        if (waitingPortal)
        {
            moving = true;
            this.GetComponent<TakeDamage>().invulnerable = false;
        }
        waitingPortal = false;

        if (waitinClose)
        {
            _portal.Appear(this.transform);
            waitinClose = false;
            waitingPortal = true;
         }
    }

    internal void FinishedRetiring()
    {
        this.transform.position = movePoints[Random.Range(0, movePoints.Length)].position;
        waitingPortal = true;
        emerging = true;
        _portal.Disappear();
        waitinClose = true;
    }

    internal void FinishedEmerging()
    {
        _portal.Disappear();
        emerging = false;
        
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
        waitingPortal = true;
        emerging = true;
        _portal.Appear(this.transform);
        

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

            if (waitingPortal || !moving)
            {
                return;
            }
            
            if (this.GetComponent<TakeDamage>().health <= this.GetComponent<TakeDamage>().startHealth / 2 && !phase2)
            {
                phase2 = true;
                waitingPortal = true;
                emerging = false;
                _portal.Appear(this.transform);
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
