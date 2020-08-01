using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravellingObject : MonoBehaviour
{
    public LaneData parentLane;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(/*parentLane.movementDirection **/ Vector3.left * parentLane.movementSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collsion)
    {
        if (collsion.tag == "LaneLimit")
        {
            Vector3 newPosition = parentLane.laneLimit.transform.position;
            newPosition.x = -newPosition.x;
            transform.position = newPosition;

        }

    }
}
