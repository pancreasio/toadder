using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneData : MonoBehaviour
{
    public bool walkable;
    public Vector3 movementDirection;
    public float movementSpeed;

    public GameObject laneLimit;
    public GameObject laneSpawner;
}
