using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    Animator startAnimation;
    [SerializeField]
    Animator endAnimation;
    [SerializeField]
    Transform barrelsParent, skullsParent;
    [SerializeField]
    GameObject barrelsUIPrefab, skullsUIPrefab;
    [SerializeField]
    Text timerText;
    [SerializeField]
    TextMeshProUGUI objectiveText;
    [SerializeField]
    string goalText, killText, surviveText;
    float timer;
    Coroutine cor;
    UIList barrelsUI, skullsUI;
    ObjectiveController objControl;
    // Start is called before the first frame update
    void Start()
    {
        objControl = GetComponent<ObjectiveController>();
        barrelsUI = barrelsParent.GetComponent<UIList>();
        skullsUI = skullsParent.GetComponent<UIList>();
    }

    public void StartSequence()
    {
        startAnimation.Play("StartAnimation");
    }

    public void StartGame()
    {
        objControl.DoneSequence();
    }

    public void SetupUILists(int barrelCount, int enemiesCount)
    {
        SetupUIList(barrelCount, barrelsUIPrefab, barrelsParent);
        SetupUIList(enemiesCount, skullsUIPrefab, skullsParent);
    }

    void SetupUIList(int count, GameObject prefab, Transform parent)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefab, parent);
            parent.GetComponent<UIList>().items.Add(obj.transform);
        }
    }

    public void BlackBarrel()
    {
        barrelsUI.BlackItem();
    }

    public void DeBlackBarrel()
    {
        barrelsUI.DeBlackItem();
    }

    public void BlackEnemy()
    {
        skullsUI.BlackItem();
    }


    public void StartTimer(float time)
    {
        timerText.color = new Color(timerText.color.r, timerText.color.g, timerText.color.b, 1f);
        timer = time;
        cor = StartCoroutine(TimerCoroutine());
    }

    public void StopTimer()
    {
        timerText.color = new Color(timerText.color.r, timerText.color.g, timerText.color.b, 0f);
        StopCoroutine(cor);
    }

    IEnumerator TimerCoroutine()
    {
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            SetTimerTime(timer);
            yield return null;
        }
    }

    void SetTimerTime(float time)
    {
        if (time <= 0)
            time = 0;
        if (time < 5f)
            timerText.text = time.ToString("F2");
        else if (time < 10f)
            timerText.text = time.ToString("F1");
        else
            timerText.text = time.ToString("F0");
    }

    public void SetObjectiveText(MapType type)
    {
        string text;
        switch(type)
        {
            case MapType.goal:
                text = goalText;
                break;
            case MapType.kill:
                text = killText;
                break;
            default:
                text = surviveText;
                break;
        }
        objectiveText.SetText(text);
    }


}
