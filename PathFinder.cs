using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {

    [SerializeField] Waypoint startWaypoint, endWaypoint;
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    

    Queue<Waypoint> queue = new Queue<Waypoint>();
    // Use this for initialization
    Waypoint searchCenter;
    Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

    List<Waypoint> path = new List<Waypoint>();

    public List<Waypoint> GetPath()
    {
        if (path.Count == 0)
        {
            LoadBlocks();
            BreadthfirstSearch();
            CreatePath();
        }
        return path;
    }
    

    bool isRunning = true;
   

    private void CreatePath()
    {
        path.Add(endWaypoint);
        endWaypoint.isPlacable = false;
        Waypoint previous = endWaypoint.exploredFrom;
        while(previous != startWaypoint)
        {
            path.Add(previous);
            previous.isPlacable = false;
            previous = previous.exploredFrom;
        }
        path.Add(startWaypoint);
        startWaypoint.isPlacable = false;
        path.Reverse();
    }

    private void BreadthfirstSearch()
    {
        queue.Enqueue(startWaypoint);
        while(queue.Count > 0 && isRunning)
        {
           searchCenter = queue.Dequeue();
            
            HaltIfEnd();
            ExploreNeighbors();
            searchCenter.isExplored = true;
        }
    }

    private void HaltIfEnd()
    {
        if(searchCenter == endWaypoint)
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbors()
    {
        if(!isRunning) { return; }
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoordinates = searchCenter.GetGridPos() + direction;
            if(grid.ContainsKey(neighborCoordinates))
            {
                QueueNewNeighbors(neighborCoordinates);
            }
        }
    }

    private void QueueNewNeighbors(Vector2Int neighborCoordinates)
    {
        Waypoint neighbor = grid[neighborCoordinates];
        if (neighbor.isExplored || queue.Contains(neighbor))
        {
            //do nothing
        }
        else
        {
            queue.Enqueue(neighbor);
            neighbor.exploredFrom = searchCenter;
        }
    }


    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Skipping overlapping block " + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);
                
            }
        }
        
    }
}


	
	
