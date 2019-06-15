using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableColliderAfter : MonoBehaviour
{
    [SerializeField]
    float enableAfter;
    float timer;
    Collider col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= enableAfter)
        {
            col.enabled = true;
            Destroy(this);
        }
    }
}
