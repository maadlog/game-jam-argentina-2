using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

	private void OnTriggerEnter2D(Collider2D collision)
	{        
		// collision with base
		if (collision.CompareTag("Base"))
		{
			GameObject.Destroy(this.gameObject);
			GameObject.FindObjectOfType<Base>().GetHit(10);
		}
	}
}
