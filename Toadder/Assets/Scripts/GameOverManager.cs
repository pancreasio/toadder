using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager gameOverManagerInstance;
    public GameObject winText;
    public GameObject lostText;
    public Text finalScoreText;

    public static event GameFlowManager.GameplayEvent OnGameOverLoaded;

    void Awake()
    {
        gameOverManagerInstance = this;
    }

    void Start()
    {
        if(OnGameOverLoaded != null)
            OnGameOverLoaded.Invoke();
    }

    public void UpdateFinalScore(int score)
    {
        finalScoreText.text = "Final Score: " + score + " points";
    }

    public void ActivateWinText()
    {
        winText.SetActive(true);
    }

    public void ActivateLostText()
    {
        lostText.SetActive(true);
    }
}
