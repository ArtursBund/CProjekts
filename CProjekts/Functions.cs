using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MathNet.Numerics.Integration;



namespace CProjekts
{
    public class Functions
    {        
        public double ConvulationWithTimeDelay(AnalyticalFunctions A1, AnalyticalFunctions A2, List<double> forA1, List<double> forA2, double Delay)
        {
            double MinLimit = Delay > 0 ? 10 * forA1[2] : 10 * forA2[2];
            double MaxLimit = Delay > 0 ? 10 * forA2[2] : 10 * forA1[2];

            return SimpsonRule.IntegrateComposite(x => A1.Value(forA1, x) * A2.Value(forA1, x + Delay), MinLimit, MaxLimit, 4);
        }

        public double FunctionCombination(List<AnalyticalFunctions> functions, List<List<double>> parameters, double t)
        {
            double rez = 1;
            var col = functions.Zip(parameters, (x, y) => new { X = x, Y = y });
            foreach (var entry in col)
            {
                rez = rez * entry.X.Value(entry.Y, t);
            }
            return rez;
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

        public ValueTuple<double[], double[]> NormalizeData(RawData Data, double region, double offset)
        {
            int dataPoints = 0;
            double XDiffSum = 0;
            double YDiffSum = 0;
            double[] XData = new double[Data.XData.Length];
            if (region > (Data.XData[Data.XData.Length - 1] - Data.XData[0])) { throw new InputOutOfBondsException("Region is larger than data interval"); }
            if (offset > Data.XData[Data.XData.Length - 1] || offset < Data.XData[0]) { throw new InputOutOfBondsException("Offset is out of bonds"); }
            Data.Offset(offset);
            for (int i = 0; Data.XData[i] < Data.XData[0] + region; i++)
            {
                XDiffSum = XDiffSum + Data.XDifference[i];
                YDiffSum = YDiffSum + Data.YDifference[i];
                dataPoints++;
            }
            double[] Abs_Diff = new double[Data.XData.Length];
            for (int i = 0; i < Abs_Diff.Length; i++)
            {
                XData[i] = Data.XData[i];
                Abs_Diff[i] = Math.Sqrt(Math.Pow(Data.XDifference[i] - XDiffSum / dataPoints, 2) + Math.Pow(Data.YDifference[i] - YDiffSum / dataPoints, 2));                
            }            
            return (Abs_Diff, XData);
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
