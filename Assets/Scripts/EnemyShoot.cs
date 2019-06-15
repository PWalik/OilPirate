using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField]
    Transform muzzle;
    [SerializeField]
    float minShootCD, maxShootCD;
    float shootCD;
    [SerializeField]
    float distanceToShoot;
    EnemyMovement move;
    float timer;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    float shootSpeed;
    [SerializeField]
    float bulletUptime;
    [SerializeField]
    ParticleSystem explosion;
    public bool isShooting;
    private void Start()
    {
        move = GetComponent<EnemyMovement>();
        shootCD = Random.Range(minShootCD, maxShootCD);
    }

    void Update()
    {
        if (isShooting)
        {
            timer += Time.deltaTime;
            if (Range() < distanceToShoot && timer >= shootCD)
            {
                Shoot();
                shootCD = Random.Range(minShootCD, maxShootCD);
                timer = 0f;
            }
        }
    }

    float Range()
    {
        Vector3 playerPos = move.player.transform.position;
        return Vector3.Distance(transform.position, playerPos);
    }

    void Shoot()
    {
        GameObject obj = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        obj.GetComponent<Rigidbody>().velocity = muzzle.transform.forward * shootSpeed;
        obj.GetComponent<BulletScript>().bulletUptime = bulletUptime;
        explosion.Play();
    }
}
