using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public interface IDamageable
{
    void Die();
}

public class TakeDamage : MonoBehaviour 
{
	public float startHealth = 100f;
	public float timeBetweenHits = 0.1f;
	public string[] damageableTags = new string[0];
	public ParticleSystem damageParticles;

	public float health;
	float hitsTimer = 0f;

    public bool invulnerable;

    GameObject owner;


	// Start is called before the first frame update
	void Start()
	{
		health = startHealth;
	}

	// Update is called once per frame
	void Update()
	{
		hitsTimer += Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		foreach (var tag in damageableTags)
		{
			if (other.CompareTag(tag))
			{
				Destroy(other.gameObject);
				if (hitsTimer >= timeBetweenHits)
				{

					TakeHit();
					hitsTimer = 0;
				}
			}
		}
	}

	void TakeHit()
	{
        if (invulnerable)
        {
            return;
        }
		if (health > 0)
		{
			health -= 10;
		}
		else
		{
			Kill();

		}

		Instantiate(damageParticles, transform.position, Quaternion.identity);
	}

	void Kill()
	{
        this.GetComponent<IDamageable>().Die();
		

    }
}
