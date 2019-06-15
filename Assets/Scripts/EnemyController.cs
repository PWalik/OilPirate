using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    EnemyShoot shoot;
    EnemyMovement move;
    private void Awake()
    {
        GetReference();
    }
    public void StopEnemy()
    {
        GetReference();

        shoot.isShooting = false;
        move.isMoving = false;
        move.DisableFoam();
    }

    public void StartEnemy()
    {
        GetReference();
        shoot.isShooting = true;
        move.isMoving = true;
        move.EnableFoam();
    }

    void GetReference()
    {
        if (shoot == null)
            shoot = GetComponent<EnemyShoot>();
        else if (move == null)
            move = GetComponent<EnemyMovement>();
    }
}
