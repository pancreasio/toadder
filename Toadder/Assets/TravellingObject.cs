using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravellingObject : MonoBehaviour
{
    public LaneData parentLane;
    public bool walkable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(parentLane.movementDirection * parentLane.movementSpeed * Time.deltaTime);
    }
}
