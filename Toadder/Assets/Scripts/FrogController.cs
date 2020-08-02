using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    private float travelDistance;
    private PlayerController parentController;
    public LayerMask WallLayerMask;
    private TravellingObject platformObject;
    private LaneData currentLane;

    // Start is called before the first frame update
    void Start()
    {
        travelDistance = LevelData.LevelInstance.GetComponent<Grid>().cellSize.x *
                         LevelData.LevelInstance.transform.localScale.x;
        platformObject = null;
        currentLane = null;
        parentController = PlayerController.playerInstance.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (platformObject != null)
        {
            transform.Translate(platformObject.parentLane.movementDirection * platformObject.parentLane.movementSpeed * Time.deltaTime);
        }
        else
        {
            if (currentLane != null && !currentLane.walkable)
            {
                Die();
            }

        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && !ChechIfNextToWall(Vector3.up))
            transform.Translate(Vector3.up * travelDistance);
        if (Input.GetKeyDown(KeyCode.DownArrow) && !ChechIfNextToWall(Vector3.down))
            transform.Translate(Vector3.up * -travelDistance);
        if (Input.GetKeyDown(KeyCode.RightArrow) && !ChechIfNextToWall(Vector3.right))
            transform.Translate(Vector3.right * travelDistance);
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !ChechIfNextToWall(Vector3.left))
            transform.Translate(Vector3.right * -travelDistance);
    }

    //void LateUpdate()
    //{
    //    if (platformObject != null)
    //    {
    //        transform.Translate(platformObject.parentLane.movementDirection * platformObject.parentLane.movementSpeed * Time.deltaTime);
    //    }
    //    else
    //    {
    //        if (currentLane != null && !currentLane.walkable)
    //        {
    //            Die();
    //        }
    //    }
    //}

    bool ChechIfNextToWall(Vector3 movementDirection)
    {
        RaycastHit2D wallHit2D = Physics2D.Raycast(transform.position, movementDirection, travelDistance, WallLayerMask);
        if (wallHit2D)
            return true;
        else
            return false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            Die();

        if (collision.gameObject.tag == "Platform")
            platformObject = collision.GetComponent<TravellingObject>();

        if(collision.gameObject.tag == "Lane")
            currentLane = collision.GetComponent<LaneData>();

        if(collision.gameObject.tag == "Objective")
            ObjectiveCompleted();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
            platformObject = collision.GetComponent<TravellingObject>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Platform" && platformObject.gameObject == collision.gameObject)
            platformObject = null;

        //if (collision.tag == "Lane" && currentLane != null)
        //    currentLane = null;
    }

    void Die()
    {
        parentController.ReportDeath();
        Destroy(this.gameObject);
    }

    void ObjectiveCompleted()
    {
        parentController.ReportSuccess();
        Destroy(this.gameObject);
    }
}
