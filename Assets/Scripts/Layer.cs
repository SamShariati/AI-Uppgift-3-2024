using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Layer
{

    public float[,] weightsArray; //värden för kopplingar
    public float[] biasesArray; // bias värden för noderna
    public float[] nodeArray; // alla noder i ett lager

    private int n_nodes;
    private int n_inputs;

    public Layer(int n_inputs, int n_nodes)
    {
        weightsArray = new float[n_nodes, n_inputs];
        biasesArray = new float[n_nodes];
        //nodeArray = new float[n_nodes];

        this.n_nodes = n_nodes;
        this.n_inputs = n_inputs;
    }

    //Räknar ut den totala summan av värden för varje nod i ett lager.
    public void ForwardPass(float[] inputsArray)
    {
        nodeArray = new float[n_nodes];

        for (int i = 0; i < n_nodes; i++)
        {

            for (int j = 0; j < n_inputs; j++)
            {
                nodeArray[i] += weightsArray[i, j] * inputsArray[j];
            }

            nodeArray[i] += biasesArray[i];
        }
    }

    //Aktiveringsfunktion för varje nod
    public void ActivationFunction()
    {
        for (int i = 0; i < n_nodes; i++)
        {
            if (nodeArray[i] <= 0)
            {
                nodeArray[i] = 0;
            }
        }
    }

    public void MutateLayer(float mutationChance, float mutationAmount)
    {
        for (int i = 0; i < n_nodes; i++)
        {
            for (int j = 0; j < n_inputs; j++)
            {
                if (Random.value < mutationChance)
                {
                    weightsArray[i, j] += Random.Range(-1.0f, 1.0f) * mutationAmount;
                }
            }

            if (Random.value < mutationChance)
            {
                biasesArray[i] += Random.Range(-1.0f, 1.0f) * mutationAmount;
            }
        }
    }
    
}
