using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jug : MonoBehaviour
{
    public float velocidad;
    Vector3 move, movevelocity;
    public Rigidbody2D rb;
    //
    public void Start()
    {
       
        
    }
    private void FixedUpdate()
    {
        rb.velocity = movevelocity;
    }
    void Update()
    {
        

      
        //gameObject.transform.Translate(0, m * Time.deltaTime * velocidad, 0);

    }

}
