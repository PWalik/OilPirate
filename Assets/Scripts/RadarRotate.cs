using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarRotate : MonoBehaviour
{
    [SerializeField]
    float minRange, maxRange;

    [SerializeField]
    float sizeAtMinRange, sizeAtMaxRange;

    public Transform target;
    float distance;
    float y;
    float rotX, rotZ;
    float startScale;
    float currScale;
    private void Start()
    {
        rotX = transform.rotation.eulerAngles.x;
        rotZ = transform.rotation.eulerAngles.z;
        startScale = transform.localScale.x;
    }

    private void Update()
    {
        if (target == null)
            Destroy(gameObject);

        transform.LookAt(target);
        SetScale();
        transform.localScale = new Vector3(currScale, transform.localScale.y, currScale);
        
    }

    void SetScale()
    {
        distance = Vector3.Distance(transform.position, target.position);
        if (distance > maxRange)
            currScale = sizeAtMaxRange;
        else if (distance < minRange)
            currScale = sizeAtMinRange;
        else
        {
            
            float a = (sizeAtMaxRange - sizeAtMinRange) / (maxRange - minRange);
            float b = sizeAtMinRange - a * minRange;
            currScale = a * (distance) + b;
            if (currScale > sizeAtMinRange)
                currScale = sizeAtMinRange;
            else if (currScale < sizeAtMaxRange)
                currScale = sizeAtMaxRange;
        }

    }

}
