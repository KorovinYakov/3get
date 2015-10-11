using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MRC_C_sharp
{
    public partial class Form1 : Form
    {
        static double fvx(double x, double y)
        {
            return 1 - x * x - y * y;
        }

        static double fvy(double x, double y)
        {
            return 2 * x * y;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double x0 = Double.Parse(textBox1.Text);//Начальное значение x
            double y0 = Double.Parse(textBox2.Text);//конечное значение x
            int n = Int32.Parse(textBox3.Text);//количество шагов
            //double _h = Double.Parse(textBox4.Text);

            double k1, k2, k3, k4, l1, l2, l3, l4, _h = 0.02, y1, y2, x1, x2;

            zedGraphControl1.MasterPane.PaneList.Clear();
            ZedGraph.GraphPane pane = new ZedGraph.GraphPane();
            pane.CurveList.Clear();
            ZedGraph.PointPairList list = new ZedGraph.PointPairList();

            do
            {
                y0 = y0 + _h;

                k1 = _h * fvx(y0, x0);
                l1 = _h * fvy(y0, x0);
                k2 = _h * fvx(y0 + l1 / 2, x0 + k1 / 2);
                l2 = _h * fvy(y0 + l1 / 2, x0 + k1 / 2);


                x1 = x0 + (k1 + k2) / 2;
                y1 = y0 + (l1 + l2) / 2;
                x0 = x1; y0 = y1;

                listBox1.Items.Add(x1);
                listBox1.Items.Add(y1);
                list.Add(x1, y1);
                //i++;
                //printf("\n %lf", x1);
                //printf("\n %lf", y1);
            }

            while (y0 > 0.02); // ((y0 > -0.00001));//(x1 <= n);
            ZedGraph.LineItem MyCurve = pane.AddCurve("func", list, Color.Blue);
            zedGraphControl1.MasterPane.Add(pane);
            using (Graphics g = CreateGraphics())
            {
                zedGraphControl1.MasterPane.SetLayout(g, ZedGraph.PaneLayout.ExplicitCol12);
            }
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
    }
}
