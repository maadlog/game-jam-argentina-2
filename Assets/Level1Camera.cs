using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Camera : MonoBehaviour
{
    Animator cameraAnimator;
    // Start is called before the first frame update
    void Start()
    {
        cameraAnimator = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
    }

    bool done = false;
    // Update is called once per frame
    void Update()
    {
        if (!done)
        {
            cameraAnimator.Play("FocusingEnemies");
            done = true;
        }
    }
}
