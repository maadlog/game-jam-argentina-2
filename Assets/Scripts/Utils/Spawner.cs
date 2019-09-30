using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{

	public GameObject gameObjectToSpawn;
	public float timeToSpawn = 2f;
    public int spawnDistance = 12;

    private Vector3[] spawnPoints;
	private float timerToSpawn;

    // Start is called before the first frame update
    void Start()
	{
        InitializeSpawnPoints();
	}

    private int spawnPointIndex;
	// Update is called once per frame
	void Update()
	{
		if (timerToSpawn < 0f)
		{
			// select random spawn point index
			spawnPointIndex = Random.Range(0, spawnPoints.Length);


            //Generate random point
            var offset = GetRandomOffset2D(Random.Range(5,12));
            // Add random point to the spawnerPoint to get the new position
            var spawnPosition = spawnPoints[spawnPointIndex] + offset;
            
            Instantiate(gameObjectToSpawn, spawnPosition, Quaternion.identity);

			// Reset timer
            timerToSpawn = timeToSpawn;
		}
		else
		{
			timerToSpawn -= Time.deltaTime;
		}
	}

    //Generates a random array of Points arround the spawner
    // called only once on start
    private void InitializeSpawnPoints()
    {
        var totalPoints = Random.Range(4, 7);
        spawnPoints = Enumerable.Range(0, totalPoints).Select(i =>
        {
            return GetRandomOffset2D(spawnDistance);
        }).ToArray();
    }

    /// <summary>
    /// Generates a Random Vector3 with
    ///     a size of offsetDistance,
    ///     a random direction,
    ///     and z = 0
    /// </summary>
    /// <param name="offsetDistance">Result's size</param>
    /// <returns>Vector3</returns>
    private Vector3 GetRandomOffset2D(int offsetDistance = 1)
    {
        Vector3 offset = Random.onUnitSphere;
        offset.z = 0;
        return offset.normalized * offsetDistance;
    }
}
