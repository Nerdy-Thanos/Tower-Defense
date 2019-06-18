using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    [SerializeField] Transform objectToPan;
    Transform targetEnemy;

    const float attackRange = 30f;
    [SerializeField] ParticleSystem projectile;

    public Waypoint baseWayPoint;

	// Update is called once per frame
	void Update ()
    {
        SetTargetEnemy();
        if (targetEnemy)
        {
            objectToPan.LookAt(targetEnemy);
            FireAtEnemy();
        }
        else
        {
            Shoot(false);
        }
    }

    private void SetTargetEnemy()
    {
        var currentEnemy = FindObjectsOfType<EnemyHp>();
        if(currentEnemy.Length == 0) { return; }

        Transform closeEnemy = currentEnemy[0].transform;
        foreach(EnemyHp testEnemy in currentEnemy)
        {
            closeEnemy = GetClose(closeEnemy, testEnemy.transform);
        }
        targetEnemy = closeEnemy;
    }

    private Transform GetClose(Transform transformA, Transform transformB)
    {
        var distToA = Vector3.Distance(transform.position, transformA.position);
        var distToB = Vector3.Distance(transform.position, transformB.position);

        if(distToA < distToB)
        {
            return transformA;
        }
        return transformB;
    }

    private void FireAtEnemy()
    {
        float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
        if (distanceToEnemy <= attackRange)
        {
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }
    }
    private void Shoot(bool isActive)
    {
        var emissionModule = projectile.emission;
        emissionModule.enabled = isActive;
    }
}
