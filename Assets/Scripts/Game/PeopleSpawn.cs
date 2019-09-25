using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleSpawn : MonoBehaviour
{
    public int peopleTotal = 5;
    public Transform[] spawnPoints;
    public GameObject gameObjectToSpawn;
    public float timeToSpawn = 2f;
    System.Random rand = new System.Random();
    public int peopleCont;

    int spawnPointIndex;
    float timerToSpawn;
    float time;


    void Start()
    {



    }

    float RandomTime() {
        return rand.Next(1, 4);
    }

    void Update()
    {

     //  time = RandomTime();

        //if (peopleCont <= peopleTotal)
        //{
            if (time < 0f)
            {
                // select random spawn point index
                spawnPointIndex = Random.Range(0, spawnPoints.Length);
                Instantiate(gameObjectToSpawn, spawnPoints[spawnPointIndex].position, Quaternion.identity);

                // Reset timer
                time = RandomTime();
                peopleCont++;
            }
            else 
            {
                time -= Time.deltaTime;
            }
        //}


    }
}
