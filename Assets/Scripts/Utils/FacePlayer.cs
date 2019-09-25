using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{

    GameObject objective;

    public float rotSpeed = 90f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (objective == null)
        {
            //Find the base
            GameObject go = GameObject.FindWithTag("Base");

            if (go != null)
            {
                objective = go;
            }
        }

        if (objective == null)
            return;

        // We have a player

        Vector3 dir = objective.transform.position - transform.position;
        dir.Normalize();

        float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;


        Quaternion desiredRotation = Quaternion.Euler(0, 0, zAngle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, rotSpeed * Time.deltaTime);

    }
}
