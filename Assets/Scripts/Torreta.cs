using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    public GameObject uivida;
    void Start()
    {
        uivida.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
