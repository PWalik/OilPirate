using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHit : MonoBehaviour
{
    [SerializeField]
    int minBarrelsOnHit, maxBarrelsOnHit;

    [SerializeField]
    float minBarrelDropRange, maxBarrelDropRange, barrelDropHeight;

    PlayerController control;
    bool isPlayer;

    [SerializeField]
    GameObject barrelPrefab;

    [SerializeField]
    GameObject sinkPrefab;

    [SerializeField]
    float invincibilityTime;
    bool isInvincible;
    float inviTimer;
    private void Start()
    {
        control = GetComponent<PlayerController>();
        if (control != null)
            isPlayer = true;
    }


    private void Update()
    {
        if (isInvincible)
        {
            inviTimer += Time.deltaTime;
            if (inviTimer >= invincibilityTime)
                isInvincible = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Explosion")
        {
            GetHit();
        }
        else if (other.tag == "Barrel" && isPlayer)
            CollectBarrel(other);
    }


    void CollectBarrel(Collider barrelCol)
    {
        Destroy(barrelCol.gameObject);
        control.CollectBarrels(1);
    }


    void GetHit()
    {
        if (isPlayer)
        {
            if (isInvincible)
                return;

            if (control.BarrelCount > 0)
            {
                int barrels = Random.Range(minBarrelsOnHit, maxBarrelsOnHit);
                DropBarrels(control.LoseBarrels(barrels));
                isInvincible = true;
            }
            else
            {
                PlayerSink();
            }
        }
        else EnemySink();
    }

    void EnemySink()
    {
        DropBarrels(Random.Range(minBarrelsOnHit, maxBarrelsOnHit));
        Instantiate(sinkPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void PlayerSink()
    {
        control.Die();
    }

    void DropBarrels(int howMany)
    {
        for (int i = 0; i < howMany; i++)
        {
            float x = RandomizeRange();
            float z = RandomizeRange();
            GameObject obj = Instantiate(barrelPrefab, transform.position, transform.rotation);
            BarrelThrow bthrow = obj.GetComponent<BarrelThrow>();
            Vector3 target = new Vector3(transform.position.x + x, 0, transform.position.z + z);
            bthrow.target = target;
            bthrow.throwHeight = barrelDropHeight;
        }
    }

    float RandomizeRange()
    {
        float a;
        do
        {
            a = Random.Range(-maxBarrelDropRange, maxBarrelDropRange);
        } while (Mathf.Abs(a) < minBarrelDropRange);
        return a;
    }


    public void Destroy()
    {
        Destroy(gameObject);
    }

}
