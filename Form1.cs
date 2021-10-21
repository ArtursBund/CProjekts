using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CProjekts
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        #region Class objects
        public class DataStorage
        {
            public double[] X_Data_G { get; set; } // X ass dati
            public double[] X_Diff_G { get; set; } // Pirmā signāla Y ass dati
            public double[] Y_Diff_G { get; set; } // Otrā signāla Y ass dati
            public double[] Abs_X_Data { get; set; } // X ass dati pēc apstrādes
            public double[] Abs_Diff { get; set; } // Apkopotais signāls
            public double[] Abs_Diff_Fit { get; set; } // Analītiskā funkcija
            public string[] Data_Raw { get; set; } // Ielasītais fails kāds ir
        } 
        DataStorage DS = new DataStorage();
        #endregion
        #region Functions
        public void buttonFunction(bool state)
        {
            foreach (Control ctrl in Controls)
            {
                if (ctrl is Button)
                {
                    Button btn = (Button)ctrl;
                    btn.Enabled=state;
                }
            }
        }
        #endregion
        #region Button functions
        private void buttonReadData_Click(object sender, EventArgs e)
        {
            buttonFunction(false);
            string FileName = textFileName.Text;
            string location = Application.StartupPath;
            string FilePath = string.Format(location + @"\{0}.txt", FileName);
            if(!File.Exists(FilePath)) 
            { 
                MessageBox.Show("File does not exist"); 
                buttonFunction(true); 
                return; 
            }

            DS.Data_Raw = File.ReadAllLines(FilePath);
            DS.X_Data_G = new double[DS.Data_Raw.Length - 1];
            DS.X_Diff_G = new double[DS.Data_Raw.Length - 1];
            DS.Y_Diff_G = new double[DS.Data_Raw.Length - 1];
            var func = new Functions();
            (DS.X_Data_G,DS.X_Diff_G, DS.Y_Diff_G)=func.ConvertData(DS.Data_Raw);

            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[0].Points.DataBindXY(DS.X_Data_G, DS.X_Diff_G);
            chart1.Series[1].Points.DataBindXY(DS.X_Data_G, DS.Y_Diff_G);
            chart1.Series[0].Name = "X_Diff";
            chart1.Series[1].Name = "Y_Diff";
            chart1.ChartAreas[0].AxisX.Minimum = DS.X_Data_G[0]-Math.Round(DS.X_Data_G[0], 2)>0 ? Math.Round(DS.X_Data_G[0], 2) : Math.Round(DS.X_Data_G[0], 2)-0.01;
            chart1.ChartAreas[0].AxisX.Maximum = DS.X_Data_G[DS.X_Data_G.Length - 1] - Math.Round(DS.X_Data_G[DS.X_Data_G.Length - 1], 2) > 0 ? Math.Round(DS.X_Data_G[DS.X_Data_G.Length - 1], 2)+0.01 : Math.Round(DS.X_Data_G[DS.X_Data_G.Length - 1], 2);
            chart1.ChartAreas[0].AxisX.Interval = (chart1.ChartAreas[0].AxisX.Maximum - chart1.ChartAreas[0].AxisX.Minimum) / 10;
            buttonFunction(true);
        }
        private void buttonNormData_Click(object sender, EventArgs e)
        {
            buttonFunction(false);
            if (!double.TryParse(textRefReg.Text, out double region))
            {
                MessageBox.Show("Could not convert Region to number");
                buttonFunction(true);
                return;
            }
            if (!double.TryParse(textOffset.Text, out double offset))
            {
                MessageBox.Show("Could not convert Offset to number");
                buttonFunction(true);
                return;
            }
            region = Convert.ToDouble(textRefReg.Text);
            offset = Convert.ToDouble(textOffset.Text);
            int dataPoints=0;
            double XDiffSum = 0;
            double YDiffSum = 0;
            for(int i=0; DS.X_Data_G[i]< DS.X_Data_G[0]+region;i++)
            {
                XDiffSum = XDiffSum + DS.X_Diff_G[i];
                YDiffSum = YDiffSum + DS.Y_Diff_G[i];
                dataPoints++;
            }
            DS.Abs_Diff = new double[DS.X_Data_G.Length];
            for (int i = 0; i<DS.X_Data_G.Length; i++)
            {
                DS.Abs_Diff[i] = Math.Sqrt(Math.Pow(DS.X_Diff_G[i] - XDiffSum / dataPoints, 2) + Math.Pow(DS.Y_Diff_G[i] - YDiffSum / dataPoints, 2));
                DS.X_Data_G[i] = DS.X_Data_G[i] - offset;
            }

            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[0].Points.DataBindXY(DS.X_Data_G, DS.Abs_Diff);
            chart1.ChartAreas[0].AxisX.Minimum = DS.X_Data_G[0] - Math.Round(DS.X_Data_G[0], 2) > 0 ? Math.Round(DS.X_Data_G[0], 2) : Math.Round(DS.X_Data_G[0], 2) - 0.01;
            chart1.ChartAreas[0].AxisX.Maximum = DS.X_Data_G[DS.X_Data_G.Length - 1] - Math.Round(DS.X_Data_G[DS.X_Data_G.Length - 1], 2) > 0 ? Math.Round(DS.X_Data_G[DS.X_Data_G.Length - 1], 2) + 0.01 : Math.Round(DS.X_Data_G[DS.X_Data_G.Length - 1], 2);
            chart1.ChartAreas[0].AxisX.Interval = (chart1.ChartAreas[0].AxisX.Maximum - chart1.ChartAreas[0].AxisX.Minimum) / 10;
            chart1.Series[0].Name = "Diff";
            chart1.Series[1].IsVisibleInLegend = false;
            chart1.ChartAreas[0].AxisX.Crossing = 0; // Varbūt uzlikt drošību
            buttonFunction(true);
        }

        private void buttonPlot_Click(object sender, EventArgs e)
        {
            var func = new Functions();
            DS.Abs_Diff_Fit = new double[DS.Data_Raw.Length - 1];
            double[] arrayP = { Convert.ToDouble(textBoxA1.Text), Convert.ToDouble(textBoxTf1.Text), Convert.ToDouble(textBoxTr1.Text), Convert.ToDouble(textBoxA2.Text), Convert.ToDouble(textBoxTf2.Text), Convert.ToDouble(textBoxTr2.Text), Convert.ToDouble(textBoxA3.Text), Convert.ToDouble(textBoxTf3.Text), Convert.ToDouble(textBoxTr3.Text) };
            for (int i=0; i< DS.Data_Raw.Length-1;i++)
            {
                DS.Abs_Diff_Fit[i] = func.Fun(DS.X_Data_G[i], arrayP);
            }
            chart1.Series[1].Points.Clear();
            chart1.Series[1].IsVisibleInLegend = true;
            chart1.Series[1].Name = "Fit";
            chart1.Series[1].Points.DataBindXY(DS.X_Data_G, DS.Abs_Diff_Fit);
            buttonFunction(true);
        }
        #endregion

    }
    public class Functions
    {
        public double Fun(double x, double[] P)
        {
            if (x < 0)
            {
                return 0;
            }
            return P[0] * (1 - Math.Exp(-x / P[1])) * Math.Exp(-x / P[2]) + P[3] * (1 - Math.Exp(-x / P[4])) * Math.Exp(-x / P[5]); //+ P[6] * Math.Exp(-x / P[7]);
        }

        public ValueTuple<double[], double[], double[]> ConvertData(string[] line)
        {
            double[] X_Data = new double[line.Length - 1];
            double[] X_Diff = new double[line.Length - 1];
            double[] Y_Diff = new double[line.Length - 1];

            for (int i = 0; i < line.Length - 1; i++)
            {
                string[] subs = line[i + 1].Split(',');
                X_Data[i] = Convert.ToDouble(subs[0]);
                X_Diff[i] = Convert.ToDouble(subs[1]);
                Y_Diff[i] = Convert.ToDouble(subs[2]);
            }

            return (X_Data, X_Diff, Y_Diff);

        }
    }
}
