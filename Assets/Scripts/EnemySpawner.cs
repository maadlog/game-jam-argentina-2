using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject prefab;
    public GameObject spawnPoint;

    float enemyRate = 5; // 1 Enemies generated in 5 secs
    float enemyDelay = 1; // Time delay to generate enemy

    float enemyRateDecrease = 0.9f; // How much the rate decreases with each generation
    float minimunEnemyRate = 1; //Minimun rate for enemy spawnage

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyDelay -= Time.deltaTime;

        if (enemyDelay <= 0)
        {
            //Time to instantiate an enemy

            //Reset timers
            enemyDelay = enemyRate;
            //Decrease rate
            enemyRate *= enemyRateDecrease;
            if (enemyRate < minimunEnemyRate)
            {
                enemyRate = minimunEnemyRate;
            }

            //Generate random point in sphere centered on this Spawner
            Vector3 offset = Random.onUnitSphere;
            offset.z = 0;
            offset = offset.normalized * spawnDistance(); // Normalize direction and get distance

            GameObject newEnemy = Instantiate(
                prefab //Enemy prefab
                , spawnPoint.transform.position + offset //Initial Position
                , Quaternion.identity); // Dont Rotate

            EnemyMovement newEnemyMov;

            if (newEnemy.TryGetComponent<EnemyMovement>(out newEnemyMov))
            {
                newEnemyMov.VarySpeedRandom();
            }
            
        }
    }

    float spawnDistance()
    {
        return 12 * Random.Range(0.8f, 1.2f);
    }
}
