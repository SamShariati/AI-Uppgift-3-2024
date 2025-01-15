using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NeuralNetwork : MonoBehaviour
{
    //Buggar dessa värden reseta i inspectorn
    int[] networkShape = { 6, 32, 2 };
    public Layer[] layers;
    public int NN_ID;

    public void Awake()
    {
        layers = new Layer[networkShape.Length - 1];

        for (int i = 0; i < layers.Length; i++)
        {
            layers[i] = new Layer(networkShape[i], networkShape[i + 1]);
        }
        NN_ID = Random.Range(1, 1000);
        //Debug.Log(NN_ID);
    }
    

    public float[] Brain(float[] inputs)
    {
        for (int i = 0; i < layers.Length; i++)
        {
            //indata
            if (i == 0)
            {
                layers[i].ForwardPass(inputs);
                layers[i].ActivationFunction();
            }
            //utdata
            else if (i == layers.Length - 1)

            {
                layers[i].ForwardPass(layers[i - 1].nodeArray);
            }
            //dold
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
        Layer[] tempLayers = new Layer[networkShape.Length - 1];
        for (int i = 0; i < layers.Length; i++)
        {
            tempLayers[i] = new Layer(networkShape[i], networkShape[i + 1]);
            System.Array.Copy(layers[i].weightsArray, tempLayers[i].weightsArray, layers[i].weightsArray.GetLength(0) * layers[i].weightsArray.GetLength(1));
            System.Array.Copy(layers[i].biasesArray, tempLayers[i].biasesArray, layers[i].biasesArray.GetLength(0));
        }

        return tempLayers;
    }
    public int CopyID()
    {
        return NN_ID;
    }

    public void MutateNetwork(float mutationChance, float mutationAmount)
    {
        for (int i = 0; i < layers.Length; i++)
        {
            layers[i].MutateLayer(mutationChance, mutationAmount);
        }
    }


}
