using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSettings : MonoBehaviour
{
    public MapType mapType;
    public bool isRandom;
    public bool isRepeating;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
