using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerToDestroy : MonoBehaviour
{
    [SerializeField]
    float howMuchTime;
    [SerializeField]
    float destoyColliderAfter;
    Collider col;
    float timer;
    private void Start()
    {
        col = GetComponent<Collider>();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= destoyColliderAfter)
            Destroy(col);

        if (timer >= howMuchTime)
            Destroy(gameObject);
    }
}
