using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork : MonoBehaviour
{
    //Buggar dessa värden reseta i inspectorn
    [HideInInspector] public int[] networkShape = { 6, 32, 2 };
    [HideInInspector] public Layer[] layers;

    public void Start()
    {
        layers = new Layer[networkShape.Length - 1];

        for (int i = 0; i < layers.Length; i++)
        {
            layers[i] = new Layer(networkShape[i], networkShape[i + 1]);
        }
    }

    public float[] Brain(float[] inputs)
    {
        for (int i = 0; i < layers.Length; i++)
        {
            if (i == 0)
            {
                layers[i].ForwardPass(inputs);
                layers[i].ActivationFunction();
            }
            else if (i == layers.Length - 1)

            {
                layers[i].ForwardPass(layers[i - 1].nodeArray);
            }
            else
            {
                layers[i].ForwardPass(layers[i - 1].nodeArray);
                layers[i].ActivationFunction();
            }
        }

        return layers[layers.Length - 1].nodeArray;
    }

    public Layer[] CopyLayers()
    {
        Layer[] tempLayers = new Layer[layers.Length-1];
        for (int i = 0; i < layers.Length; i++)
        {
            tempLayers[i] = new Layer(networkShape[i], networkShape[i + 1]);
            System.Array.Copy (layers[i].weightsArray, tempLayers[i].weightsArray, layers[i].weightsArray.GetLength(0) * layers[i].weightsArray.GetLength(1));
            System.Array.Copy(layers[i].biasesArray, tempLayers[i].biasesArray, layers[i].biasesArray.GetLength(0));
        }

        return tempLayers;
    }


    public void MutateNetwork(float mutationChance, float mutationAmount)
    {
        for (int i = 0; i < layers.Length; i++)
        {
            layers[i].MutateLayer(mutationChance, mutationAmount);
        }
    }

}
