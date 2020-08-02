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

    public Grid levelGrid;
    public GameObject playerSpawnPoint;
    public CinemachineVirtualCamera FollowCamera;
    public List<Objective> objectiveList;

    void Awake()
    {
        LevelInstance = this.gameObject;
    }

    void Start()
    {
        GameFlowManager.gameInstance.StartGame();
    }

    public void UpdateObjectives()
    {
        int completedObjectives = 0;
        foreach (Objective objective in objectiveList)
        {
            if (objective.completed)
                completedObjectives++;
        }
        if(completedObjectives >= objectiveList.Count)
            GameFlowManager.gameInstance.LevelCompleted();
    }

    public void ResetCameraTarget(GameObject followTarget)
    {
        FollowCamera.Follow = followTarget.transform;
    }

}
