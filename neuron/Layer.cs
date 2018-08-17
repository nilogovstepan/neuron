using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuron
{
    //*****************************************LAYER*******************************************

    public class Layer
    {

        int CountX = 0;
        int index;
        List<Neuron> neurons;
        public List<double> Z;
        public List<double> _sigma;
        List<double> sigmaIn;

        //1.3 Create layer in neuron machine with N neurons and X enters in every neuron in layer
        public Layer(int CountOfNeuron, int CountX)
        {
            neurons = new List<Neuron>();
            CreateNeurons(CountOfNeuron, CountX);
            
        }

        //1.4 Create N neurons with X entry in every neuron
        public void CreateNeurons(int CountOfNeurons, int CountX)
        {
            for (int i = 0; i < CountOfNeurons; i++)
            {
                Neuron n = new Neuron(CountX);
                neurons.Add(n);
            }
        }

        //2.2 Layer work with entry data
        public void Work(List<double> X)
        {
            SendXAll(X);
        }

        //2.3 Data sends for all neurons in layer
        void SendXAll(List<double> X)
        {

            Z = new List<double>();
            foreach (Neuron n in neurons)
            {
                Z.Add(n.Work(X));
            }

        }
        
        //3.1.1 Teach OUTPUT layer
        public void TeachY(List<double> T, double A)
        {
            _sigma = new List<double>();
            int i = 0;
            foreach (Neuron n in neurons)
            {
                _sigma.Add(n.SigmaY(T[i++], A));
            }


        }

        /// <summary>
        ///3.1.2 Teach HIDDEN layer 
        /// </summary>
        /// <param name="SigmaK"></param>
        /// <param name="A"></param>
        public void Teach(List<double> SigmaK, double A)
        {
            int i = 0;
            foreach (Neuron n in neurons)
            {
                sigmaIn.Add(n.SigmaIn(SigmaK[i++], A));
            }
        }

        /// <summary>
        /// 3.1.3 Change weights in all neurons in layer
        /// </summary>
        /// <returns></returns>
        public void ChangeWeights()
        {
            foreach (Neuron n in neurons)
            {
                n.ChangeWeights();
            }
        }
        
        public int CountOfNeuron()
        {
            return neurons.Count();
        }
        
        public Layer(int Index)
        {
            neurons = new List<Neuron>();
        }
        
        public void CreateNeuron(int Index, int CountX)
        {
            Neuron n = new Neuron(CountX);
            neurons.Add(n);
        }
        
    }
    //****************************************LAYER END****************************************

}
