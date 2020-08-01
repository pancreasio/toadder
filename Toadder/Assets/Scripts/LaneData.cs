using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneData : MonoBehaviour
{
    public bool walkable;
    public Vector3 movementDirection;
    public float movementSpeed;
    public int enemyCount;

    public GameObject laneLimit;
    public GameObject enemyPrefab;

    void Start()
    {
        if (enemyPrefab != null && laneLimit != null)
        {
            for (int i = 0; i < enemyCount; i++)
            {

                float positionOffset = laneLimit.transform.position.x / 2f * Mathf.Sign(movementDirection.x);

                Vector3 newPosition = new Vector3(i * 2 * Mathf.Abs(laneLimit.transform.position.x) / enemyCount - positionOffset,
                    transform.position.y, transform.position.z);

                Quaternion newRotation = Quaternion.identity;
                if (movementDirection.x > 0f)
                {
                    //newRotation = Quaternion.Euler(0f, 0f, 180f);
                }

                Vector3 newScale = Vector3.one;
                if (movementDirection.x > 0f)
                {
                    newScale.x = -newScale.x;
                }
                GameObject spawnedObject = Instantiate(enemyPrefab, newPosition, newRotation);
                spawnedObject.GetComponent<TravellingObject>().parentLane = this;
                if (movementDirection.x > 0f)
                {
                   spawnedObject.transform.localScale = new Vector3(-spawnedObject.transform.localScale.x, spawnedObject.transform.localScale.y, spawnedObject.transform.localScale.z);
                }
            }
        }
    }
}
