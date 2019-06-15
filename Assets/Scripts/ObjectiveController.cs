using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class ObjectiveController : MonoBehaviour
{
    [SerializeField]
    float goalAcceptableDistance;
    MapType mapType;
    MapSettings mapSettings;
    Transform player;
    Transform goal;
    List<Transform> islands;
    List<Transform> enemies;
    int enemyCount;
    PlayerRadarController radarController;
    PlayerController playerControl;
    [SerializeField]
    float secondsToSurvive;
    float timer;
    bool isCounting;
    UIController UIcontrol;
    float intBarrels, intEnemies;
    [SerializeField]
    GameObject winScreen;
    public bool isSequenceDone;
    [SerializeField]
    GameObject goalMarkerPrefab;
    [SerializeField]
    AudioClip goalClip, fightClip, surviveClip;
    [SerializeField]
    TextMeshProUGUI winText;
    [SerializeField]
    Transform camera;
    [SerializeField]
    Vector3 cameraWinLosePos;
    [SerializeField]
    Vector3 cameraWinLoseRotation;
    AudioSource source;
    MenuSettings menuSet;
    public void GetSettings(MapType type, MapSettings settings, Transform mPlayer, List<Transform> mIslands, List<Transform> mEnemies, MenuSettings menu)
    {
        mapType = type;
        mapSettings = settings;
        islands = mIslands;
        enemies = mEnemies;
        player = mPlayer;
        menuSet = menu;
        source = GetComponent<AudioSource>();
        radarController = player.GetComponent<PlayerRadarController>();
        playerControl = player.GetComponent<PlayerController>();
        UIcontrol = GetComponent<UIController>();
        playerControl.SetBarrelCount(settings.playerLives);
        isCounting = true;
        StartCoroutine(StartSequence());
    }

    IEnumerator StartSequence()
    {
        isSequenceDone = false;
        UIcontrol.SetObjectiveText(mapType);
        SetStuff(false);
        UIcontrol.StartSequence();
        while(!isSequenceDone)
        yield return null;

        SetStuff(true);
        StartObjective();
    }

    public void DoneSequence()
    {
        isSequenceDone = true;
    }

    void StartObjective()
    {
        switch (mapType)
        {
            case MapType.goal:
                goal = FindFarthestIsland();
                source.clip = goalClip;
                source.Play();
                GameObject obj = Instantiate(goalMarkerPrefab, goal.transform.position, goal.transform.rotation);
                obj.transform.localScale = new Vector3(goalAcceptableDistance * 2, 1000, goalAcceptableDistance * 2);
                UIcontrol.SetupUILists(mapSettings.playerLives, 0);
                radarController.CreateNewRadar(false, goal, this);
                break;
            case MapType.kill:
                source.clip = fightClip;
                source.Play();
                enemyCount = enemies.Count;
                intEnemies = enemyCount;
                UIcontrol.SetupUILists(mapSettings.playerLives, enemyCount);
                CreateEnemyRadars();
                break;
            case MapType.survive:
                source.clip = surviveClip;
                source.Play();
                enemyCount = enemies.Count;
                UIcontrol.SetupUILists(mapSettings.playerLives, 0);
                UIcontrol.StartTimer(secondsToSurvive);
                CreateEnemyRadars();
                break;
        }
        StartCoroutine(ObjectiveCheck());
    }

    IEnumerator ObjectiveCheck()
    {
        bool isDone = false;
        while(!isDone)
        {
            
            switch(mapType)
            {
                case MapType.goal:
                    if(MapGoalCheck())
                        isDone = true;
                    break;
                case MapType.kill:
                    if(MapKillCheck())
                        isDone = true;
                    UIEnemyCheck();
                    break;
                case MapType.survive:
                    if (MapSurviveCheck())
                    {
                        UIcontrol.StopTimer();
                        isDone = true;
                    }
                    break;
            }
            UIBarrelCheck();
            yield return null;
        }
        Win();
    }

    bool MapGoalCheck()
    {
        float dis = Vector3.Distance(player.transform.position, goal.transform.position);
        if (dis <= goalAcceptableDistance)
            return true;
        return false;
    }

    bool MapKillCheck()
    {
        CheckEnemies();
        if (enemyCount == 0)
            return true;
        return false;
    }

    bool MapSurviveCheck()
    {
        if (isCounting)
            timer += Time.deltaTime;
        CheckEnemies();
        if (timer >= secondsToSurvive || enemyCount == 0)
            return true;

        return false;
    }

    void UIBarrelCheck()
    {
        while(intBarrels > playerControl.BarrelCount)
        {
            UIcontrol.BlackBarrel();
            intBarrels--;
        }

        while (intBarrels < playerControl.BarrelCount)
        {
            UIcontrol.DeBlackBarrel();
            intBarrels++;
        }
    }

    void UIEnemyCheck()
    {
        while (intEnemies > enemyCount)
        {
            UIcontrol.BlackEnemy();
            intEnemies--;
        }
    }


    void CheckEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                enemyCount--;
                enemies.RemoveAt(i);
            }
        }
    }

    void CreateEnemyRadars()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            radarController.CreateNewRadar(true, enemies[i], this);   
        }
    }

    Transform FindFarthestIsland()
    {
        int j = 0;
        float dis = 0;
        float currDistance;
        for (int i = 0; i < islands.Count; i++)
        {
            currDistance = Vector3.Distance(player.transform.position, islands[i].transform.position);
            if(currDistance > dis)
            {
                dis = currDistance;
                j = i;
            }
        }
        return islands[j];
    }


    public void Win()
    {
        SetStuff(false);
        winText.text = "You win!";
        camera.GetComponent<CopyPosition>().enabled = false;
        camera.transform.position = player.position + cameraWinLosePos;
        camera.transform.rotation = Quaternion.Euler(cameraWinLoseRotation);
        Destroy(player.GetComponent<Collider>());
        winScreen.SetActive(true);
        StartCoroutine(WinWait(true));
        
    }

    public void Lose()
    {
        SetStuff(false);
        winText.text = "You lose!";
        camera.GetComponent<CopyPosition>().enabled = false;
        camera.transform.position = player.position + cameraWinLosePos;
        camera.transform.rotation = Quaternion.Euler(cameraWinLoseRotation);
        if (mapType == MapType.survive)
        UIcontrol.StopTimer();
        winScreen.SetActive(true);
        StartCoroutine(WinWait(false));
    }

    IEnumerator WinWait(bool isWin)
    {
        while (!Input.GetKey(KeyCode.Mouse0))
        yield return null;

        GoNext(isWin);
    }

    void GoNext(bool isWin)
    {
        int sceneNr;
        if (menuSet == null || (menuSet.isRepeating && isWin))
            sceneNr = SceneManager.GetActiveScene().buildIndex;
        else sceneNr = 0;


        SceneManager.LoadScene(sceneNr);
    }

    public void SetStuff(bool isOn)
    {
        if(isOn)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].GetComponent<EnemyController>().StartEnemy();
            }
            player.GetComponent<PlayerController>().StartPlayer();
        }
        else
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if(enemies[i] != null)
                enemies[i].GetComponent<EnemyController>().StopEnemy();
            }
            player.GetComponent<PlayerController>().StopPlayer();
        }
    }
}
