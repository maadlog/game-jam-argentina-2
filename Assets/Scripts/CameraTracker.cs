using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

class Square
{
    public Vector2 bottomLeft;
    public Vector2 topRight;

    public Square(Vector2 bottomLeft, Vector2 topRight)
    {
        this.bottomLeft = bottomLeft;
        this.topRight = topRight;
    }

    public Square(float width, float height, Vector2 center)
    {

        this.bottomLeft = new Vector2(center.x - (width / 2), center.y - (height/2));
        this.topRight = new Vector2(center.x + (width / 2), center.y + (height / 2));
    }

    public float Height()
    {
        return topRight.y - bottomLeft.y;
    }

    public float Width()
    {
        return topRight.x - bottomLeft.x;
    }

    public Vector2 Center()
    {
        return new Vector2(bottomLeft.x + ((topRight.x - bottomLeft.x) / 2), bottomLeft.y + ((topRight.y - bottomLeft.y) / 2));
    }

    public override string ToString()
    {
        return $"Square( ({bottomLeft.x},{bottomLeft.y}), ({topRight.x},{topRight.y}) )";
    }
}

public interface ITrackable
{
    void SetTracker(CameraTracker tracker);
}

public class CameraTracker : MonoBehaviour
{
    
    public List<GameObject> trackingObjects;

    public float movementSpeed = 5f;

    void Start()
    {
        trackingObjects.ForEach(x => x.GetComponent<ITrackable>()?.SetTracker(this));
    }

    public void RemoveItem(GameObject obj)
    {
        this.trackingObjects.Remove(obj);
    }

    private void Awake()
    {
        InitializeStandars();
    }

    float originalFOV;

    private void InitializeStandars()
    {
        originalFOV = Camera.main.fieldOfView;
        //This asumes floor at 0 z
        // float viewingMaxLength = Mathf.Tan(originalFOV * Mathf.Deg2Rad * 0.5f)
        //    * Mathf.Abs(Camera.main.transform.position.z) * 2f;

        // Aspect Ratio = 16:9 ->

        // viewingWidth = viewingMaxLength;
        // viewingHeight = viewingMaxLength * 9f / 16f;
    }

    void Update()
    {
        InitializeStandars();

        Vector3 targetPosition;
        if (trackingObjects.Count == 0)
        {
            targetPosition = Camera.main.transform.position;
        }
        else
        {

            Square tracking = new Square(Vector2.zero, Vector2.zero);
            
            trackingObjects.ForEach(x =>
            {
                if (x.transform.position.x < tracking.bottomLeft.x)
                {
                    tracking.bottomLeft.x = x.transform.position.x;
                }
                if (x.transform.position.x > tracking.topRight.x)
                {
                    tracking.topRight.x = x.transform.position.x;
                }
                if (x.transform.position.y < tracking.bottomLeft.y)
                {
                    tracking.bottomLeft.y = x.transform.position.y;
                }
                if (x.transform.position.y > tracking.topRight.y)
                {
                    tracking.topRight.y = x.transform.position.y;
                }
                
            });

            targetPosition = tracking.Center();
            if (tracking.Height() > tracking.Width())
            {
                targetPosition.z = GetZToViewY(tracking.Height());
            } else
            {
                targetPosition.z = GetZToViewX(tracking.Width());
            }
        }

        var mov = Vector3.MoveTowards(GetComponent<Transform>().position
            , targetPosition, movementSpeed * Time.deltaTime);

        Camera.main.transform.position = mov;
    }

    private float GetZToViewX(float x)
    {
        var vl = x;

        var absZ = vl / (Mathf.Tan(originalFOV * Mathf.Deg2Rad * 0.5f) * 2);

        return -absZ;
    }

    private float GetZToViewY(float y)
    {
        var vl = y * 16 / 9;

        var absZ = vl / (Mathf.Tan(originalFOV * Mathf.Deg2Rad * 0.5f) * 2);

        return -absZ;
    }
}
