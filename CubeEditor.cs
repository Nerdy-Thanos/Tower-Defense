using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]

public class CubeEditor : MonoBehaviour {

    // Update is called once per frame

    

    Vector3 gridPos;

    Waypoint waypoint;
    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }
    void Update ()
    {
        GridSnap();
        UpdateLabel();
    }

    private void UpdateLabel()
    {
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        
        string labelText = waypoint.GetGridPos().x + "," + waypoint.GetGridPos().y;
        
        textMesh.text = labelText;
        gameObject.name = labelText;
    }

    private void GridSnap()
    {
        int gridSize = waypoint.Getgridsize();
        
        transform.position = new Vector3(
            waypoint.GetGridPos().x * gridSize,
            0f, 
            waypoint.GetGridPos().y * gridSize);
    }
}
