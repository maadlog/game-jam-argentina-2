using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{

	public GameObject gameObjectToSpawn;
	public float timeToSpawn = 2f;
    public int spawnDistance = 12;

    public Transform[] spawnPoints;
	private float timerToSpawn;

    public PointDispatcher pointDispatcher;

    void Start()
	{
        pointDispatcher = new PointDispatcher(
            new PointDispatcher.RandomAtFixedDistanceFromPoints(
                spawnPoints.Select(x => x.position).ToArray()
                , spawnDistance));
	}

	void Update()
	{
		if (timerToSpawn < 0f)
		{
            Vector3 spawnPosition = pointDispatcher.GetNextPoint();

            Instantiate(gameObjectToSpawn, spawnPosition, Quaternion.identity);

			// Reset timer
            timerToSpawn = timeToSpawn;
		}
		else
		{
			timerToSpawn -= Time.deltaTime;
		}
	}


    
}
