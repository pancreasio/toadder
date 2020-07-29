using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public static GameObject LevelInstance;

    public Grid levelGrid;

    void Awake()
    {
        if (LevelInstance == null)
            LevelInstance = this.gameObject;
        else
            Destroy(this.gameObject);
    }
}
