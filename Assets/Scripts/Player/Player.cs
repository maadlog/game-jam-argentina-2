using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
	public float movementSpeed = 1f;
	public float rotateSpeed = 1f;
	public float shootColdDown = 2f;

	public float shootEffectProportion = 0.6f;
	public float allowedDistanceFromBase = 35f;

	public GameObject bullet;
	public GameObject[] guns;

	float shootTimer;
	Animator cameraAnimator;
	Animator animator;

	System.Random random = new System.Random();
	public AudioSource[] ShootSounds;

	Indicator gunIndicator { get; set; }

	// Axis and button strings to set with controller number
	string horizontalInput;
	string verticalInput;
	string fireInput;
	string fixAInput;
	string fixBInput;

	// Start is called before the first frame update
	void Start()
	{
		cameraAnimator = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
		animator = GetComponent<Animator>();
		shootTimer = shootColdDown;
		gunIndicator = GameObject
			.FindGameObjectsWithTag("GunIndicator")
			.FirstOrDefault(x => x.name.Contains("MainGun"))
			?.GetComponent<Indicator>();
	}

	// Update is called once per frame
	void Update()
	{
		Movement();

		Shoot();
	}

	public void SetControllerNumber(int controllerNumber)
	{
		horizontalInput = String.Format("P{0}Horizontal", controllerNumber);
		verticalInput = String.Format("P{0}Vertical", controllerNumber);
		fireInput = String.Format("P{0}Fire", controllerNumber);
		fixAInput = String.Format("P{0}FixA", controllerNumber);
		fixBInput = String.Format("P{0}FixB", controllerNumber);
	}

	void Movement()
	{
		// get input

		float horizontal = Input.GetAxis(horizontalInput);
		float vertical = Input.GetAxis(verticalInput);

		if (horizontal != 0 || vertical != 0)
		{
			// set move boolean to state moving
			animator.SetBool("isMoving", true);

			// handle rotation
			transform.Rotate(0.0f, 0.0f, (vertical >= 0 ? -horizontal : horizontal) * rotateSpeed);

			// handle foward or backwards
			float moveVertical = vertical;
			transform.position += moveVertical * transform.right * movementSpeed * Time.deltaTime;
			// transform.position = new Vector3(Mathf.Clamp(transform.position.x, -allowedDistanceFromBase, allowedDistanceFromBase), Mathf.Clamp(transform.position.y, -allowedDistanceFromBase, allowedDistanceFromBase));
		}
		else
		{
			// set move boolean to state idle
			animator.SetBool("isMoving", false);
		}
	}

	bool isStaggered = false;
	float weaponHeat = 0f;
	public float weaponHeatLimit = 4f;
	public float weaponCoolOffTimeRate = 0.5f;
    public float weaponCoolOffOnFix = 0.1f;

    private bool Shooting()
	{
		return Input.GetAxisRaw(fireInput) == 1;
	}
	private bool Fixing()
	{
		return Input.GetButtonDown(fixAInput)   //Map to koystick L & R
			|| Input.GetButtonDown(fixBInput);
	}

	private void FixedUpdate()
	{
		if (isStaggered)
		{
			if (Shooting())
			{
				return;
			}
			if (Fixing())
			{
				weaponHeat -= weaponCoolOffOnFix;
			}
			else
			{
				weaponHeat -= Time.deltaTime * weaponCoolOffTimeRate;
			}
			if (weaponHeat <= 0)
			{
				weaponHeat = 0;
				isStaggered = false;
				animator.SetBool("isStaggered", false);
			}

			gunIndicator?.SetRate(weaponHeat / weaponHeatLimit);

			return;
		}
		if (Shooting())
		{
			weaponHeat += Time.deltaTime;

			var heatProportion = weaponHeat / weaponHeatLimit;

			if(heatProportion > shootEffectProportion) {
				cameraAnimator.Play("Shoot");
			}

			gunIndicator?.SetRate(heatProportion);
			if (weaponHeat >= weaponHeatLimit)
			{
				isStaggered = true;
				animator.SetBool("isStaggered", true);
				return;
			}

		}
		else
		{
			if (!Shooting())
			{
				weaponHeat -= Time.deltaTime * weaponCoolOffTimeRate;
				if (weaponHeat <= 0)
				{
					weaponHeat = 0;
				}
				gunIndicator?.SetRate(weaponHeat / weaponHeatLimit);
			}

		}
	}

	void Shoot()
	{
		if (isStaggered)
		{
			return;
		}
		if (shootTimer < 0 && Shooting())
		{
			var heatProportion = weaponHeat / weaponHeatLimit;

			var sign = (int)Math.Round(UnityEngine.Random.Range(-1f, 1f), 0);

			foreach (GameObject gun in guns)
			{
				var intDisplace = UnityEngine.Random.Range(0f, .1f) * sign;
				var disp = intDisplace * heatProportion;

				var displace = gun.transform.TransformVector(new Vector3(0, disp, 0));
				var trasn = gun.transform.position + displace;
				Instantiate(bullet, trasn, transform.rotation);
				PlayShootSound();
			}

			shootTimer = shootColdDown;
		}
		else
		{
			shootTimer -= Time.deltaTime;
		}
	}

	void PlayShootSound()
	{
		var i = random.Next(0, ShootSounds.Length);
		ShootSounds[i].Play();
	}
}
