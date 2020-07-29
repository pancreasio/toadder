using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    private float travelDistance;
    // Start is called before the first frame update
    void Start()
    {
        travelDistance = LevelData.LevelInstance.GetComponent<Grid>().cellSize.x *
                         LevelData.LevelInstance.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            transform.Translate(Vector3.up * travelDistance);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            transform.Translate(Vector3.up * -travelDistance);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            transform.Translate(Vector3.right * travelDistance);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            transform.Translate(Vector3.right * -travelDistance);
    }
}
