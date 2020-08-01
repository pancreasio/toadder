using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public static GameObject LevelInstance;

    public Grid levelGrid;
    public GameObject playerSpawnPoint;
    public List<Objective> objectiveList;

    void Awake()
    {
        if (LevelInstance == null)
            LevelInstance = this.gameObject;
        else
            Destroy(this.gameObject);
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

}
