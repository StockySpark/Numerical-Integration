using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using System.IO;

namespace Lab1
{
    public partial class Form1 : Form
    {
        string path = "";
        GraphPane pane;
        bool flag1 = false;
        bool flag2 = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pane = z1.GraphPane;
        }

        double func(double x)
        {
            
            return Math.Pow(x, 3) - 3 * (Math.Pow(x, 2)) + 8;
        }

        double primitive(double x)
        {
            return (Math.Pow(x, 4) / 4 - Math.Pow(x, 3) + 8 * x);
        }

        double Integral(double a, double b, int n)
        {
            int i;
            double result, h;
            result = 0;
            h = (b - a) / n; 
            for (i = 0; i < n; i++)
            {
                result += func(a + h * (i + 0.5)) * h; 
            }
            return result;
        }

        double NL(double a, double b)
        {
            return primitive(b) - primitive(a);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            z1.GraphPane.CurveList.Clear();
            //GraphPane pane = z1.GraphPane;
            //pane.Title = "cosX";
            PointPairList list = new PointPairList();
            double xmin = /*-1.5*/Convert.ToDouble(numericUpDown1.Value);
            double xmax = /*3.5*/Convert.ToDouble(numericUpDown2.Value);
            double xmin_limit = -10;
            double xmax_limit = 80;
            double ymin_limit = -1.0;
            double ymax_limit = 1.0;
            for (double x = xmin; x <= xmax; x += 0.1)
            {
                list.Add(x, func(x)); 
            }
            // !!!
            // Ось X будет пересекаться с осью Y на уровне Y = 0
            pane.XAxis.Cross = 0.0;

            // Ось Y будет пересекаться с осью X на уровне X = 0
            pane.YAxis.Cross = 0.0;

            // Отключим отображение первых и последних меток по осям
            pane.XAxis.Scale.IsSkipFirstLabel = true;
            pane.XAxis.Scale.IsSkipLastLabel = true;

            // Отключим отображение меток в точке пересечения с другой осью
            pane.XAxis.Scale.IsSkipCrossLabel = true;


            // Отключим отображение первых и последних меток по осям
            pane.YAxis.Scale.IsSkipFirstLabel = true;

            // Отключим отображение меток в точке пересечения с другой осью
            pane.YAxis.Scale.IsSkipLastLabel = true;
            pane.YAxis.Scale.IsSkipCrossLabel = true;

            // Спрячем заголовки осей
            pane.XAxis.Title.IsVisible = false;
            pane.YAxis.Title.IsVisible = false;
            LineItem myCurve = pane.AddCurve("Function", list, Color.Blue, SymbolType.None);
            //pane.XAxis.Scale.Min = xmin_limit;
            //pane.XAxis.Scale.Max = xmax_limit;
            //pane.YAxis.Scale.Min = ymin_limit;
            //pane.YAxis.Scale.Max = ymax_limit;

            // !!! Установим заливку для кривой
            // Используем градиентную заливку от красного цвета до голубого через желтый
            // Последний параметр задает угол наклона градиента
            myCurve.Line.Fill = new ZedGraph.Fill(Color.Red, Color.Yellow, Color.Blue, 90.0f);

            z1.AxisChange();
            z1.Invalidate();

            if(flag1 && flag2)
                label7.Text = "Порядок точности - " + Math.Abs(firstAnswer - secondAnswer);
        }

        double firstAnswer = 0.0;
        private void button2_Click(object sender, EventArgs e)
        {
            firstAnswer = Integral(Convert.ToDouble(numericUpDown1.Value),
                Convert.ToDouble(numericUpDown2.Value),
                Convert.ToInt32(numericUpDown3.Value));
            label5.Text = "Значение интеграла = " + firstAnswer.ToString();
            flag1 = true;
        }

        double secondAnswer = 0.0;
        private void button3_Click(object sender, EventArgs e)
        {
            secondAnswer = NL(Convert.ToDouble(numericUpDown1.Value), Convert.ToDouble(numericUpDown2.Value));
            label6.Text = "Значение интеграла = " + secondAnswer;
            flag2 = true;
        }

    }
}
