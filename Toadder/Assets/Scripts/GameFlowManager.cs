using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    private LevelData currentLevel;

    public static GameFlowManager gameInstance;

    public int SplashScreenIndex;
    public int MenuSceneIndex;
    public int GameOverSceneIndex;
    public int FirstLevelIndex;
    private int currentSceneIndex;

    public float levelTime;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (gameInstance == null)
            gameInstance = this;
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        levelTime += Time.deltaTime;
        if(UIManager.currentUiManager != null)
            UIManager.currentUiManager.UpdateTime();

        if(Input.GetKeyDown(KeyCode.M))
            GoToMenu();
        if (Input.GetKeyDown(KeyCode.N))
            GoToNextLevel();
    }

    public void PlayerDied()
    {
        if (PlayerController.playerInstance != null && LevelData.LevelInstance != null)
        {
            LevelData.LevelInstance.GetComponent<LevelData>().ResetCameraTarget(
                PlayerController.playerInstance.GetComponent<PlayerController>().RespawnPlayer(LevelData.LevelInstance
                .GetComponent<LevelData>().playerSpawnPoint.transform.position));
        }

        if(UIManager.currentUiManager !=null)
            UIManager.currentUiManager.UpdateLives();
    }

    private void SyncWithCurrentLevel()
    {
        if (UIManager.currentUiManager != null)
        {
            UIManager.currentUiManager.UpdateLives();
            UIManager.currentUiManager.UpdateScore();
            UIManager.currentUiManager.UpdateTime();
        }
    }

    public void PlayerLost()
    {
        GoToGameOver();
    }

    public void LevelCompleted()
    {
        if (LevelData.LevelInstance.GetComponent<LevelData>().IsFinalLevel)
            GoToGameOver();
        else
            GoToNextLevel();
    }

    public void GoToMenu()
    {
        ChangeScene(MenuSceneIndex);
    }

    public void GoToFirstLevel()
    {
        ChangeScene(FirstLevelIndex);
    }

    public void GoToNextLevel()
    {
        ChangeScene(LevelData.LevelInstance.GetComponent<LevelData>().NextLevelSceneIndex);
    }

    public void StartGame()
    {
        PlayerDied();
        SyncWithCurrentLevel();
    }

    public void GoToGameOver()
    {
        ChangeScene(GameOverSceneIndex);
    }

    private void ChangeScene(int nextSceneIndex)
    {
        levelTime = 0f;
        if(PlayerController.playerInstance != null)
            PlayerController.playerInstance.GetComponent<PlayerController>().DestroyPlayer();

        currentSceneIndex = nextSceneIndex;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
