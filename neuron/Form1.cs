using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace neuron
{
    public partial class Form1 : Form
    {

        NeuronMachine a = new NeuronMachine();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = Convert.ToString(a.Work(Convert.ToDouble(textBox1.Text)));
            listBox1.Items.Add("WORK*****************************");
            listBox1.Items.Add("x=" + a.x);
            listBox1.Items.Add("wx=" + a.wx);
            listBox1.Items.Add("zin=" + a.zin);
            listBox1.Items.Add("zout=" + a.zout);
            listBox1.Items.Add("wz=" + a.wz);
            listBox1.Items.Add("yin=" + a.yin);
            listBox1.Items.Add("yout=" + a.yout);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("TEACH*****************************");
            a.Teach(Convert.ToDouble(textBox2.Text));
            listBox1.Items.Add("sk=" + a.sk);
            listBox1.Items.Add("dwz=" + a.dwz);
            listBox1.Items.Add("szin=" + a.szin);
            listBox1.Items.Add("szout=" + a.szout);
            listBox1.Items.Add("dwx=" + a.dwx);
            listBox1.Items.Add("wx=" + a.wx);
            listBox1.Items.Add("wz=" + a.wz);
        }

        class NeuronMachine
        {
            //hyperparameters
            /// <summary>
            ///Скорость обучения Нейросистемы
            /// </summary>
            double a = 5; 

            //variables and arrays
            int CountX = 1;
            int CountWX = 1;
            int CountZ = 1;
            int CountWZ = 1;
            int CountY = 1;
            public double x = 0;
            public double wx = 0;
            public double zin = 0;
            public double zout = 0;
            public double wz = 0;
            public double yin = 0;
            public double yout = 0;
            public double sk = 0;
            public double szin = 0;
            public double szout = 0;
            public double dwx = 0;
            public double dwz = 0;

            public NeuronMachine()
            {
                //Initialization
                Random r = new Random(DateTimeOffset.Now.Millisecond);
                for (int i = 0; i < CountWX; i++) { wx = r.Next(0, 10000) / 10000.0f; wz = r.Next(0, 10000) / 10000.0f; }
                int f = 0;
            }

            public double Work(double X)
            {
                //solve 
                x = X;
                for (int i = 0; i < CountZ; i++) { zin = x * wx; zout = F(zin); }
                for (int i = 0; i < CountY; i++) { yin = zout * wz; yout = F(yin); }
                return yout;
            }

            public void Teach(double t)
            {
                //MORerror
                //from hidden layer
                sk = (t - yout) * Fd(yin);
                dwz = a * sk * zout;
                //from input layer
                szin = sk * wz;
                szout = szin * Fd(szin);
                dwx = a * szout * x;

                //change weight
                wz += dwz;
                wx += dwx;

                //show

            }

            double F(double x)
            {
                //double xx = 1.0 / (1.0 + Math.Exp(-x));
                return 1.0 / (1.0 + Math.Exp(-x));
            }

            double Fd(double x)
            {
                double a = F(x);
                return a * (1 - a);
            }

            class Link
            {
                int a = 0, b = 0;
                double aout = 0;
                double bin = 0;
                double w = 0;

                public Link(int A, int B)
                {
                    a = A;
                    b = B;
                    Random r = new Random(DateTimeOffset.Now.Millisecond);
                    w = (double)(r.Next(10000)) / 10000.0;

                }

                public Link(int A, int B, double W)
                {
                    a = A;
                    b = B;
                    w = W;

                }



            }

            //******************************************************************************************************
            //Dictionary for weight

            static public Dictionary<int[], double> weights = new Dictionary<int[], double>();
            static public int Count=0; //count of layers
            public List<Layer> layers = new List<Layer>();
            List<double> X=new List<double>(); //input X
            List<double> Y = new List<double>(); //output Y
            List<double> T = new List<double>();

            //1.1 Create neuron machine with X inputs and set A 
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
                //Count++;
                layers.Add(l); //add created layer to list of layers
            }

            //2.1 Begin work neuron machine(send data to layer)
            public List<double> Work(List<double> Data)
            {
                X = Data;//for anything
                if (layers.Count >= 2)
                { 
                    layers[0].Work(X);//send x to first layer
                    for (int i = 1; i < layers.Count;i++)
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
                layers[layers.Count - 1].TeachY(T,a);//Teach output layer
                for (int i = layers.Count-1; i == 0; --i)
                {
                    layers[i].Teach(layers[i+1]._sigma,a);//Teach hidden layer
                }
                for (int i = layers.Count; i == 0; --i)
                {
                    layers[i].ChangeWeights();//Teach hidden layer
                }
            }

            //public void CreateLayer()
            //{
            //    Layer l = new Layer(Count++);
            //    layers.Add(l);
            //}

            //public void CreateLayer(int CountOfNeuron)
            //{
            //    Layer l = new Layer(Count++);
            //    l.CreateNeurons(CountOfNeuron);
            //    layers.Add(l);
            //}

            



            //*****************************************LAYER*******************************************

            public class Layer {
                int CountX = 0;
                //int Count = 0; //count of neurons in layer
                
                int index;
                List<Neuron> neurons;
                public List<double> Z;
                public List<double> _sigma;
                List<double> sigmaIn;

                //1.3 Create layer in neuron machine with N neurons and X enters in every neuron in layer
                public Layer(int CountOfNeuron, int CountX)
                {
                    neurons = new List<Neuron>();
                    //index = Index;
                    CreateNeurons(CountOfNeuron, CountX);
                    

                }

                //1.4 Create N neurons with X entry in every neuron
                public void CreateNeurons(int CountOfNeurons, int CountX)
                {
                    for (int i = 0; i < CountOfNeurons; i++)
                    {
                        Neuron n = new Neuron(CountX);
                        neurons.Add(n);
                        //Random r = new Random();
                        //double w = r.Next(0, 10000) / 10000.0f;

                        //int[] m = { index, ++Count, j};
                        //weights.Add(m,w);
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
                    foreach(Neuron n in neurons)
                    {
                        _sigma.Add(n.SigmaY(T[i++],A));
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
                    foreach(Neuron n in neurons)
                    {
                        sigmaIn.Add(n.SigmaIn(SigmaK[i++],A));
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
                    //index = Index;
                }

                

                public void CreateNeuron(int Index)
                {

                    Neuron n = new Neuron(++Count);
                    neurons.Add(n);
                }

                public void CreateNeuron(int Index, int CountX)
                {
                    Neuron n = new Neuron(CountX);
                    neurons.Add(n);
                    //Random r = new Random();
                    //double w = r.Next(0, 10000) / 10000.0f;
                    //index = Index;
                    //int[] m = { index, ++Count, j};
                    //weights.Add(m,w);
                }

                //public void CreateNeurons(int CountOfNeurons)
                //{
                //    for (int i = 0; i < CountOfNeurons; i++)
                //    {
                //        Neuron n = new Neuron(neurons.Count);
                //        neurons.Add(n);
                //    }
                //}

                

            }
            //****************************************LAYER END****************************************


            //******************************************NEURON*****************************************

            class Neuron
            {
                int index = 0; //индекс нейрона в слое?
                //Dictionary<int, double> weightsFrom;//индескы и веса от соответствующих нейронов
                //Dictionary<int, double> weightsTo;//индексы и веса к соответствующим нейронам
                //Dictionary<int, double> Z;//взвешенный сигнал от соотвествующего нейрона
                //Dictionary<int, double> ZTo;
                double Zin=0;
                double Zout = 0;

                List<double> w; //all weigths to neuron
                List<double> dw; //delta for every weight
                List<double> z; //all weigthing signal to neuron
                List<double> s; //all signal to neuron
                double _sigma; //sigma для обучения
                double _sigmaOut = 0;
                double bias = 0;
                double dbias=0;

                //public Neuron(int Index)
                //{
                //    index = Index;
                //    weightsFrom = new Dictionary<int, double>();
                //    weightsTo = new Dictionary<int, double>();
                //    Z = new Dictionary<int, double>();
                //}


                //1.5 Create Neuron with X enters and create random weight for all enters
                public Neuron(int CountX)
                {
                    Random r = new Random(DateTimeOffset.Now.Millisecond);
                    w = new List<double>();
                    for (int i = 0; i < CountX; i++)
                    {
                        w.Add((r.Next(0, 10000) / 10000.0f));
                    }
                    bias = r.Next(0, 10000) / 10000.0f;
                }

                //2.4 Neuron get data and solve
                public double Work(List<double> X)
                {
                    z = new List<double>();
                    s = X;
                    for (int i=0;i<s.Count;i++) {
                        z.Add(s[i]*w[i]);
                        }
                    ZIN();
                    ZOUT();
                    return Zout;
                }

                //3.1.1.1
                public double SigmaY(double t, double a)
                {
                    _sigma = (t-Zout)*Fd(Zin);
                    int i = 0;
                    dw = new List<double>();
                    foreach(double _w in w)
                    {
                        dw.Add(a * _sigma * s[i++]);
                    }
                    dbias = a * t;
                    return _sigma;

                }

                public void ChangeWeights()
                {
                    int i = 0;
                    foreach (double _w in w)
                    {
                        w[i]+=dw[i++];
                    }
                    bias += dbias;
                }

                public double SigmaIn(double sigmaK, double a)
                {
                    foreach (double _w in w)
                    {
                        _sigma = sigmaK * _w;
                    }
                    _sigmaOut = _sigma * Fd(Zin);
                    int i = 0;
                    foreach (double _w in w)
                    {
                        dw.Add(a * _sigma * s[i++]);
                    }
                    dbias = a * _sigmaOut;
                    return _sigmaOut;
                    //int i = 0;
                    //foreach (double _w in w)
                    //{
                    //    dw.Add(a * _sigma * s[i++]);
                    //}
                    //dbias = a * sigmaIN;
                    //return _sigma;

                }

                public void ZIN()
                {
                    foreach(double zj in z)
                    {
                        Zin += zj;
                    }
                    Zin += bias;
                    //return Zin;
                }

                public void AddLinkFrom(int index, double weigth)
                {
                    //weightsFrom.Add(index, weigth);
                }

                public void AddLinkTo(int index, double weigth)
                {
                    //weightsTo.Add(index, weigth);
                }

                public void FindZTo(int index)
                {
                    //ZTo.Add(index, weightsTo[index] * Zout);
                }

                public double SolveZ(int index, double weight, double x)
                {
                    double z = weight * x;
                    //Z.Add(index,z);
                    return z;
                } 

                public void ZOUT (){
                    Zout = F(Zin);
                }

                public double F(double x) {
                    return 1.0 / (1.0 + Math.Exp(-x));
                }

                public double Fd(double x)
                {
                    double a = F(x);
                    return a * (1 - a);
                }



            }

            //**************************************************NEURON END********************************************

        }

        //*************************************NEURON MACHINE END********************************************************


        NeuronMachine n;
        List<double> X;
        //1.Click for Create neuron machine
        private void button3_Click(object sender, EventArgs e)
        {
            n = new NeuronMachine(2, 5);
            if (n != null)
            {
                n.CreateLayer(2);
                n.CreateLayer(1);
            }
        }

        //2. Create List X and do Work neuron machine
        private void button4_Click(object sender, EventArgs e)
        {
            X = new List<double>();
            List<double> Y = new List<double>();
            X.Add(Convert.ToDouble(textBox1.Text));
            X.Add(Convert.ToDouble(textBox2.Text));
            if (n != null)
            {
                Y = n.Work(X);
            }
            textBox3.Text = Convert.ToString(Y[0]);
        }

        //3. Teach neuron machine
        private void button5_Click(object sender, EventArgs e)
        {
            int jj = 0;
            if (n != null)
            {
                List<double> t = new List<double>();
                t.Add(Convert.ToDouble(textBox3.Text));
                n.Teach(t);
            }
        }
    }
}
