using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimiento_cards : MonoBehaviour
{

    public GameObject canvas; //Objeto para recivir la pos del canvas (SAcar de aca)
    //Un objeto vacio para triggear el stop

    float maxPositionX ;
    Vector3 maxPositionY = new Vector2();
    Vector3 maxPositionZ = new Vector3();
    public float displaySpeed =100f;

    public bool stop {set; get;}
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {   
        //Muevo el cuadro o imagen hacia la izquierda     
        transform.position += -transform.right*displaySpeed*Time.deltaTime;

        
        if(stop)
        {
            transform.position += transform.right*0;
        }
    }
}
