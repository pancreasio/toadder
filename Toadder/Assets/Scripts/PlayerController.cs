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

    // Start is called before the first frame update
    private void Awake()
    {
        if (playerInstance == null)
            playerInstance = this.gameObject;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        remainingLives = maxLives;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer(Vector3 respawnPosition)
    {
       Instantiate(FrogPrefab, respawnPosition, Quaternion.identity, transform);
    }

    public void ReportDeath()
    {
        remainingLives -= 1;
        if (remainingLives > 0)
            GameFlowManager.gameInstance.PlayerDied();
        else
            GameFlowManager.gameInstance.PlayerLost();
    }
}
