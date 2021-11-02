using System;
using System.Collections.Generic;
using System.IO;
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
        RawData rawdata = new RawData();
        FittingData fittingdata = new FittingData();
        AnalyticalFit analyticalfit = new AnalyticalFit();
        List<DataObject> fullDataObjects = new List<DataObject>();
        ICheck checkFile = new FileCheck();
        ICheck checkConvertionToDouble = new ConvertionToDoubleCheck();
        ICheck checkLargerThanZero = new IsLargerThanZeroCheck();        

        #endregion
        #region Button functions
        private void buttonReadData_Click(object sender, EventArgs e)
        {
            // Header
            List<ICheck> CheckField = new List<ICheck>();
            CheckField.Add(checkFile);
            var functions = new Functions();
            functions.ButtonOnOff(false, this.Controls);

            // Ielasa datus
            string FileName = textFileName.Text;
            string location = Application.StartupPath;
            string FilePath = string.Format(location + @"\{0}.txt", FileName);
            
            // Pārbauda ielasīšanas lauciņu
            if (!functions.RunCheckList(CheckField, textFileName, this.Controls, "File")) { return; }

            // Ieliekt datus rawdata objektā
            rawdata.DataRaw = File.ReadAllLines(FilePath);
            rawdata.XData = new double[rawdata.DataRaw.Length - 1];
            rawdata.XDifference = new double[rawdata.DataRaw.Length - 1];
            rawdata.YDifference = new double[rawdata.DataRaw.Length - 1];
            (rawdata.XData, rawdata.XDifference, rawdata.YDifference) = functions.ReadDataFromFile(rawdata.DataRaw);
            fullDataObjects.Add(rawdata);

            // Pārbauda vai dati ir derīgi
            if(rawdata.XData == null) 
            {
                MessageBox.Show("Incomplite data file");
                functions.ButtonOnOff(true, this.Controls);
                return;
            }

            // Uzzīmē datus
            rawdata.PlotData(chart1);
            
            //Footer
            functions.ButtonOnOff(true, this.Controls);
        }
        private void buttonNormData_Click(object sender, EventArgs e)
        {
            //Header
            List<ICheck> CheckNumber = new List<ICheck>();
            CheckNumber.Add(checkConvertionToDouble);
            CheckNumber.Add(checkLargerThanZero);           
            var functions = new Functions();
            functions.ButtonOnOff(false, this.Controls);
            
            // Pārbauda vai lauciņos ierakstītas derīgas vērtības un ielasa šīs vērtības
            if (!functions.RunCheckList(CheckNumber, textRefReg, this.Controls, "number")) { return; }
            if (!functions.RunCheckList(CheckNumber, textOffset, this.Controls, "number")) { return; }              
            double region = Convert.ToDouble(textRefReg.Text);
            double offset = Convert.ToDouble(textOffset.Text);

            // Normē datus un ieraksta fittingdata objektā
            List<DataObject> dataObjects = new List<DataObject>();
            dataObjects.Add(rawdata);
            dataObjects.Add(fittingdata);
            (fittingdata.AbsoluteDifference, fittingdata.XData) = functions.NormalizeData(rawdata, region, offset);
            fullDataObjects.Add(fittingdata);
           
            // Uzzīmē datus
            fittingdata.PlotData(chart1);  
            
            //Footer
            functions.ButtonOnOff(true, this.Controls);
        }

        private void buttonPlot_Click(object sender, EventArgs e)
        {
            // Header
            var functions = new Functions();
            functions.ButtonOnOff(false, this.Controls);

            // Ievada datus analyticalfit objektā
            analyticalfit.AbsoluteDifferenceAnalyticalFit = new double[fittingdata.XData.Length];
            analyticalfit.XData = new double[fittingdata.XData.Length];            
            double[] arrayP = { Convert.ToDouble(textBoxA1.Text), Convert.ToDouble(textBoxTf1.Text), Convert.ToDouble(textBoxTr1.Text), Convert.ToDouble(textBoxA2.Text), Convert.ToDouble(textBoxTf2.Text), Convert.ToDouble(textBoxTr2.Text), Convert.ToDouble(textBoxA3.Text), Convert.ToDouble(textBoxTf3.Text), Convert.ToDouble(textBoxTr3.Text) };
            for (int i=0; i< fittingdata.XData.Length - 1; i++)
            {
                analyticalfit.XData[i] = fittingdata.XData[i];
                analyticalfit.AbsoluteDifferenceAnalyticalFit[i] = functions.AnalyticalFunction(analyticalfit.XData[i], arrayP);
            }
            fullDataObjects.Add(analyticalfit);
           
            // Uzzīmē datus
            analyticalfit.PlotData(chart1);

            // Footer
            functions.ButtonOnOff(true, this.Controls);
        }
        
        private void buttonOffsetAllData_Click(object sender, EventArgs e)
        {
            // Header
            var functions = new Functions();
            functions.ButtonOnOff(false, this.Controls);
            List<ICheck> CheckNumber = new List<ICheck>();
            CheckNumber.Add(checkConvertionToDouble);

            // Pārbauda vai lauciņā ierakstītas derīgas vērtības un ielasa šīs vērtības
            if (!functions.RunCheckList(CheckNumber, textOffsetForAll, this.Controls, "number")) { return; }

            // Normē visus datu objektus, kuros ir dati
            foreach (DataObject c in fullDataObjects)
            {
                c.Offset(Convert.ToDouble(textOffsetForAll.Text));
                c.PlotData(chart1);
            }

            // Footer
            functions.ButtonOnOff(true, this.Controls);
        }
        #endregion
    }
}
