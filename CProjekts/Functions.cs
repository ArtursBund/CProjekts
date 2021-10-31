using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CProjekts
{
    public class Functions
    {
        public double AnalyticalFunction(double x, double[] P)
        {
            if (x < 0)
            {
                return 0;
            }
            return P[0] * (1 - Math.Exp(-x / P[1])) * Math.Exp(-x / P[2]) + P[3] * (1 - Math.Exp(-x / P[4])) * Math.Exp(-x / P[5]); //+ P[6] * Math.Exp(-x / P[7]);
        }

        public ValueTuple<double[], double[], double[]> ReadDataFromFile(string[] line)
        {
            double[] X_Data = new double[line.Length - 1];
            double[] X_Diff = new double[line.Length - 1];
            double[] Y_Diff = new double[line.Length - 1];

            for (int i = 0; i < line.Length - 1; i++)
            {
                string[] subs = line[i + 1].Split(',');
                if (subs.Length != 3) { return (null, null, null); }
                X_Data[i] = Convert.ToDouble(subs[0]);
                X_Diff[i] = Convert.ToDouble(subs[1]);
                Y_Diff[i] = Convert.ToDouble(subs[2]);
            }

            return (X_Data, X_Diff, Y_Diff);
        }
        public void ButtonOnOff(bool state, Control.ControlCollection Controls)
        {
            foreach (Control ctrl in Controls)
            {
                if (ctrl is Button)
                {
                    Button btn = (Button)ctrl;
                    btn.Enabled = state;
                }
            }
        }

        public ValueTuple<double[], double[]> NormalizeData(double[] X_Data_G, double region, double[] X_Diff_G, double[] Y_Diff_G, double offset)
        {
            int dataPoints = 0;
            double XDiffSum = 0;
            double YDiffSum = 0;
            for (int i = 0; X_Data_G[i] < X_Data_G[0] + region; i++)
            {
                XDiffSum = XDiffSum + X_Diff_G[i];
                YDiffSum = YDiffSum + Y_Diff_G[i];
                dataPoints++;
            }
            double[] Abs_Diff = new double[X_Data_G.Length];
            for (int i = 0; i < X_Data_G.Length; i++)
            {
                Abs_Diff[i] = Math.Sqrt(Math.Pow(X_Diff_G[i] - XDiffSum / dataPoints, 2) + Math.Pow(Y_Diff_G[i] - YDiffSum / dataPoints, 2));
                X_Data_G[i] = X_Data_G[i] - offset;
            }
            return (Abs_Diff, X_Data_G);
        }
        
        public bool RunCheckList(List<ICheck> Checks, TextBox box, Control.ControlCollection Controls, string type)
        {
            string error=null;
            foreach (var test in Checks)
            {
                if (!test.Check(box.Text)) { error=test.GetType().Name; break; }
            }
            if(error == null) { return true; }
            MessageBox.Show("Could not convert " + box.Name + " to " + type + ". Error Type: " + error);
            ButtonOnOff(true, Controls);
            return false;
        }
    }

}
