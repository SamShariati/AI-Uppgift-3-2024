using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnFood : MonoBehaviour
{
    public static SpawnFood Instance;
    public float spawnRate = 1;
    public int floorScale = 1;
    public GameObject foodPrefab;
    public int nrFoodStart;
    public Transform foodParent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (foodParent == null)
        {
            GameObject parentObj = new GameObject("FoodParent");
            foodParent = parentObj.transform;
        }

        for (int i = 0; i < nrFoodStart; i++)
        {
            SpawnFoodPellet();
        }
    }

    public static void SpawnFoodPellet()
    {
        int x = Random.Range(-50, 51) * Instance.floorScale;
        int z = Random.Range(-50, 51) * Instance.floorScale;

        GameObject newFood = Instantiate(Instance.foodPrefab, new Vector3((float)x, 1, (float)z), Quaternion.identity);

        if (Instance.foodParent != null)
        {
            newFood.transform.SetParent(Instance.foodParent);
        }
    }
}