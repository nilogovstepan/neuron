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

        /// <summary>
        /// 1.2.1 Create layer in neuron machine with N neurons and X enters in every neuron in layer
        /// </summary>
        /// <param name="CountOfNeuron">Количество нейронов в слое</param>
        /// <param name="CountX">Количество входящих связей (нейронов предыдущего слоя)</param>
        public Layer(int CountOfNeuron, int CountX)
        {
            neurons = new List<Neuron>();
            CreateNeurons(CountOfNeuron, CountX);
        }

        /// <summary>
        /// 1.2.2 Create N neurons with X entry in every neuron
        /// </summary>
        /// <param name="CountOfNeurons">Количество нейронов в слое</param>
        /// <param name="CountX">Количество входящих связей (нейронов предыдущего слоя)</param>
        public void CreateNeurons(int CountOfNeurons, int CountX)
        {
            for (int i = 0; i < CountOfNeurons; i++)
            {
                Console.WriteLine("Нейрон " + i);
                Neuron n = new Neuron(CountX);
                neurons.Add(n);
            }
        }

        /// <summary>
        /// 2.1.1 Layer work with entry data
        /// </summary>
        /// <param name="X">Входные данные в слой</param>
        public void Work(List<double> X)
        {
            SendXAll(X);
        }

        /// <summary>
        /// 2.1.2 Data sends for all neurons in layer
        /// </summary>
        /// <param name="X">Входные данные в слой</param>
        void SendXAll(List<double> X)
        {

            Z = new List<double>();
            int i = 0;
            foreach (Neuron n in neurons)
            {
                Console.WriteLine("Нейрон "+ i++);
                Z.Add(n.Work(X));
            }

        }

        /// <summary>
        /// 3.1.1 Teach OUTPUT layer
        /// </summary>
        /// <param name="T">Массив данных для обучения</param>
        /// <param name="A">Скорость обучения нейросети</param>
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
        /// <param name="SigmaK">Массив ошибок с предыдущего слоя</param>
        /// <param name="A">Скорость обучения нейросети</param>
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
        
        /// <summary>
        /// Возвращает количество нейронов в слое
        /// </summary>
        /// <returns></returns>
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
