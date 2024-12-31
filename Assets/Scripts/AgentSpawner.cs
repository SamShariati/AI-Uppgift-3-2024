using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    public GameObject agentPrefab;
    private GameObject[] agentList;
    public NeuralNetwork lastSavedNN;
    public int generationSaved;

    float generationValue;
    float lastSavedTime;
    float delay = 3;

    private void Start()
    {
        lastSavedNN = GetComponent<NeuralNetwork>();
    }

    void FixedUpdate()
    {
        agentList = GameObject.FindGameObjectsWithTag("Agent");

        if (agentList.Length < 1)    
        {
            SpawnAgent();      
        }
    }
    void SpawnAgent()
    {
        // Instantiate a new agent
        GameObject backupChild = Instantiate(agentPrefab, new Vector3(0, 1.5f, 0), Quaternion.identity);


        // Check if a saved neural network exists
        if (generationSaved != 0)
        {
            NeuralNetwork backupNN = backupChild.GetComponent<NeuralNetwork>();
            AgentManager backupM = backupChild.GetComponent<AgentManager>();
            backupNN.layers = lastSavedNN.CopyLayers();
            backupNN.NN_ID = lastSavedNN.CopyID();
            backupM.generation = generationSaved;

        }
    }
}
