using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    [SerializeField]
    Transform targetTransform;
    Transform thisTransform;
    [SerializeField]
    Vector3 offset;
    private void Start()
    {
        thisTransform = GetComponent<Transform>();
    }

    void Update()
    {
        thisTransform.position = targetTransform.position + offset;
    }
}
