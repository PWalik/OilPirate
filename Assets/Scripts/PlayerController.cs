using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    [SerializeField]
    public int maxBarrelCount;

    [SerializeField]
    public int startBarrelCount;

    PlayerMovement move;
    PlayerShoot shoot;
    [SerializeField]
    ObjectiveController objControl;
    [SerializeField]
    GameObject sinkPrefab;
    int barrelCount;

    public int BarrelCount { get => barrelCount; }

    void Awake()
    {
        shoot = GetComponent<PlayerShoot>();
        move = GetComponent<PlayerMovement>();
    }

    public void SetBarrelCount(int barrels)
    {
        maxBarrelCount = barrels;
        startBarrelCount = barrels;
        barrelCount = barrels;
    }


    public int LoseBarrels(int howManyBarrels)
    {
        int tempCount = barrelCount;
        barrelCount -= howManyBarrels;
        if (barrelCount < 0)
        {
            barrelCount = 0;
            return tempCount;
        }
        return howManyBarrels;
    }


    public void CollectBarrels(int howManyBarrels)
    {
        barrelCount += howManyBarrels;
        if (barrelCount > maxBarrelCount)
            barrelCount = maxBarrelCount;
    }

    public void Die()
    {
        Instantiate(sinkPrefab, transform.position, transform.rotation);
        objControl.Lose();
        Destroy(gameObject);
    }

    public void StopPlayer()
    {
        GetReference();
        move.isMoving = false;
        shoot.isShooting = false;
        move.DisableBackFoam();
        move.DisableFoam();
    }

    public void StartPlayer()
    {
        GetReference();
        move.isMoving = true;
        shoot.isShooting = true;
    }

    void GetReference()
    {
        if (shoot == null)
            shoot = GetComponent<PlayerShoot>();
        else if (move == null)
            move = GetComponent<PlayerMovement>();
    }
}
