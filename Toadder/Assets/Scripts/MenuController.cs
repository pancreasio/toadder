using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static event GameFlowManager.GameplayEvent OnMenuButtonPressed;
    public static event GameFlowManager.GameplayEvent OnStartButtonPressed;

    public void GoToMenu()
    {
        if(OnMenuButtonPressed!=null)
            OnMenuButtonPressed.Invoke();
        GameFlowManager.gameInstance.GoToMenu();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        if(OnStartButtonPressed!=null)
            OnStartButtonPressed.Invoke();
        //GameFlowManager.gameInstance.GoToFirstLevel();
    }
}
