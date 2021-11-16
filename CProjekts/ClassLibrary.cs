using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MathNet.Numerics.Integration;

namespace CProjekts
{
    public abstract class AnalyticalFunctions
    {
       // public double Fit(double[] decision, double x)
       // {
           
       // }
        public abstract double Value(List<double> parameters, double x);

    }
    public class Gaussian : AnalyticalFunctions
    {
        public override double Value(List<double> P, double x)
        {
            if(P[2]==0) { throw new InputOutOfBondsException("Sigma for Gauss cannot be 0"); }
            return P[0] * Math.Exp(-((x - P[1]) * (x - P[1])) / (2 * P[2] * P[2]));
        }
    }
    public class ResponseFunction : AnalyticalFunctions
    {
        Functions function = new Functions();
        Gaussian gaussian = new Gaussian();
        public override double Value(List<double> P, double x)
        {
            if (x < 0)
            {
                return 0;
            }            
            if (P[1] == 0 || P[2]==0 || P[4]==0) { throw new InputOutOfBondsException("Time constant cannot be 0"); }
            List<double> gaussianParameters = new List<double>() { 1, 0, 0.05 };
            double convulationOfPulses = function.ConvulationWithTimeDelay(gaussian, gaussian, gaussianParameters, gaussianParameters, x-0.05);
            return convulationOfPulses*(P[0] * (1 - Math.Exp(-x / P[1])) * Math.Exp(-x / P[2]) + P[3] * (1 - Math.Exp(-x / P[4])) * Math.Exp(-x / P[4]));
        }
    }
    public abstract class DataObject
    {
        public double[] XData { get; set; } // X ass dati
        public void Offset(double offset)
        {
            for(int i=0; i<XData.Length;i++)
            {
                XData[i] = XData[i] - offset;
            }
        }
        public abstract void PlotData(Chart chart);
    }
    public class RawData : DataObject
    {
        public double[] XDifference { get; set; } // Stara nobīde X ass virzienā
        public double[] YDifference { get; set; } // Stara nobīde Y ass virzienā
        public string[] DataRaw { get; set; } // Ielasītais fails kāds ir
        public override void PlotData(Chart chart)
        {
            chart.Series[0].Points.Clear();
            chart.Series[1].Points.Clear();
            chart.Series[0].Points.DataBindXY(XData, XDifference);
            chart.Series[1].Points.DataBindXY(XData, YDifference);
            chart.Series[0].Name = "X_Diff";
            chart.Series[1].Name = "Y_Diff";
            chart.ChartAreas[0].AxisX.Minimum = XData[0] - Math.Round(XData[0], 2) > 0 ? Math.Round(XData[0], 2) : Math.Round(XData[0], 2) - 0.01;
            chart.ChartAreas[0].AxisX.Maximum = XData[XData.Length - 1] - Math.Round(XData[XData.Length - 1], 2) > 0 ? Math.Round(XData[XData.Length - 1], 2) + 0.01 : Math.Round(XData[XData.Length - 1], 2);
            chart.ChartAreas[0].AxisX.Interval = (chart.ChartAreas[0].AxisX.Maximum - chart.ChartAreas[0].AxisX.Minimum) / 10;
        }
    }
    public class FittingData : DataObject
    {
        public double[] AbsoluteDifference { get; set; } // Apkopotais signāls
        public double[] AbsoluteDifferenceAnalyticalFit { get; set; } // Analītiskā funkcija
        public override void PlotData(Chart chart)
        {
            chart.Series[0].Points.Clear();
            chart.Series[1].Points.Clear();
            chart.Series[0].Points.DataBindXY(XData, AbsoluteDifference);
            chart.ChartAreas[0].AxisX.Minimum = XData[0] - Math.Round(XData[0], 2) > 0 ? Math.Round(XData[0], 2) : Math.Round(XData[0], 2) - 0.01;
            chart.ChartAreas[0].AxisX.Maximum = XData[XData.Length - 1] - Math.Round(XData[XData.Length - 1], 2) > 0 ? Math.Round(XData[XData.Length - 1], 2) + 0.01 : Math.Round(XData[XData.Length - 1], 2);
            chart.ChartAreas[0].AxisX.Interval = (chart.ChartAreas[0].AxisX.Maximum - chart.ChartAreas[0].AxisX.Minimum) / 10;
            chart.Series[0].Name = "Diff";
            chart.Series[1].IsVisibleInLegend = false;
            chart.ChartAreas[0].AxisX.Crossing = 0;
        }

    }
    public class AnalyticalFit : DataObject
    {
        public double[] AbsoluteDifferenceAnalyticalFit { get; set; }
        public override void PlotData(Chart chart)
        {
            chart.Series[1].Points.Clear();
            chart.Series[1].IsVisibleInLegend = true;
            chart.Series[1].Name = "Fit";
            chart.Series[1].Points.DataBindXY(XData, AbsoluteDifferenceAnalyticalFit);
        }
    }
}
