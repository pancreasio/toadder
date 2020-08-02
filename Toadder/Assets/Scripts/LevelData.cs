using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Cinemachine;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public static GameObject LevelInstance;

    public int NextLevelSceneIndex;
    public bool IsFinalLevel;
    public bool gamePaused;

    public static event GameFlowManager.GameplayEvent OnLevelInitialized;
    public static event GameFlowManager.GameplayEvent OnLevelCompleted;
    public event GameFlowManager.GameplayEvent OnMenuButtonPressed;
    public Grid levelGrid;
    public GameObject playerSpawnPoint;
    public GameObject pauseButton;
    public GameObject pauseMenu;
    public CinemachineVirtualCamera FollowCamera;
    public List<Objective> objectiveList;

    void Awake()
    {
        LevelInstance = this.gameObject;
    }

    void Start()
    {
        if(OnLevelInitialized!= null)
            OnLevelInitialized.Invoke();
        Objective.OnObjectiveCompleted += UpdateObjectives;
    }

    public void UpdateObjectives()
    {
        int completedObjectives = 0;
        foreach (Objective objective in objectiveList)
        {
            if (objective.completed)
                completedObjectives++;
        }

        if (completedObjectives >= objectiveList.Count)
        {
            if(OnLevelCompleted!=null)
                OnLevelCompleted.Invoke();
        }
    }

    public void ResetCameraTarget(GameObject followTarget)
    {
        FollowCamera.Follow = followTarget.transform;
    }

    public void PauseGame()
    {
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToMenu()
    {
        UnpauseGame();
        if(OnMenuButtonPressed != null)
            OnMenuButtonPressed.Invoke();
    }
}
