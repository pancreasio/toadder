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
    public List<int> LevelIndexList;
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
        //foreach (int i in LevelIndexList)
        //{
        //    if (i == currentSceneIndex)
        //    {
        //        PlayerDied();
        //        SyncWithCurrentLevel();
        //        break;
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        levelTime += Time.deltaTime;
        if(UIManager.currentUiManager != null)
            UIManager.currentUiManager.UpdateTime();
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
        GoToMenu();
    }

    public void LevelCompleted()
    {
        GoToMenu();
    }

    public void GoToMenu()
    {
        ChangeScene(MenuSceneIndex);
    }

    public void GoToFirstLevel()
    {
        ChangeScene(LevelIndexList[0]);
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
        currentSceneIndex = nextSceneIndex;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
