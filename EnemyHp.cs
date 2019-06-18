using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour {

    [SerializeField] Collider collissionMesh;
    [SerializeField] ParticleSystem enemyHitPrefab;
    [SerializeField] ParticleSystem enemyDeathPrefab;
    int hitPoints = 8;
	// Use this for initialization
	void Start () {
		
	}

    private void OnParticleCollision(GameObject other)
    {
        DamageTaken();
        enemyHitPrefab.Play();
        if(hitPoints < 1)
        {
            Die();
        }
    }

    private void Die()
    {
        var vfx = Instantiate(enemyDeathPrefab, transform.position, Quaternion.identity);
        vfx.Play();
        Destroy(vfx.gameObject, vfx.main.duration);
        Destroy(gameObject);
    }

    private void DamageTaken()
    {
        hitPoints = hitPoints - 1;
    }
}
