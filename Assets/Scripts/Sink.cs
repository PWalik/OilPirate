using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{
    [SerializeField]
    float destroyAfter;
    float timer;
    [SerializeField]
    ParticleSystem sys;
    [SerializeField]
    GameObject ship;
    public void DestroyShip()
    {
        ship.SetActive(false);
        sys.Stop();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= destroyAfter)
            Destroy(gameObject);
    }
}
