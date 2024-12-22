using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    
    public GameObject agentPrefab;
    public NeuralNetwork nn;
    float agent_LR;
    float agent_FB;

    public float maxHealth;
    public float currentHealth;
    public float rotationSpeed;

    public float totalEnergy;
    public float foodValue;
    public float reproductionCost;
    public int nrFoodBeforeProduce;
    public int nrFoodEaten;
    public int maxChildren;
    private int nrOfChildren;
    public float FB = 0;
    public float LR = 0;
    private float viewDistance = 30;

    private bool isMutated = false;
    private float mutationAmount = 0.8f;
    private float mutationChance = 0.2f;


    public bool isDead = false;
    public bool childrenProduced = false;

    private MoveAgent movement;

    private void Awake()
    {
        currentHealth = maxHealth;
        movement = GetComponent<MoveAgent>();
        nn = GetComponent<NeuralNetwork>();
    }

    private void Update()
    {
        if (!isMutated)
        {
            isMutated = true;
            MutateAgent();
        }


        if (nrFoodEaten >= nrFoodBeforeProduce && nrOfChildren <= maxChildren)
        {
            Reproduce();
        }
        ManageEnergy();

        float[] inputsToNN = CreateRaycasts(6, 30);

        // Get outputs from the neural network
        float[] outputsFromNN = nn.Brain(inputsToNN);

        //Store the outputs from the neural network in variables
        FB = outputsFromNN[0];
        LR = outputsFromNN[1];
        movement.Move(FB, LR);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Food")
        {
            totalEnergy += foodValue;
            nrFoodEaten += 1;
            Destroy(gameObject);
        }
    }

    public void ManageEnergy()
    {
        totalEnergy -= Time.deltaTime;

        if (totalEnergy <= 0)
        {
            this.transform.Rotate(0, 0, 90);
            Destroy(this.gameObject, 3);
            movement.enabled = false;
        }
    }

    public void Reproduce()
    {
        for (int i = 0; i < maxChildren; i++)
        {
            GameObject child = Instantiate(agentPrefab, this.transform.position, Quaternion.identity);
            child.GetComponent<NeuralNetwork>().layers = nn.CopyLayers();
            nrOfChildren++;
        }
    }

    public float[] CreateRaycasts(int numRayCasts, float angleBetweenRaycasts)
    {
        float[] distances = new float[numRayCasts];
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
