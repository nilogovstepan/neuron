using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuron
{
    //******************************************NEURON*****************************************

    public class Neuron
    {
        int index = 0; //индекс нейрона в слое?
        double Zin = 0;
        double Zout = 0;
        List<double> w; //all weigths to neuron
        List<double> dw; //delta for every weight
        List<double> z; //all weigthing signal to neuron
        List<double> s; //all signal to neuron
        double _sigma; //sigma для обучения
        double _sigmaOut = 0;
        double bias = 0;
        double dbias = 0;

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
            for (int i = 0; i < s.Count; i++)
            {
                z.Add(s[i] * w[i]);
            }
            ZIN();
            ZOUT();
            return Zout;
        }

        //3.1.1.1
        public double SigmaY(double t, double a)
        {
            _sigma = (t - Zout) * Fd(Zin);
            int i = 0;
            dw = new List<double>();
            foreach (double _w in w)
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
                w[i] += dw[i++];
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
            
        }

        public void ZIN()
        {
            foreach (double zj in z)
            {
                Zin += zj;
            }
            Zin += bias;
        }

        public double SolveZ(int index, double weight, double x)
        {
            double z = weight * x;
            return z;
        }

        public void ZOUT()
        {
            Zout = F(Zin);
        }

        public double F(double x)
        {
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
