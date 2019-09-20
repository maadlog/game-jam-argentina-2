using UnityEngine;
using System.Collections.Generic;


public class EnemySpawner : MonoBehaviour
{
    public GameObject prefab;
    public GameObject spawnPoint;

    float enemyRate = 2; // 1 Enemies generated every 2 secs
    float enemyDelay = 2; // Time delay to generate enemy

    bool simultaneousSpawn;

    float enemyRateDecrease = 0.8f; // How much the rate decreases with each generation
    float minimunEnemyRate = 0.6f; //Minimun rate for enemy spawnage

    // Start is called before the first frame update
    void Start()
    {
        
    }

    int spawnedInThisGeneration;
    int deltaNextGeneration = 10;

    // Update is called once per frame
    void Update()
    {
        enemyDelay -= Time.deltaTime;
        
        if (enemyDelay <= 0)
        {
            ResetTimers();

            SpawnEnemy();

            if (spawnedInThisGeneration >= deltaNextGeneration)
            {
                ResetGenerationCounters();
                MoveSpawnPoint();
            }
            else
            {
                spawnedInThisGeneration++;
            }
        }
    }

    private void MoveSpawnPoint()
    {
        //Generate random point in sphere centered on this Spawner
        Vector3 newPosition = Random.onUnitSphere;
        newPosition.z = 0;
        newPosition = newPosition.normalized * spawnPoint.transform.position.magnitude; // Normalize direction and get distance

        spawnPoint.transform.position = newPosition;
    }

    private void ResetGenerationCounters()
    {
        spawnedInThisGeneration = 0;
        deltaNextGeneration = Random.Range(8, 12);
    }

    private void SpawnEnemy()
    {
        float offsetRotation;
        if (spawnPoint.transform.position.x > 0)
        { 
            offsetRotation = Random.Range(0f, 180f);
        } else
        {
            offsetRotation = Random.Range(0f, -180f);
        }
        

        Instantiate(
            prefab //Enemy prefab
            , spawnPoint.transform.position
            , Quaternion.Euler(0,0, offsetRotation));
        

        
       
    }

    private void ResetTimers()
    {
        //Reset timers
        enemyDelay = enemyRate;
        //Decrease rate
        enemyRate *= enemyRateDecrease;
        if (enemyRate < minimunEnemyRate)
        {
            enemyRate = minimunEnemyRate;
        }
    }


}
