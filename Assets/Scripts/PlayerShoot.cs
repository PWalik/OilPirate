using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    GameObject barrelPrefab;
    [SerializeField]
    Transform crosshairTransform;
    [SerializeField]
    float cooldown;
    [SerializeField]
    GoOnCd cd;
    [SerializeField]
    float throwHeight;
    PlayerController control;
    float timer;
    bool isOnCD;
    bool isTurned;
    public float ThrowHeight { get => throwHeight; }
    public bool isShooting;
    void Start()
    {
        timer = cooldown;
        control = GetComponent<PlayerController>();
    }

    void Update()
    {
        if(isShooting)
        {
            if (control.BarrelCount == 0)
            {
                if (!isTurned)
                {
                    isTurned = true;
                    isOnCD = true;
                    cd.TurnCD(true);
                    cd.isOnCD = false;
                }
            }
            else
            {
                if (isTurned)
                {
                    isTurned = false;
                    timer = cooldown;
                }
                timer += Time.deltaTime;
                if (timer >= cooldown && isOnCD)
                {
                    isOnCD = false;
                    cd.TurnCD(false);
                }

                if (Input.GetKeyDown(KeyCode.Mouse0) && !isOnCD)
                {
                    isOnCD = true;
                    cd.TurnCD(true);
                    Shoot();
                    timer = 0f;
                }
            }
        }
    }

    void Shoot()
    {
        GameObject obj = Instantiate(barrelPrefab, transform.position, transform.rotation);
        BarrelThrow bthrow = obj.GetComponent<BarrelThrow>();
        bthrow.throwHeight = throwHeight;
        bthrow.org = GetComponent<Collider>();
        bthrow.target = crosshairTransform.position;
        control.LoseBarrels(1);
    }
}
