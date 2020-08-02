using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static GameObject playerInstance;

    public int maxLives;
    private int remainingLives;

    public int score;

    public GameObject FrogPrefab;
    private GameObject FrogInstance;

    public static event GameFlowManager.GameplayEvent OnScoredPoints;
    public static event GameFlowManager.GameplayEvent OnPlayerDeath;
    public static event GameFlowManager.GameplayEvent OnPlayerLost;


    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (playerInstance == null)
        {
            playerInstance = this.gameObject;
            Restart();
        }
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        FrogController.OnObjectiveComplete += ReportSuccess;
        FrogController.OnNewHeight += AdvancedOnLevel;
        FrogController.OnDeath += ReportDeath;
    }

    public void Restart()
    {
        TopUpLives();
        score = 0;
    }

    private void TopUpLives()
    {
        remainingLives = maxLives;
    }

    public int GetLives()
    {
        return remainingLives;
    }


    public GameObject RespawnPlayer(Vector3 respawnPosition)
    {
        FrogInstance = Instantiate(FrogPrefab, respawnPosition, Quaternion.identity, transform);
        return FrogInstance;
    }

    public void ReportDeath()
    {
        remainingLives -= 1;
        if (remainingLives > 0)
        {
            if (OnPlayerDeath != null)
                OnPlayerDeath.Invoke();
        }

        else
        {
            TopUpLives();
            if(OnPlayerLost != null)
                OnPlayerLost.Invoke();
        }

    }

    void ReportSuccess()
    {
        ScorePoints(400);
        if(OnPlayerDeath!=null)
            OnPlayerDeath.Invoke();
    }

    void AdvancedOnLevel()
    {
        ScorePoints(10);
    }

    void ScorePoints(int points)
    {
        score += points;
        OnScoredPoints.Invoke();
    }

    public int GetScore()
    {
        return score;
    }

    public void DestroyPlayer()
    {
        if(FrogInstance != null)
            Destroy(FrogInstance);
    }
}
