using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {


    const int gridSize = 10;
    Vector2Int gridPos;

    public bool isExplored = false;

    public bool isPlacable = true;

    // Use this for initialization

    public Waypoint exploredFrom;


    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
                Mathf.RoundToInt(transform.position.x / gridSize),
                Mathf.RoundToInt(transform.position.z / gridSize)
                          );
    }
    public int Getgridsize()
    {
        return gridSize;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isPlacable)
            {
                FindObjectOfType<TowerFactory>().AddTower(this);
            }
            else
            {
                print("cant place here");
            }
        }
    }

}
