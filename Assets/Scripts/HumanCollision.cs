using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCollision : MonoBehaviour
{
  
        void OnTriggerEnter2D(Collider2D c)
        {
            if (c.CompareTag("Base"))
            {
                Destroy(this.gameObject);

            }

        }
    
}
