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
        DataStorage DS = new DataStorage();               
        ICheck checkFile = new FileCheck();
        ICheck checkConvertionToDouble = new ConvertionToDoubleCheck();
        ICheck checkLargerThanZero = new IsLargerThanZeroCheck();        

        #endregion
        #region Button functions
        private void buttonReadData_Click(object sender, EventArgs e)
        {
            List<ICheck> CheckField = new List<ICheck>();
            CheckField.Add(checkFile);
            var functions = new Functions();
            functions.ButtonOnOff(false, this.Controls);
            string FileName = textFileName.Text;
            string location = Application.StartupPath;
            string FilePath = string.Format(location + @"\{0}.txt", FileName);
            
            if (!functions.RunCheckList(CheckField, textFileName, this.Controls, "File")) { return; }           

            DS.Data_Raw = File.ReadAllLines(FilePath);
            DS.X_Data_G = new double[DS.Data_Raw.Length - 1];
            DS.X_Diff_G = new double[DS.Data_Raw.Length - 1];
            DS.Y_Diff_G = new double[DS.Data_Raw.Length - 1];
            (DS.X_Data_G,DS.X_Diff_G, DS.Y_Diff_G)=functions.ReadDataFromFile(DS.Data_Raw);
            
            if(DS.X_Data_G==null) 
            {
                MessageBox.Show("Incomplite data file");
                functions.ButtonOnOff(true, this.Controls);
                return;
            }

            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[0].Points.DataBindXY(DS.X_Data_G, DS.X_Diff_G);
            chart1.Series[1].Points.DataBindXY(DS.X_Data_G, DS.Y_Diff_G);
            chart1.Series[0].Name = "X_Diff";
            chart1.Series[1].Name = "Y_Diff";
            chart1.ChartAreas[0].AxisX.Minimum = DS.X_Data_G[0]-Math.Round(DS.X_Data_G[0], 2)>0 ? Math.Round(DS.X_Data_G[0], 2) : Math.Round(DS.X_Data_G[0], 2)-0.01;
            chart1.ChartAreas[0].AxisX.Maximum = DS.X_Data_G[DS.X_Data_G.Length - 1] - Math.Round(DS.X_Data_G[DS.X_Data_G.Length - 1], 2) > 0 ? Math.Round(DS.X_Data_G[DS.X_Data_G.Length - 1], 2)+0.01 : Math.Round(DS.X_Data_G[DS.X_Data_G.Length - 1], 2);
            chart1.ChartAreas[0].AxisX.Interval = (chart1.ChartAreas[0].AxisX.Maximum - chart1.ChartAreas[0].AxisX.Minimum) / 10;
            functions.ButtonOnOff(true, this.Controls);
        }
        private void buttonNormData_Click(object sender, EventArgs e)
        {
            List<ICheck> CheckNumber = new List<ICheck>();
            CheckNumber.Add(checkConvertionToDouble);
            CheckNumber.Add(checkLargerThanZero);            

            var functions = new Functions();
            functions.ButtonOnOff(false, this.Controls);            
            if (!functions.RunCheckList(CheckNumber, textRefReg, this.Controls, "number")) { return; }
            if (!functions.RunCheckList(CheckNumber, textOffset, this.Controls, "number")) { return; }            
            
            double region = Convert.ToDouble(textRefReg.Text);
            double offset = Convert.ToDouble(textOffset.Text);
            
            DS.Abs_Diff = new double[DS.X_Data_G.Length];
            (DS.Abs_Diff, DS.X_Data_G) = functions.NormalizeData(DS.X_Data_G, region, DS.X_Diff_G, DS.Y_Diff_G, offset);
            
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[0].Points.DataBindXY(DS.X_Data_G, DS.Abs_Diff);
            chart1.ChartAreas[0].AxisX.Minimum = DS.X_Data_G[0] - Math.Round(DS.X_Data_G[0], 2) > 0 ? Math.Round(DS.X_Data_G[0], 2) : Math.Round(DS.X_Data_G[0], 2) - 0.01;
            chart1.ChartAreas[0].AxisX.Maximum = DS.X_Data_G[DS.X_Data_G.Length - 1] - Math.Round(DS.X_Data_G[DS.X_Data_G.Length - 1], 2) > 0 ? Math.Round(DS.X_Data_G[DS.X_Data_G.Length - 1], 2) + 0.01 : Math.Round(DS.X_Data_G[DS.X_Data_G.Length - 1], 2);
            chart1.ChartAreas[0].AxisX.Interval = (chart1.ChartAreas[0].AxisX.Maximum - chart1.ChartAreas[0].AxisX.Minimum) / 10;
            chart1.Series[0].Name = "Diff";
            chart1.Series[1].IsVisibleInLegend = false;
            chart1.ChartAreas[0].AxisX.Crossing = 0; // Varbūt uzlikt drošību
            functions.ButtonOnOff(true, this.Controls);
        }

        private void buttonPlot_Click(object sender, EventArgs e)
        {
            var functions = new Functions();
            DS.Abs_Diff_Fit = new double[DS.Data_Raw.Length - 1];
            double[] arrayP = { Convert.ToDouble(textBoxA1.Text), Convert.ToDouble(textBoxTf1.Text), Convert.ToDouble(textBoxTr1.Text), Convert.ToDouble(textBoxA2.Text), Convert.ToDouble(textBoxTf2.Text), Convert.ToDouble(textBoxTr2.Text), Convert.ToDouble(textBoxA3.Text), Convert.ToDouble(textBoxTf3.Text), Convert.ToDouble(textBoxTr3.Text) };
            for (int i=0; i< DS.Data_Raw.Length-1;i++)
            {
                DS.Abs_Diff_Fit[i] = functions.AnalyticalFunction(DS.X_Data_G[i], arrayP);
            }
            chart1.Series[1].Points.Clear();
            chart1.Series[1].IsVisibleInLegend = true;
            chart1.Series[1].Name = "Fit";
            chart1.Series[1].Points.DataBindXY(DS.X_Data_G, DS.Abs_Diff_Fit);
            functions.ButtonOnOff(true, this.Controls);
        }
        #endregion
    }    
}
