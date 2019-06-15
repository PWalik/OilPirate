using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelThrow : MonoBehaviour
{
    //[System.NonSerialized]
    public Vector3 target;
    [SerializeField]
    float minRotateSpeed, maxRotateSpeed;
    [System.NonSerialized]
    public float throwHeight;
    Quaternion rotateDir;
    float rotateSpeed;
    [SerializeField]
    float goSpeed;
    [SerializeField]
    float offset;
    public bool isExploding;
    float distanceReached;
    float full;
    float height;
    [SerializeField]
    GameObject explosionPrefab;
    Collider col;
    [System.NonSerialized]
    public Collider org;
    [SerializeField]
    public ParticleSystem barrel;
    bool isFinished;
    // Start is called before the first frame update
    void Start()
    {
        rotateDir = Random.rotation;
        rotateSpeed = Random.Range(minRotateSpeed, maxRotateSpeed);
        full = Vector3.Distance(transform.position, target);
        col = GetComponent<Collider>();
        distanceReached = 0;
        height = transform.position.y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider != org)
            Explode();
    }

    // Update is called once per frame
    void Update()
    {
        if (distanceReached + goSpeed < full)
        {
            transform.Rotate(new Vector3(rotateDir.x * rotateSpeed, rotateDir.y * rotateSpeed, rotateDir.z * rotateSpeed));
            Vector3 xz = Vector3.MoveTowards(transform.position, target, goSpeed);
            float y = height + throwHeight * Mathf.Sin(Mathf.PI * distanceReached / full);
            float x = xz.x - transform.position.x;
            float z = xz.z - transform.position.z;
            distanceReached += Mathf.Abs(Mathf.Sqrt(x * x + z * z));
            transform.position = new Vector3(xz.x, y, xz.z);
        }
        else if (isExploding) Explode();
        else if(!isFinished) BecomeCollectible();
    }


    void Explode()
    {
        GameObject obj = Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
        Destroy(this.gameObject);
    }

    void BecomeCollectible()
    {
        isFinished = true;
        transform.rotation = Quaternion.identity;
        transform.Rotate(new Vector3(10f, 0));
        transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);
        transform.localScale *= 2;
        barrel.Play();
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        float rotateSpeed = Random.Range(minRotateSpeed, maxRotateSpeed)/10;
        while(true)
        {
            transform.Rotate(new Vector3(0f, rotateSpeed, 0f));
            yield return null;
        }
    }
}

