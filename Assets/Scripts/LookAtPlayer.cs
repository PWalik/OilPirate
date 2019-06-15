using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player;
    float y;
    private void Start()
    {
        y = transform.position.y;
    }
    void Update()
    {
        transform.LookAt(player);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}
