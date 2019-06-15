using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField]
    ParticleSystem center;
    [SerializeField]
    ParticleSystem[] Ring;

    [SerializeField]
    float minSize, maxSize;

    private void Start()
    {
        Play();
        float size = Random.Range(minSize, maxSize);
        transform.localScale *= size;
    }

    public void Play()
    {
        center.Play();
        for (int i = 0; i < Ring.Length; i++)
        {
            Ring[i].Play();
        }
    }
}
