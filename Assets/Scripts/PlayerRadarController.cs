using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRadarController : MonoBehaviour
{
    [SerializeField]
    GameObject goalRadarPrefab, enemyRadarPrefab;
    [SerializeField]
    Transform radarParent;
    ObjectiveController objControl;
    public void CreateNewRadar(bool isEnemy, Transform target, ObjectiveController objc)
    {
        if (objControl == null)
            objControl = objc;
        GameObject prefab;
        if (isEnemy)
            prefab = enemyRadarPrefab;
        else prefab = goalRadarPrefab;
        GameObject obj = Instantiate(prefab, transform.position, prefab.transform.rotation);
        obj.transform.SetParent(radarParent);
        obj.GetComponent<RadarRotate>().target = target;
    }
}
