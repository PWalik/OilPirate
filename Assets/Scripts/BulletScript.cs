using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    public GameObject explosionPrefab;
    public float bulletUptime = 2f;
    float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= bulletUptime || transform.position.y <= 0f)
        {
            Explode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }


    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
        Destroy(gameObject);
    }
}
