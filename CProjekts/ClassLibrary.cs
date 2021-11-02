using System;
using System.Windows.Forms.DataVisualization.Charting;

namespace CProjekts
{
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
