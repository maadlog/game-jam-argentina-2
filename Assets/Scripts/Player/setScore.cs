using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setScore : MonoBehaviour
{
    
    void Start()
    {
        PlayerPrefs.SetInt("Score", 0);
    }
    
}
