using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    public GameObject agentPrefab;
    private GameObject[] agentList;
    public int floorScale = 1;
    public static NeuralNetwork lastSavedNN;
    public static int logsSaved;


    private void Start()
    {
        lastSavedNN = new NeuralNetwork();
    }

    void FixedUpdate()
    {
        agentList = GameObject.FindGameObjectsWithTag("Agent");

        // if there are no agents in the scene, spawn one at a random location. 
        // This is to ensure that there is always at least one agent in the scene.
        if (agentList.Length < 1)    
        {
            SpawnAgent();
            Debug.Log(logsSaved);
        }
    }

    void SpawnAgent()
    {
        GameObject backupChild = Instantiate(agentPrefab, new Vector3(0,0,0),Quaternion.identity);
        NeuralNetwork backupNN = backupChild.GetComponent<NeuralNetwork>();
        backupNN.layers = lastSavedNN.layers;
    }
}
