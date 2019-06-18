using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {

    [SerializeField] Tower towerPrefab;
    [SerializeField] int towerLimit = 3;

    Queue<Tower> towerQueue = new Queue<Tower>();

    public void AddTower(Waypoint baseWayPoint)
    {
        int numTowers = towerQueue.Count;
        if (numTowers < towerLimit)
        {
            Maketowers(baseWayPoint);
        }
        else
        {
            MoveTowers(baseWayPoint);
        }
    }

    private void MoveTowers(Waypoint baseWayPoint)
    {
        print("Tower limit reached");
        var oldTower = towerQueue.Dequeue();

        oldTower.baseWayPoint.isPlacable = true;
        baseWayPoint.isPlacable = false;

        oldTower.baseWayPoint = baseWayPoint;
        oldTower.transform.position = baseWayPoint.transform.position;

        towerQueue.Enqueue(oldTower);
    }

    private void Maketowers(Waypoint baseWayPoint)
    {
        var newTower = Instantiate(towerPrefab, baseWayPoint.transform.position, Quaternion.identity);
        baseWayPoint.isPlacable = false;

        newTower.baseWayPoint = baseWayPoint;
        baseWayPoint.isPlacable = false;

        towerQueue.Enqueue(newTower);
    }
}
