using System.Linq;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageable
{
	public float moveSpeed = 0.5f;
	public float startWaitTime = 2f;

    private bool emerging = true;
    private bool moving = true;
    private bool waitingPortal;
    private bool waitinClose;

    public int generateChildrens = 2;

    private TakeDamage damageComponent;

    #region Animations
    internal void PortalActive()
    {
        //If emerging or not, play animation and stop shooting
        if (emerging && animator.isActiveAndEnabled)
        {
            animator.Play("Emerge");
        } else
        {
            animator.Play("Retire");
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
        if (this.transform != null)
        {
            this.transform.position = movePoints[Random.Range(0, movePoints.Length)].position;
        }
        waitingPortal = true;
        emerging = true;
        _portal?.Disappear();
        waitinClose = true;
    }

    internal void FinishedEmerging()
    {
        _portal?.Disappear();
        emerging = false;
        if (GameObject.FindGameObjectWithTag("BossUI") != null)
        {
            GameObject.FindGameObjectWithTag("BossUI").transform.localScale = new Vector3(1, 1, 1);
        }
        
    }

    #endregion


    public int BossId = 0;

    public PointDispatcher pointDispatcher;

    public Transform[] movePoints = default;
	public GameObject bullet;
	public Transform gun;
    public GameObject bossPortal;
    private BossPortal _portal;

	Vector3 target;
	float waitTimer;
	float shootTimer;

	Animator animator;

	Base baseTarget;

	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponentInChildren<Animator>();
		baseTarget = GameObject.FindObjectOfType<Base>();
        damageComponent = this.GetComponent<TakeDamage>();
        if (pointDispatcher == null)
        {
            pointDispatcher = new PointDispatcher(
               new PointDispatcher.RandomPointFromArraySet(
                   movePoints.Select(x => x.position)
               )
           );
        }
       
        

        _portal = bossPortal?.GetComponent<BossPortal>();
        _portal?.Handle(this);
        waitingPortal = true;
        emerging = true;
        if (GameObject.FindGameObjectWithTag("BossUI") != null)
        {
            GameObject.FindGameObjectWithTag("BossUI").transform.localScale = Vector3.zero;
        }
        _portal?.Appear(this.transform);


        target = pointDispatcher.GetNextPoint(this.BossId);
		waitTimer = startWaitTime;
	}

	// Update is called once per frame
	void Update()
	{
		MoveToFixedPoints();
	}

    void SetHealth(int someHealt)
    {
        this.GetComponent<TakeDamage>().startHealth = someHealt;
        this.GetComponent<TakeDamage>().health = someHealt;
    }

    bool phase2 = false;
    public int shoots = 3;
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
            
            if (this.GetComponent<TakeDamage>() != null
                && this.GetComponent<TakeDamage>().health <= this.GetComponent<TakeDamage>().startHealth / 2
                && !phase2)
            {
                phase2 = true;
                waitingPortal = true;
                emerging = false;
                if (GameObject.FindGameObjectWithTag("BossUI") != null)
                {
                    GameObject.FindGameObjectWithTag("BossUI").transform.localScale = Vector3.zero;
                }
                _portal?.Appear(this.transform);
                return;
            }

            if (Vector2.Distance(transform.position, target) > 0.2f)
			{
				animator?.SetBool("isMoving", true);
			}

			transform.position =
				Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
			if (Vector2.Distance(transform.position, target) < 0.2f)
			{
				animator?.SetBool("isMoving", false);
                
				if (waitTimer <= 0f)
				{
                    target = pointDispatcher.GetNextPoint(this.BossId);
					waitTimer = startWaitTime;
					shootTimer = shootWaitTime;
				}
				else
				{
					if (shootTimer <= 0f)
					{
                        if (shoots == 1)
                        {
                            Instantiate(bullet, gun.transform.position, transform.rotation);
                        } else
                        {
                            var move = 10f;
                            var start = move / 2;
                            var piece = move / shoots;
                            var original = transform.rotation.eulerAngles;
                            original.z -= start;
                            for (int i = 0; i < shoots; i++)
                            {
                                Instantiate(bullet, gun.transform.position, Quaternion.Euler(original));
                                original.z += piece;
                            }
                        }
                        
                        
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

    public void Die()
    {
        if (generateChildrens > 0)
        {
            GameManager.getGameManager().RegisterBoss();
            GameManager.getGameManager().RegisterBoss();
            int newBossId = this.BossId * 10 + 1;
            int newHealth = Mathf.RoundToInt( damageComponent
                .startHealth / 2);

            var miniboss = Instantiate(GameManager.getGameManager().BossPrefab, Vector3.zero, Quaternion.identity);
            var minibossDirector = miniboss.GetComponentInChildren<Boss>();
            minibossDirector.pointDispatcher = pointDispatcher;
            minibossDirector.transform.position = pointDispatcher.GetNextPoint(newBossId);
            minibossDirector.BossId = newBossId;
            minibossDirector.generateChildrens = generateChildrens - 1;
            minibossDirector.shoots = shoots - 1;
            minibossDirector.transform.localScale = this.transform.localScale * 0.8f;
            minibossDirector.SetHealth(newHealth);
            
            newBossId = this.BossId * 10 + 2;
            miniboss = Instantiate(GameManager.getGameManager().BossPrefab, Vector3.zero, Quaternion.identity);
            minibossDirector = miniboss.GetComponentInChildren<Boss>();
            minibossDirector.pointDispatcher = pointDispatcher;
            minibossDirector.transform.position = pointDispatcher.GetNextPoint(newBossId);
            minibossDirector.BossId = newBossId;
            minibossDirector.generateChildrens = generateChildrens - 1;
            minibossDirector.shoots = shoots - 1;
            minibossDirector.transform.localScale = this.transform.localScale * 0.8f;
            minibossDirector.SetHealth(newHealth);
            
            pointDispatcher.Reset(this.BossId);
        }
        Destroy(GetComponentInParent<TaggingScript>().gameObject);
       
        GameManager.getGameManager().BossDefeated();
        
    }
}
