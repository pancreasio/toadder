using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    private LevelData currentLevel;

    public static GameFlowManager gameInstance;

    void Awake()
    {
        if (gameInstance == null)
            gameInstance = this;
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        UIManager.currentUiManager.UpdateLives();
        UIManager.currentUiManager.UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDied()
    {
        PlayerController.playerInstance.GetComponent<PlayerController>().RespawnPlayer(LevelData.LevelInstance.GetComponent<LevelData>().playerSpawnPoint.transform.position);
        UIManager.currentUiManager.UpdateLives();
    }

    public void PlayerLost()
    {
        Application.Quit();
    }
}
