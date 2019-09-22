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

    public float allowedDistanceFromBase = 35f;

    public GameObject bullet;

    float shootTimer;
    Animator cameraAnimator;
    Animator animator;

    System.Random random = new System.Random();
    public AudioSource[] ShootSounds;

    private Indicator gunIndicator { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        //  ShootSound.GetComponent<AudioSource>();
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
            transform.position = new Vector3(Mathf.Clamp(transform.position.x,-allowedDistanceFromBase, allowedDistanceFromBase), Mathf.Clamp(transform.position.y,-allowedDistanceFromBase, allowedDistanceFromBase));
        }
        else
        {
            // set move boolean to state idle
            animator.SetBool("isMoving", false);
        }

    }

    bool isStaggered = false;
    float weaponHeatLimit = 1.5f;
    float weaponHeat = 0f;
    float weaponCoolOffRate = 0.2f;

    private bool Shooting()
    {
        return Input.GetKey(KeyCode.Space) || Input.GetAxisRaw("Fire1") == 1;
    }
    private bool Fixing()
    {
        return Input.GetKey(KeyCode.Q)
            || Input.GetKey(KeyCode.E)
            || Input.GetButtonDown("FixA")   //Map to koystick L & R
            || Input.GetButtonDown("FixB");
    }

    void Shoot()
    {
        if (isStaggered)
        {
            if (Fixing())
            {
                weaponHeat -= Time.deltaTime * 2;
            } 
            else
            {
                weaponHeat -= Time.deltaTime * weaponCoolOffRate;
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
        if (shootTimer < 0 && Shooting())
        {
                weaponHeat += Time.deltaTime;

                var heatProportion = weaponHeat / weaponHeatLimit;
                gunIndicator?.SetRate(heatProportion);
                if (weaponHeat >= weaponHeatLimit)
                {
                    isStaggered = true;
                    animator.SetBool("isStaggered", true);
                    return;
                }


                var sign = (int)Math.Round(UnityEngine.Random.Range(-1f, 1f), 0);

                
                foreach (GameObject gun in GameObject.FindGameObjectsWithTag("Gun"))
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
            if (!Shooting())
            {
                weaponHeat -= Time.deltaTime * weaponCoolOffRate;
                if (weaponHeat <= 0)
                {
                    weaponHeat = 0;
                }
                gunIndicator?.SetRate(weaponHeat / weaponHeatLimit);
            }
                
        }
    }

    void PlayShootSound()
    {
       var i = random.Next(0, ShootSounds.Length);
        ShootSounds[i].Play();
    }

    

}
