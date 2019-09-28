using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationCallback : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FinishedEmerging()
    {
        this.GetComponentInParent<Boss>().FinishedEmerging();
    }

    void FinishedRetiring()
    {
        this.GetComponentInParent<Boss>().FinishedRetiring();
    }
}
