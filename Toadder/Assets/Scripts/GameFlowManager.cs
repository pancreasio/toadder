using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    private LevelData currentLevel;

    public static GameFlowManager gameInstance;

    public float levelTime;

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
        levelTime = 0f;
        UIManager.currentUiManager.UpdateLives();
        UIManager.currentUiManager.UpdateScore();
        UIManager.currentUiManager.UpdateTime();
    }

    // Update is called once per frame
    void Update()
    {
        levelTime += Time.deltaTime;
        UIManager.currentUiManager.UpdateTime();
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

    public void LevelCompleted()
    {

    }
}
