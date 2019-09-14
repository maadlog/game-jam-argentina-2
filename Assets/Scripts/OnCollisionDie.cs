using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public interface ICanDie {
    void OnDie();
}

public class OnCollisionDie : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {   
        // collision with base
        if (collision.CompareTag("Base"))
        {
            this.GetComponent<EnemyMovement>().stopped = true;
            GameObject.Destroy(this.gameObject);
        }

        // collision with bullet
        if (collision.CompareTag("Bullet"))
        {
            GameObject.Destroy(this.gameObject);
            GameObject.Destroy(collision.gameObject);
        }
    }

}
