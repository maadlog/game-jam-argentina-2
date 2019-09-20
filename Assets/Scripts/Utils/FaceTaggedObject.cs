using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTaggedObject : MonoBehaviour
{

    private GameObject objective;

    public float rotSpeed = 90f;
    public string tagToFace;
    public bool instantaneous;

    // Start is called before the first frame update
    void Awake()
    {
        FindObjective();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (objective == null)
        {
            FindObjective();
        }

        if (objective == null)
            return;

        // We have an objective

        Vector3 dir = objective.transform.position - transform.position;
        dir.Normalize();

        float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        Quaternion desiredRotation = Quaternion.Euler(0, 0, zAngle);

        if (instantaneous)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, 360);
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, rotSpeed * Time.deltaTime);
            
        }

    }

    private void FindObjective()
    {
        if (String.IsNullOrEmpty(tagToFace))
        {
            objective = null;
        }
        else
        {
            //Find the desired target
            GameObject go = GameObject.FindWithTag(tagToFace);

            if (go != null)
            {
                objective = go;
            }
        }

    }
}