using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    private LevelData currentLevel;

    public static GameFlowManager gameInstance;

    public delegate void GameplayEvent();

    public int SplashScreenIndex;
    public int MenuSceneIndex;
    public int GameOverSceneIndex;
    public int FirstLevelIndex;
    private int currentSceneIndex;

    private bool gameWon;

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
        SyncWithCurrentLevel();
        SplashScreenManager.OnSplashesDone += GoToMenu;
        MenuController.OnMenuButtonPressed += GoToMenu;
        MenuController.OnStartButtonPressed += GoToFirstLevel;
        LevelData.OnLevelInitialized += StartGame;
        LevelData.OnLevelCompleted += GoToNextLevel;
        PlayerController.OnPlayerLost += PlayerLost;
        PlayerController.OnPlayerDeath += PlayerDied;
        GameOverManager.OnGameOverLoaded += SetGameOver;
        gameWon = false;
    }

    // Update is called once per frame
    void Update()
    {
        levelTime += Time.deltaTime;
        if (UIManager.currentUiManager != null)
            UIManager.currentUiManager.UpdateTime(levelTime);

        if (Input.GetKeyDown(KeyCode.M))
            GoToMenu();
        if (Input.GetKeyDown(KeyCode.N))
            GoToNextLevel();
    }

    private void PlayerDied()
    {
        if (PlayerController.playerInstance != null && LevelData.LevelInstance != null)
        {
            LevelData.LevelInstance.GetComponent<LevelData>().ResetCameraTarget(
                PlayerController.playerInstance.GetComponent<PlayerController>().RespawnPlayer(LevelData.LevelInstance
                .GetComponent<LevelData>().playerSpawnPoint.transform.position));
        }

        if (UIManager.currentUiManager != null)
            UIManager.currentUiManager.UpdateLives(PlayerController.playerInstance.GetComponent<PlayerController>().GetLives());
    }

    private void SyncWithCurrentLevel()
    {
        if (UIManager.currentUiManager != null)
        {
            UIManager.currentUiManager.UpdateLives(PlayerController.playerInstance.GetComponent<PlayerController>().GetLives());
            UIManager.currentUiManager.UpdateScore(PlayerController.playerInstance.GetComponent<PlayerController>().GetScore());
            UIManager.currentUiManager.UpdateTime(levelTime);
        }

        if (PlayerController.playerInstance != null)
        {
            PlayerController.OnScoredPoints += UpdateScore;
        }

        if (LevelData.LevelInstance != null)
        {
            GameObject.Find("Level").GetComponent<LevelData>().OnMenuButtonPressed += GoToMenu;
        }

    }

    private void SetGameOver()
    {
        if (GameOverManager.gameOverManagerInstance != null)
        {
            GameOverManager.gameOverManagerInstance.UpdateFinalScore(PlayerController.playerInstance.GetComponent<PlayerController>().GetScore());
            if (gameWon)
            {
                GameOverManager.gameOverManagerInstance.GetComponent<GameOverManager>().ActivateWinText();
            }
            else
            {
                GameOverManager.gameOverManagerInstance.GetComponent<GameOverManager>().ActivateLostText();
            }
        }
    }

    private void PlayerLost()
    {
        GoToGameOver();
    }

    private void LevelCompleted()
    {
        if (LevelData.LevelInstance.GetComponent<LevelData>().IsFinalLevel)
        {
            gameWon = true;
            GoToGameOver();
        }
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
        PlayerController.playerInstance.GetComponent<PlayerController>().ResetScore();
        gameWon = false;
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

    private void UpdateScore()
    {
        if (UIManager.currentUiManager != null)
            UIManager.currentUiManager.UpdateScore(PlayerController.playerInstance.GetComponent<PlayerController>().GetScore());
    }

    private void ChangeScene(int nextSceneIndex)
    {
        levelTime = 0f;
        if (PlayerController.playerInstance != null)
            PlayerController.playerInstance.GetComponent<PlayerController>().DestroyPlayer();

        currentSceneIndex = nextSceneIndex;
        SceneManager.LoadScene(nextSceneIndex);

        SyncWithCurrentLevel();
    }
}
