using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MapSettings
{
    [SerializeField]
    [Range(1, 100)]
    public float minMapSize, maxMapSize;

    [SerializeField]
    public int playerLives;

    [SerializeField]
    public bool isMapSymmetrical;


    [SerializeField]
    public int minIslandCount, maxIslandCount;
    [SerializeField]
    public float minIslandScale, maxIslandScale;
    [SerializeField]
    public int minEnemyCount, maxEnemyCount;
}

public enum MapType { goal, kill, survive }
public class StartGame : MonoBehaviour
{
   
    [SerializeField]
    public MapType mapToLoad;

    [SerializeField]
    float islandsDistance;

    [SerializeField]
    float enemyDistance;

    [SerializeField]
    int diffLvl;
    [Header("----------------------------------")]
    [SerializeField]
    MapSettings goalMapSettings, killMapSettings, surviveMapSettings;
    [Header("----------------------------------")]
    [SerializeField]
    GameObject map;
    [SerializeField]
    Transform player;
    [SerializeField]
    GameObject enemyPrefab;
    [SerializeField]
    List<GameObject> islandPrefabs;
    ObjectiveController objControl;
    List<Transform> islands;
    List<Transform> enemies;
    MenuSettings menuSet;
    private void Start()
    {

        islands = new List<Transform>();
        enemies = new List<Transform>();
        menuSet = FindObjectOfType<MenuSettings>();
        if (menuSet == null || menuSet.isRandom)
        {
            int f = Random.Range(0, 3);
            switch (f)
            {
                case 0:
                    mapToLoad = MapType.goal;
                    break;
                case 1:
                    mapToLoad = MapType.kill;
                    break;
                default:
                    mapToLoad = MapType.survive;
                    break;
            }
        }
        else mapToLoad = menuSet.mapType;
        objControl = GetComponent<ObjectiveController>();
        CreateMap();
    }


    void CreateMap()
    {
        MapSettings settings;

        switch (mapToLoad)
        {
            case MapType.goal:
                settings = goalMapSettings;
                break;
            case MapType.kill:
                settings = killMapSettings;
                break;
            default:
                settings = surviveMapSettings;
                break;
        }
        float mapSize = Random.Range(settings.minMapSize, settings.maxMapSize);
        map.transform.localScale = new Vector3(mapSize, map.transform.localScale.y, mapSize);

        int enemyCount = Random.Range(settings.minEnemyCount, settings.maxEnemyCount);
        int islandCount = Random.Range(settings.minIslandCount, settings.maxIslandCount);
        Vector3 pos;
        for (int i = 0; i < enemyCount; i++)
        {
            pos = FindSpawnPoint(mapSize, true);
            if(pos != Vector3.zero)
            {
                SpawnEnemy(pos);
            }
        }
        for (int i = 0; i < islandCount; i++)
        {
            pos = FindSpawnPoint(mapSize, false);
            if (pos != Vector3.zero)
            {
                SpawnIsland(settings, pos);
            }
        }
        objControl.GetSettings(mapToLoad, settings, player, islands, enemies, menuSet);
    }

    Vector3 FindSpawnPoint(float mapSize, bool isEnemy)
    {
        float distance = mapSize * 5 - 5;
        bool isComplete = false;
        float x = 0f, z = 0f;
        float baseDistance;
        if (isEnemy)
            baseDistance = enemyDistance;
        else baseDistance = islandsDistance;
        Vector3 pos;
        int j = 0;
        while (!isComplete)
        {
            x = Random.Range(-distance, distance);
            z = Random.Range(-distance, distance);

            pos = new Vector3(x, 0, z);
            isComplete = true;
            for (int i = 0; i < islands.Count; i++)
            {
                if (Vector3.Distance(islands[i].position, pos) < baseDistance + islandsDistance)
                    isComplete = false;
            }
            for (int i = 0; i < enemies.Count; i++)
            {
                if (Vector3.Distance(enemies[i].position, pos) < baseDistance + enemyDistance)
                    isComplete = false;
            }

            if (Vector3.Distance(Vector3.zero, pos) < baseDistance * 2)
                isComplete = false;
            j++;
            if (j > 100)
                return Vector3.zero;
        }
        return new Vector3(x, 0f, z);
    }

    void SpawnEnemy(Vector3 pos)
    {
        GameObject obj = Instantiate(enemyPrefab, pos, Quaternion.identity);
        obj.GetComponent<EnemyMovement>().player = player;
        enemies.Add(obj.transform);
    }

    void SpawnIsland(MapSettings settings, Vector3 pos)
    {
        GameObject obj = Instantiate(islandPrefabs[Random.Range(0, islandPrefabs.Count)], pos, Quaternion.identity);
        float scale = Random.Range(settings.minIslandScale, settings.maxIslandScale);
        int reverseX = Random.Range(-1, 1);
        if (reverseX == 0)
            reverseX = 1;
        obj.transform.localScale = new Vector3(scale * reverseX * obj.transform.localScale.x, obj.transform.localScale.y, scale * obj.transform.localScale.z);
        islands.Add(obj.transform);
    }

}
