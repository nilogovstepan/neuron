using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuron
{

    class NeuronMachine
    {
        //hyperparameters
        /// <summary>
        ///Скорость обучения Нейросистемы
        /// </summary>
        private double a;

        //variables and arrays
        int CountX; // Количество входов в нейросеть

        //int CountWX = 1;
        //int CountZ = 1;
        //int CountWZ = 1;
        //int CountY = 1;
        //public double x = 0;
        //public double wx = 0;
        //public double zin = 0;
        //public double zout = 0;
        //public double wz = 0;
        //public double yin = 0;
        //public double yout = 0;
        //public double sk = 0;
        //public double szin = 0;
        //public double szout = 0;
        //public double dwx = 0;
        //public double dwz = 0;

        //public NeuronMachine()
        //{
        //    //Initialization
        //    Random r = new Random(DateTimeOffset.Now.Millisecond);
        //    for (int i = 0; i < CountWX; i++) { wx = r.Next(0, 10000) / 10000.0f; wz = r.Next(0, 10000) / 10000.0f; }
        //    int f = 0;
        //}

        //public double Work(double X)
        //{
        //    //solve 
        //    x = X;
        //    for (int i = 0; i < CountZ; i++) { zin = x * wx; zout = F(zin); }
        //    for (int i = 0; i < CountY; i++) { yin = zout * wz; yout = F(yin); }
        //    return yout;
        //}

        //public void Teach(double t)
        //{
        //    //MORerror
        //    //from hidden layer
        //    sk = (t - yout) * Fd(yin);
        //    dwz = a * sk * zout;
        //    //from input layer
        //    szin = sk * wz;
        //    szout = szin * Fd(szin);
        //    dwx = a * szout * x;

        //    //change weight
        //    wz += dwz;
        //    wx += dwx;

        //    //show

        //}

        //double F(double x)
        //{
        //    //double xx = 1.0 / (1.0 + Math.Exp(-x));
        //    return 1.0 / (1.0 + Math.Exp(-x));
        //}

        //double Fd(double x)
        //{
        //    double a = F(x);
        //    return a * (1 - a);
        //}

        //class Link
        //{
        //    int a = 0, b = 0;
        //    double aout = 0;
        //    double bin = 0;
        //    double w = 0;

        //    public Link(int A, int B)
        //    {
        //        a = A;
        //        b = B;
        //        Random r = new Random(DateTimeOffset.Now.Millisecond);
        //        w = (double)(r.Next(10000)) / 10000.0;

        //    }

        //    public Link(int A, int B, double W)
        //    {
        //        a = A;
        //        b = B;
        //        w = W;

        //    }



        //}

        //******************************************************************************************************

        //static public Dictionary<int[], double> weights = new Dictionary<int[], double>();

        
        static public int Count = 0; //count of layers
        public List<Layer> layers = new List<Layer>(); //list of layers
        List<double> X = new List<double>(); //input X
        List<double> Y = new List<double>(); //output Y
        List<double> T = new List<double>(); //teach list

        /// <summary>
        /// 1.1 Создаёт нейросеть с X входами, скоростью обучения A и слоями с количеством нейронов указанных в массиве ninl.
        /// </summary>
        /// <param name="CountX">Количество входов в нейросеть</param>
        /// <param name="A">Скорость обучения нейросети</param>
        /// <param name="ninl">Массив в котором каждый элемент указывает сколько нейронов будет в слое.
        /// От количества элементов массива зависит количество слоёв в нейросети.</param>
        public NeuronMachine(int CountX, int A, int[] ninl)
        {
            this.CountX = CountX;
            X = new List<double>(CountX); //create list for input
            a = A;//set A for neuron machine
            for (int i = 0; i < ninl.Length; i++)
            {
                Console.WriteLine("Слой " + i);
                CreateLayer(ninl[i]);
            }    
        }

        /// <summary>
        /// 1.2 Create Layer for neuron machine with N neurons
        /// </summary>
        /// <param name="CountOfNeuron">Количество нейронов в сети</param>
        public void CreateLayer(int CountOfNeuron)
        {
            Layer l;
            if (layers.Count == 0)
            {
                l = new Layer(CountOfNeuron, CountX);
            }
            else
            {
                l = new Layer(CountOfNeuron, layers[Count].CountOfNeuron());
            }
            layers.Add(l);
        }

        /// <summary>
        /// 2.1 Begin work neuron machine(send data to layer)
        /// </summary>
        /// <param name="Data">Входные данные</param>
        /// <returns>Возвращает решение нейросети в виде массива весов на выходящие точки Y нейросети</returns>
        public List<double> Work(List<double> Data)
        {
            X = Data;//для обработки входных данных уже в массиве X
            if (layers.Count >= 2)
            {
                Console.WriteLine("Слой "+0);
                layers[0].Work(X);//send x to first layer
                for (int i = 1; i < layers.Count; i++)
                {
                    Console.WriteLine("Слой "+ i);
                    layers[i].Work(layers[i-1].Z);
                }
                Y = layers[layers.Count - 1].Z;
            }
            return Y;
        }

        /// <summary>
        /// 3.1 Teach all layer of neuron machine
        /// </summary>
        /// <param name="T">Массив обучающих данных</param>
        public void Teach(List<double> T)
        {
            this.T = T;
            layers[layers.Count - 1].TeachY(T, a);//Teach output layer
            for (int i = layers.Count - 1; i == 0; --i)
            {
                layers[i].Teach(layers[i + 1]._sigma, a);//Teach hidden layer
            }
            for (int i = layers.Count; i == 0; --i)
            {
                layers[i].ChangeWeights();//Teach hidden layer
            }
        }
                
    }

    //*************************************NEURON MACHINE END********************************************************

}
