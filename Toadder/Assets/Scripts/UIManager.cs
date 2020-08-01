using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public Text livesText;
    public Text timeText;

    public static UIManager currentUiManager;

    void Awake()
    {
        currentUiManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLives()
    {
        string newText = "Lives: " + PlayerController.playerInstance.GetComponent<PlayerController>().remainingLives.ToString();
        livesText.text = newText;
    }

    public void UpdateScore()
    {
        string newText = "Score: " + PlayerController.playerInstance.GetComponent<PlayerController>().score.ToString();
        scoreText.text = newText;
    }

    public void UpdateTime()
    {
        string newText = "Time: ";
        newText += Mathf.Round(GameFlowManager.gameInstance.levelTime) + "s";
        timeText.text = newText;
    }
}
