using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    ParticleSystem foamR, foamL;
    [SerializeField]
    float speed;
    public Transform player;
    [SerializeField]
    float minDistanceFromPlayer, maxDistanceFromPlayer;
    [SerializeField]
    float reactEveryXMs;
    float timer;
    float distanceFromPlayer;
    [SerializeField]
    LookAtPlayer look;
    Transform trans;
    Rigidbody rigid;
    Vector3 pos;
    bool isFoamEnabled;
    public bool isMoving;
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        trans = GetComponent<Transform>();
        look.player = player;
        distanceFromPlayer = Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
        EnableFoam();
    }
    void Update()
    {
        if (isMoving == true)
        {
            rigid.velocity = Vector3.zero;
            timer += Time.deltaTime;
            if (timer > reactEveryXMs)
            {
                timer = 0;
                pos = DetermineDirection();
            }

            rigid.MovePosition(transform.position + pos * speed);
        }
    }


    Vector3 DetermineDirection()
    {
        float x = player.position.x - trans.position.x;
        float z = player.position.z - trans.position.z;
        float dist = Vector3.Distance(trans.position, player.position);
        if (dist > distanceFromPlayer)
        {
            transform.LookAt(player);
            if(!isFoamEnabled)
            EnableFoam();
            return new Vector3(x, 0, z);
        }
        if (isFoamEnabled)
            DisableFoam();
        return Vector3.zero;
    }

    public void EnableFoam()
    {
        foamL.Play();
        foamR.Play();
        isFoamEnabled = true;
    }

    public void DisableFoam()
    {
        foamL.Stop();
        foamR.Stop();
        isFoamEnabled = false;
    }

}
