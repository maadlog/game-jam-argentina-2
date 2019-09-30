using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    public float timeToDisapear = 5f;
    
    private void Start()
    {
        Destroy(gameObject, timeToDisapear);
    }
}
