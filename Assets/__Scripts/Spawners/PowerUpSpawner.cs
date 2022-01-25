using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class PowerUpSpawner : MonoBehaviour
{
      // == private fields == 
    private const string SPAWN_PowerUp_METHOD = "SpawnOnePowerUp"; 
    [SerializeField] private PowerUpID PowerUpPrefab;
    [SerializeField] private float spawnDelay = 0.5f;
    [SerializeField] private float spawnInterval = 0.25f;

    private GameObject PowerUpParent;
    private Stack<SpawnPoint> spawnStack;
    private SpawnPoint[] spawnPointArray; // find this at the Start()

    // Start is called before the first frame update
    void Start()
    {
        PowerUpParent = GameObject.Find("PowerUpParent");
        if(!PowerUpParent)
        {
            PowerUpParent = new GameObject("PowerUpParent");
        }
        spawnPointArray = GetComponentsInChildren<SpawnPoint>();

        EnableSpawning();
    }

    private void EnableSpawning()
    {   
        // create the stack of shuffled points- fill the stack
        ShuffleSpawnPoints();

        // start the process of spawning enemies.
        // repeat spawning of a single enemy
        InvokeRepeating(SPAWN_PowerUp_METHOD, spawnDelay, spawnInterval);
    }

    private void SpawnOnePowerUp()
    {   
        if(spawnStack.Count == 0)
        {
            // fill the stack
            ShuffleSpawnPoints();
        }

        var localSpawnPoint = spawnStack.Pop();

        //int index = 0;
        var powerUp = Instantiate(PowerUpPrefab, PowerUpParent.transform);
        // randomise the index - set the position on the screen
        //index = Random.Range(0, spawnPoint.Length);
        //enemy.transform.position = spawnPoint[index].transform.position;
        powerUp.transform.position = localSpawnPoint.transform.position;
        //enemy.GetComponent<EnemyBehaviour>().ScoreValue = enemyScoreValue;
    }

    private void ShuffleSpawnPoints()
    {
        spawnStack = ListUtilities.CreateShuffledStack(spawnPointArray);
    }
}
