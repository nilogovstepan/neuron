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

        NeuronMachine a;// = new NeuronMachine();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //textBox3.Text = Convert.ToString(a.Work(Convert.ToDouble(textBox1.Text)));
            //listBox1.Items.Add("WORK*****************************");
            //listBox1.Items.Add("x=" + a.x);
            //listBox1.Items.Add("wx=" + a.wx);
            //listBox1.Items.Add("zin=" + a.zin);
            //listBox1.Items.Add("zout=" + a.zout);
            //listBox1.Items.Add("wz=" + a.wz);
            //listBox1.Items.Add("yin=" + a.yin);
            //listBox1.Items.Add("yout=" + a.yout);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //listBox1.Items.Add("TEACH*****************************");
            //a.Teach(Convert.ToDouble(textBox2.Text));
            //listBox1.Items.Add("sk=" + a.sk);
            //listBox1.Items.Add("dwz=" + a.dwz);
            //listBox1.Items.Add("szin=" + a.szin);
            //listBox1.Items.Add("szout=" + a.szout);
            //listBox1.Items.Add("dwx=" + a.dwx);
            //listBox1.Items.Add("wx=" + a.wx);
            //listBox1.Items.Add("wz=" + a.wz);
        }

        NeuronMachine n;
        List<double> X;
        /// <summary>
        ///1.Click for Create neuron machine 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            n = new NeuronMachine(2, 5);
            if (n != null)
            {
                n.CreateLayer(2);
                n.CreateLayer(1);
            }
        }

        /// <summary>
        /// 2. Create List X and do Work neuron machine
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 3. Teach neuron machine
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
