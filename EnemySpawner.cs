using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] float spawnTimer = 3f;
    [SerializeField] EnemyMovement enemyPrefab;
	// Use this for initialization
	void Start ()
    {
        StartCoroutine(Spawner());	
	}
    IEnumerator Spawner()
    {
        while(true)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
