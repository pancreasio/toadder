﻿using System;
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

    public void UpdateLives(int lives)
    {
        string newText = "Lives: " + lives;
        livesText.text = newText;
    }

    public void UpdateScore(int score)
    {
        string newText = "Score: " + score;
        scoreText.text = newText;
    }

    public void UpdateTime(float time)
    {
        string newText = "Time: " + Mathf.Round(time) + "s";
        //newText += Mathf.Round(GameFlowManager.gameInstance.levelTime) + "s";
        timeText.text = newText;
    }
}
