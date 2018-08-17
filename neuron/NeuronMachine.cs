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
        double a = 5;

        //variables and arrays
        int CountX = 1;

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
        
        static public Dictionary<int[], double> weights = new Dictionary<int[], double>();
        static public int Count = 0; //count of layers
        public List<Layer> layers = new List<Layer>();
        List<double> X = new List<double>(); //input X
        List<double> Y = new List<double>(); //output Y
        List<double> T = new List<double>();

        /// <summary>
        /// 1.1 Create neuron machine with X inputs and set A 
        /// </summary>
        /// <param name="CountX"></param>
        /// <param name="A"></param>
        public NeuronMachine(int CountX, int A)
        {
            this.CountX = CountX;
            X = new List<double>(CountX); //create list for input
            a = A;//set A for neuron machine
        }

        //1.2 Create Layer for neuron machine with N neurons
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
            layers.Add(l); //add created layer to list of layers
        }

        //2.1 Begin work neuron machine(send data to layer)
        public List<double> Work(List<double> Data)
        {
            X = Data;//for anything
            if (layers.Count >= 2)
            {
                layers[0].Work(X);//send x to first layer
                for (int i = 1; i < layers.Count; i++)
                {
                    layers[i].Work(layers[0].Z);
                }
                Y = layers[layers.Count - 1].Z;
            }
            return Y;
        }

        //3.1 Teach all layer of neuron machine
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
