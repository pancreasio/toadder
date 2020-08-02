using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static GameObject playerInstance;

    public int maxLives;
    public int remainingLives;

    public int score;

    public GameObject FrogPrefab;
    private GameObject FrogInstance;

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
        //Restart();
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

    // Update is called once per frame
    void Update()
    {
        
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
            GameFlowManager.gameInstance.PlayerDied();
        else
        {
            TopUpLives();
            GameFlowManager.gameInstance.PlayerLost();
        }

    }

    public void ReportSuccess()
    {
        TopUpLives();
        GameFlowManager.gameInstance.PlayerDied();
    }

    public void DestroyPlayer()
    {
        if(FrogInstance != null)
            Destroy(FrogInstance);
    }
}
