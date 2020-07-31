using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public Text livesText;

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
        livesText.text = PlayerController.playerInstance.GetComponent<PlayerController>().remainingLives.ToString();
    }

    public void UpdateScore()
    {
        scoreText.text = PlayerController.playerInstance.GetComponent<PlayerController>().score.ToString();
    }
}
