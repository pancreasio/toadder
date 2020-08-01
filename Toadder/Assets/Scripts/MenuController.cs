using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void GoToMenu()
    {
        GameFlowManager.gameInstance.GoToMenu();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        GameFlowManager.gameInstance.GoToFirstLevel();
    }
}
