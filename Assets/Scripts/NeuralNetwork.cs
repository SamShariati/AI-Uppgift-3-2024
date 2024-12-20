using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork : MonoBehaviour
{

    //public Layer hiddenLayer1;
    //public Layer hiddenLayer2;
    //public Layer outputLayer;

    
    public class Layer
    {
        public Layer hiddenLayer1;
        public Layer hiddenLayer2;
        public Layer outputLayer;


        public float[,] weightsArray;
        public float[] biasesArray;
        public float[] nodeArray;

        private int n_nodes;
        private int n_inputs;

        public Layer(int n_inputs, int n_nodes)
        {
            weightsArray = new float[n_nodes, n_inputs];
            biasesArray = new float [n_nodes];
            biasesArray = new float [n_nodes];

            this.n_nodes = n_nodes;
            this.n_inputs = n_inputs;
        }

        public void Awake()
        {
            hiddenLayer1 = new Layer(2, 4);
            hiddenLayer2 = new Layer(4, 4);
            outputLayer = new Layer(4, 2);

        }



    }
}
