using System.Collections;
using System.Collections.Generic;
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
        if (nrFoodEaten >= nrFoodBeforeProduce && nrOfChildren <= maxChildren)
        {
            Reproduce();
        }
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



}
