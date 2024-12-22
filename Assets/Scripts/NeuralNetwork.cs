using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork : MonoBehaviour
{

    public int[] networkShape = { 2, 4, 4, 2 };
    public Layer[] layers;

    public void Awake()
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
  
}
