using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    ParticleSystem foamR, foamL, foamB;
    [SerializeField]
    ParticleSystem stationary;
    [SerializeField]
    float maxGoSpeed, speedUp, turnSpeed, slowDownSpeed;
    Transform trans;
    Rigidbody rigid;
    float speed;
    bool isFoamEnabled, isBackFoamEnabled;
    public bool isMoving;
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        trans = GetComponent<Transform>();
        stationary.Play();
        foamR.Stop();
        foamL.Stop();
        foamB.Stop();
    }
    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            rigid.velocity = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                GoForward();
                if (!isFoamEnabled)
                    EnableFoam();
            }
            else
            {
                if (speed > 0)
                    speed -= slowDownSpeed;
                if (speed < 0)
                    speed = 0;

                if (isFoamEnabled)
                    DisableFoam();
            }
            if (Input.GetKey(KeyCode.Space))
            {
                rigid.MovePosition(trans.position + transform.forward * speed * 2);
                if (!isBackFoamEnabled)
                    EnableBackFoam();
            }
            else
            {
                rigid.MovePosition(trans.position + transform.forward * speed);
                if (isBackFoamEnabled)
                    DisableBackFoam();
            }

            if (speed > 0)
            {
                if (Input.GetKey(KeyCode.A))
                    TurnLeft();
                else if (Input.GetKey(KeyCode.D))
                    TurnRight();
            }

        }

    }

    void GoForward()
    {
        if(speed < maxGoSpeed)
        {
            speed += speedUp;
        }

    }

    void TurnLeft()
    {
        Turn(true);
    }

    void TurnRight()
    {
        Turn(false);
    }

    void Turn(bool isLeft)
    {
        int i = 1;
        if (isLeft)
            i = -1;

        trans.transform.Rotate(new Vector3(0,turnSpeed * i, 0));
    }


    public void EnableFoam()
    {
        foamL.Play();
        foamR.Play();
        stationary.Stop();
        isFoamEnabled = true;
    }

    public void DisableFoam()
    {
        foamL.Stop();
        foamR.Stop();
        stationary.Play();
        isFoamEnabled = false;
    }

    public void EnableBackFoam()
    {
        foamB.Play();
        isBackFoamEnabled = true;
    }
    public void DisableBackFoam()
    {
        foamB.Stop();
        isBackFoamEnabled = false;
    }
}
