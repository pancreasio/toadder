using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public static GameObject LevelInstance;

    public Grid levelGrid;
    public GameObject playerSpawnPoint;

    void Awake()
    {
        if (LevelInstance == null)
            LevelInstance = this.gameObject;
        else
            Destroy(this.gameObject);
    }

}
