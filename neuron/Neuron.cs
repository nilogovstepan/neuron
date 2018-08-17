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

        /// <summary>
        /// 1.2.2.1 Create Neuron with X enters and create random weight for all enters and bias
        /// </summary>
        /// <param name="CountX">Количество связей нейрона (входов)</param>
        public Neuron(int CountX)
        {
            Random r = new Random(DateTimeOffset.Now.Millisecond);
            w = new List<double>();
            for (int i = 0; i < CountX; i++)
            {
                double rand = (r.Next(0, 10000) / 10000.0f);
                w.Add(rand);
                Console.WriteLine("Вес [" + i+"] = "+rand);
            }
            bias = r.Next(0, 10000) / 10000.0f;
            Console.WriteLine("Вес [bias] = " + bias);
        }

        /// <summary>
        /// 2.1.2.1 Neuron get data and solve
        /// </summary>
        /// <param name="X">Входные данные в нейрон</param>
        /// <returns>Возвращает выходящий сигнал нейрона</returns>
        public double Work(List<double> X)
        {
            z = new List<double>();
            s = X;
            for (int i = 0; i < s.Count; i++)
            {
                double zz = s[i] * w[i];
                z.Add(zz);
                Console.WriteLine("Входящий сигнал[" + i + "] = " + s[i]);
                Console.WriteLine("Вес[" + i + "] = " + w[i]);
                Console.WriteLine("Взвешенный сигнал["+i+"] = "+ zz);
            }
            ZIN();
            ZOUT();
            Console.WriteLine("Zin = "+ Zin);
            Console.WriteLine("Zout = "+ Zout);
            return Zout;
        }

        /// <summary>
        /// 3.1.1.1 Нахождение ошибки в слое Y
        /// </summary>
        /// <param name="t">Идеальное значение</param>
        /// <param name="a">Скорость обучения</param>
        /// <returns></returns>
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

        /// <summary>
        /// 3.1.2.1
        /// </summary>
        /// <param name="sigmaK">Ошибка нейрона</param>
        /// <param name="a">Скорость обучения</param>
        /// <returns></returns>
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


        /// <summary>
        /// 2.1.2.2 Суммирует входящие сигналы на нейрон, включая смещение. Сохраняет в переменную Zin.
        /// </summary>
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


        /// <summary>
        /// 2.1.2.3 Вычисляет выходящий сигнал из нейрона Сохраняет в переменную Zout.
        /// </summary>
        public void ZOUT()
        {
            Zout = F(Zin);
        }


        /// <summary>
        /// Активационная функция, возвращает данные на отрезке [-1;1]
        /// </summary>
        /// <param name="x">Параметры для вычисления активационной функции</param>
        /// <returns>Возвращает решение активационной функции</returns>
        public double F(double x)
        {
            return Math.Pow(1.0 + Math.Exp(-x),-1);
        }

        /// <summary>
        /// Производная активационной функции
        /// </summary>
        /// <param name="x">Параметры для вычисления производной активационной функции</param>
        /// <returns>Возвращает решение производной активационной функции</returns>
        public double Fd(double x)
        {
            double a = F(x);
            return a * (1 - a);
        }
        
    }

    //**************************************************NEURON END********************************************

}
