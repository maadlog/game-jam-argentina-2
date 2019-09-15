using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public Transform[] spawnPoints;
	public GameObject gameObjectToSpawn;
	public float timeToSpawn = 2f;

    

	int spawnPointIndex;
	float timerToSpawn;
    
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (timerToSpawn < 0f)
		{
			// select random spawn point index
			spawnPointIndex = Random.Range(0, spawnPoints.Length);


            //Generate random point in sphere centered on this Spawner
            Vector3 offset = Random.onUnitSphere;
            offset.z = 0;
            offset = offset.normalized * 15f; // Normalize direction and get distance

            var a = spawnPoints[spawnPointIndex].position + offset;
            a.z = 0;

            Instantiate(gameObjectToSpawn, a, Quaternion.identity);

			// Reset timer
            timerToSpawn = timeToSpawn;
		}
		else
		{
			timerToSpawn -= Time.deltaTime;
		}
	}
}
