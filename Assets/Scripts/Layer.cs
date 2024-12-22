using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour
{

    public float[,] weightsArray; //v�rden f�r kopplingar
    public float[] biasesArray; // bias v�rden f�r noderna
    public float[] nodeArray; // alla noder i ett lager

    private int n_nodes;
    private int n_inputs;

    public Layer(int n_inputs, int n_nodes)
    {
        weightsArray = new float[n_nodes, n_inputs];
        biasesArray = new float[n_nodes];
        nodeArray = new float[n_nodes];

        this.n_nodes = n_nodes;
        this.n_inputs = n_inputs;
    }

    //R�knar ut den totala summan av v�rden f�r varje nod i ett lager.
    public void ForwardPass(float[] inputsArray)
    {
        nodeArray = new float[n_nodes];

        for (int i = 0; i < n_nodes; i++)
        {

            for (int j = 0; j <n_inputs; j++)
            {
                nodeArray[i] += weightsArray[i, j] * inputsArray[j];
            }

            nodeArray[i] += biasesArray[i];
        }
    }

    //Aktiveringsfunktion f�r varje nod
    public void ActivationFunction()
    {
        for (int i= 0; i < n_nodes; i++)
        {
            if (nodeArray[i] <= 0)
            {
                nodeArray[i] = 0;
            }
        }
    }
}
