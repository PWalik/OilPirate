using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuControl : MonoBehaviour
{
    public List<GameObject> parts;

    public void ChangePart(int part)
    {
        for (int i = 0; i < parts.Count; i++)
        {
            if (part == i)
                parts[i].SetActive(true);
            else parts[i].SetActive(false);
        }
    }

    public void StartGame(int intType)
    {
        MenuSettings menuSet = FindObjectOfType<MenuSettings>();
        if(menuSet == null)
        {
            GameObject obj = new GameObject();
            obj.name = "TransObject";
            menuSet = obj.AddComponent<MenuSettings>();
        }
        switch(intType)
        {
            case 0:
                menuSet.mapType = MapType.goal;
                menuSet.isRepeating = false;
                menuSet.isRandom = false;
                break;
            case 1:
                menuSet.mapType = MapType.kill;
                menuSet.isRepeating = false;
                menuSet.isRandom = false;
                break;
            case 2:
                menuSet.mapType = MapType.survive;
                menuSet.isRepeating = false;
                menuSet.isRandom = false;
                break;
            case 3:
                menuSet.mapType = MapType.goal;
                menuSet.isRepeating = true;
                menuSet.isRandom = true;
                break;
        }
        SceneManager.LoadScene(1);
    }
}
