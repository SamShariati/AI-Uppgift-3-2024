using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    
    public GameObject agentPrefab;
    public GameObject agentSpawnerPrefab;
    public NeuralNetwork nn;
    public AgentSpawner agentSpawner;

    public int generation;
    public float totalEnergy;
    public float foodValue;
    public int nrFoodBeforeProduce;
    public int nrFoodEaten;
    public int maxChildren;
    public float FB = 0;
    public float LR = 0;
    private float viewDistance = 30;
    float[] distances;
    int numRayCasts = 6;
    float angleBetweenRaycasts = 30;

    public bool isMutated = false;
    private float mutationAmount = 0.8f;
    private float mutationChance = 0.2f;


    public bool isDead = false;
    public bool childrenProduced = false;


    private MoveAgent movement;

    private void Awake()
    {
        agentSpawnerPrefab = GameObject.Find("AgentSpawner");
        agentSpawner = agentSpawnerPrefab.GetComponent<AgentSpawner>();
        movement = GetComponent<MoveAgent>();
        nn = GetComponent<NeuralNetwork>();
        nrFoodEaten = 0;
        totalEnergy = 10;
        distances = new float[numRayCasts];

    }

    private void Update()
    {
        if (!isMutated)
        {
            isMutated = true;
            MutateAgent();
        }


        if (nrFoodEaten >= nrFoodBeforeProduce)
        {
            Reproduce();
        }
        ManageEnergy();

        float[] inputsToNN = CreateRaycasts();

        // Get outputs from the neural network
        float[] outputsFromNN = nn.Brain(inputsToNN);

        //Store the outputs from the neural network in variables
        FB = outputsFromNN[0];
        LR = outputsFromNN[1];
        movement.Move(FB, LR);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Food"))
        {
            totalEnergy += foodValue;
            nrFoodEaten += 1;
            SpawnFood.SpawnFoodPellet();
            Destroy(collider.gameObject);
        }
    }

    public void ManageEnergy()
    {
        totalEnergy -= Time.deltaTime;

        if (totalEnergy <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Reproduce()
    {
        int x = Random.Range(-5, 5);
        int z = Random.Range(-5, 5);
        GameObject child1 = Instantiate(agentPrefab, new Vector3(x, 1.5f, z), Quaternion.identity);
        NeuralNetwork child1_NN = child1.GetComponent<NeuralNetwork>();
        AgentManager child1_M = child1.GetComponent<AgentManager>();
        child1_NN.layers = GetComponent<NeuralNetwork>().CopyLayers();
        child1_M.generation = generation + 1;

        x = Random.Range(-5, 5);
        z = Random.Range(-5, 5);
        GameObject child2 = Instantiate(agentPrefab, new Vector3(x, 1.5f, z), Quaternion.identity);
        NeuralNetwork child2_NN = child2.GetComponent<NeuralNetwork>();
        AgentManager child2_M = child2.GetComponent<AgentManager>();
        child2_NN.layers = GetComponent<NeuralNetwork>().CopyLayers();
        child2_M.generation = generation + 1;

        if (agentSpawner.generationSaved <= generation)
        {
            generation++;
            agentSpawner.lastSavedNN.layers = GetComponent<NeuralNetwork>().CopyLayers();
            agentSpawner.lastSavedNN.NN_ID = GetComponent<NeuralNetwork>().NN_ID;
            agentSpawner.generationSaved = generation;

        }
        Destroy(gameObject);
    }

    public float[] CreateRaycasts()
    {
        RaycastHit hit;
        for (int i = 0; i < numRayCasts; i++)
        {
            float angle = ((2 * i + 1 - numRayCasts) * angleBetweenRaycasts / 2);

            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
            Vector3 rayDirection = rotation * transform.forward * -1;
            Vector3 rayStart = transform.position + Vector3.up * 0.1f;

            if (Physics.Raycast(rayStart, rayDirection, out hit, viewDistance))
            {
                Debug.DrawRay(rayStart, rayDirection * hit.distance, Color.red);
                if (hit.transform.gameObject.tag == "Food")
                {
                    distances[i] = hit.distance;
                }
                else
                {
                    distances[i] = viewDistance;
                }
            }
            else
            {
                Debug.DrawRay(rayStart, rayDirection * viewDistance, Color.red);
                distances[i] = 1;
            }
        }
        return distances;

    }

    private void MutateAgent()
    {

        mutationAmount += Random.Range(-1.0f, 1.0f) / 100;
        mutationChance += Random.Range(-1.0f, 1.0f) / 100;

        //make sure mutation amount and chance are positive using max function
        mutationAmount = Mathf.Max(mutationAmount, 0);
        mutationChance = Mathf.Max(mutationChance, 0);

        nn.MutateNetwork(mutationAmount, mutationChance);
    }


}
