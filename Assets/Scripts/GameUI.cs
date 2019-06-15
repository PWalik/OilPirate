using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    UIController UIcontrol;

    public void StartGameCheck()
    {
        UIcontrol.StartGame();
    }
}
