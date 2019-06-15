using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField]
    Vector3 spinDirection;
    [SerializeField]
    float spinSpeed;
    Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        trans.Rotate(spinDirection * spinSpeed);
    }
}
