using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    private SpriteRenderer ownRenderer;

    public bool completed;

    public static event GameFlowManager.GameplayEvent OnObjectiveCompleted;
    // Start is called before the first frame update
    void Start()
    {
        ownRenderer = GetComponent<SpriteRenderer>();
        ownRenderer.enabled = false;
        completed = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ownRenderer.enabled = true;
            completed = true;
            if(OnObjectiveCompleted != null)
                OnObjectiveCompleted.Invoke();
        }
    }
}
