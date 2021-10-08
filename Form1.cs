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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

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

        public static ValueTuple<double[], double[], double[]> ConvertData(string[] line, double Offset)
        {
            int Row = line.Length;
            double[] X_Data = new double[Row - 1];
            double[] X_Diff = new double[Row - 1];
            double[] Y_Diff = new double[Row - 1];           
            
            for (int i = 0; i < Row - 1; i++)
            {
                string[] subs = line[i + 1].Split(',');
                X_Data[i] = Convert.ToDouble(subs[0].Replace('.', ',')) - Offset;
                X_Diff[i] = Convert.ToDouble(subs[1].Replace('.', ','));
                Y_Diff[i] = Convert.ToDouble(subs[2].Replace('.', ','));
            }
            return (X_Data, X_Diff, Y_Diff);
        }

        private void buttonReadData_Click(object sender, EventArgs e)
        {
            string FileName = textFileName.Text;
            string location = Application.StartupPath;
            string FilePath = string.Format(location + @"\{0}.txt", FileName);

            DS.Data_Raw = File.ReadAllLines(FilePath);
            (DS.X_Data_G, DS.X_Diff_G, DS.Y_Diff_G) = ConvertData(DS.Data_Raw, 0);

            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[0].Points.DataBindXY(DS.X_Data_G, DS.X_Diff_G);
            chart1.Series[1].Points.DataBindXY(DS.X_Data_G, DS.Y_Diff_G);
         }   
    }
}
