using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPathFollowers : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private EnemyW enemyPrefab;
    [SerializeField] private float enemyStartSpeed = 2.0f;
    [SerializeField] private Transform startPoint;
    [SerializeField] private int enemiesInWave = 5;
    [SerializeField] private float spawnInterval = 0.5f;

    private GameObject enemyParent;

     private void Start()
    {
        enemyParent = GameObject.Find("EnemyParent");
        if(!enemyParent) enemyParent = new GameObject("EnemyParent");
        //SpawnOneEnemy();
        StartCoroutine(SpawnEnemyWave());
    }

    private IEnumerator SpawnEnemyWave()
    {
        int enemyCount = 0;
        for(enemyCount = 0; enemyCount < enemiesInWave; enemyCount++)
        {
            SpawnOneEnemy();
           
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnOneEnemy()
    {
        var enemy = Instantiate(enemyPrefab, enemyParent.transform);
        enemy.transform.position = startPoint.position;
        
        var follower = enemy.GetComponent<WaypointFollower>();

        follower.Speed = enemyStartSpeed;
        foreach (Transform t in waypoints)
        {
            follower.AddWaypoint(t.position);
        }
    }
}
