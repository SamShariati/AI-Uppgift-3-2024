using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    public float spawnRate = 1;
    public int floorScale = 1;
    public GameObject foodPrefab;
    public float timeElapsed = 0;
    public int nrFoodStart;
    private float lastFoodSpawned = 0;

    private void Start()
    {
       for (int i = 0; i < nrFoodStart; i++)
       {
            SpawnFoodPellet();
       }
    }

    private void FixedUpdate()
    {
        timeElapsed += Time.time;
        if (Time.time > spawnRate + lastFoodSpawned)
        {
            SpawnFoodPellet();
            lastFoodSpawned = Time.time;
        }
    }


    private void SpawnFoodPellet()
    {
        int x = Random.Range( -50, 51 ) * floorScale;
        int z = Random.Range(-50, 51) * floorScale;

        Instantiate(foodPrefab, new Vector3((float)x, 0.75f, (float)z), Quaternion.identity );
    }















}
