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
        ResponseFunction responsfunction = new ResponseFunction();
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
            string filePath = null;
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filePath = file.FileName;
            }
            else { MessageBox.Show("No file selected"); goto end; }

            // Ieliekt datus rawdata objektā
            rawdata.DataRaw = File.ReadAllLines(filePath);
            rawdata.XData = new double[rawdata.DataRaw.Length - 1];
            rawdata.XDifference = new double[rawdata.DataRaw.Length - 1];
            rawdata.YDifference = new double[rawdata.DataRaw.Length - 1];
            (rawdata.XData, rawdata.XDifference, rawdata.YDifference) = functions.ReadDataFromFile(rawdata.DataRaw);
            fullDataObjects.Add(rawdata);

            // Pārbauda vai dati ir derīgi
            if(rawdata.XData == null) 
            {
                MessageBox.Show("Incomplite data file");
                goto end;
            }

            // Uzzīmē datus
            rawdata.PlotData(chart1);
            
            end: 
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
            if (!functions.RunCheckList(CheckNumber, textRefReg, this.Controls, "number")) { goto end; }
            if (!functions.RunCheckList(CheckNumber, textOffset, this.Controls, "number")) { goto end; }
            double region = Convert.ToDouble(textRefReg.Text);
            double offset = Convert.ToDouble(textOffset.Text);


            // Normē datus un ieraksta fittingdata objektā
            if (rawdata.DataRaw == null) { MessageBox.Show("First input data file"); goto end; }
            fittingdata.AbsoluteDifference = new double[rawdata.DataRaw.Length - 1];
            fittingdata.XData = new double[rawdata.DataRaw.Length - 1];
            try { (fittingdata.AbsoluteDifference, fittingdata.XData) = functions.NormalizeData(rawdata, region, offset); }
            catch (Exception outofbonds) { MessageBox.Show(outofbonds.Message); goto end; }
            fullDataObjects.Add(fittingdata);
           
            // Uzzīmē datus
            fittingdata.PlotData(chart1);  
            
            end:
            //Footer
            functions.ButtonOnOff(true, this.Controls);
        }

        private void buttonPlot_Click(object sender, EventArgs e)
        {
            // Header
            var functions = new Functions();
            functions.ButtonOnOff(false, this.Controls);

            // Ievada datus analyticalfit objektā            
            try{  int t = fittingdata.XData.Length; }
            catch(Exception ex) { MessageBox.Show(ex.ToString()); goto end; }
            analyticalfit.XData = new double[fittingdata.XData.Length];
            analyticalfit.AbsoluteDifferenceAnalyticalFit = new double[fittingdata.XData.Length];

            // Pārbauda vai visi parametru lauciņi ir aizpildīti
            List<double> arrayP;
            try { arrayP = new List<double>() { Convert.ToDouble(textBoxA1.Text), Convert.ToDouble(textBoxTr1.Text), Convert.ToDouble(textBoxTf1.Text), Convert.ToDouble(textBoxA2.Text), Convert.ToDouble(textBoxTr2.Text), Convert.ToDouble(textBoxTf2.Text), Convert.ToDouble(textBoxA3.Text), Convert.ToDouble(textBoxTr3.Text), Convert.ToDouble(textBoxTf3.Text) }; }
            catch (FormatException) { MessageBox.Show("Some parameter fields are empty"); goto end; }
            catch (Exception ex) { MessageBox.Show(ex.Message); goto end; }
                       
            for (int i=0; i< fittingdata.XData.Length; i++)
            {
               analyticalfit.XData[i] = fittingdata.XData[i];
               try { analyticalfit.AbsoluteDifferenceAnalyticalFit[i] = responsfunction.Value(arrayP, analyticalfit.XData[i]); }
               catch (Exception outofbonds) { MessageBox.Show(outofbonds.Message); goto end; }
            }
            fullDataObjects.Add(analyticalfit);
           
            // Uzzīmē datus
            analyticalfit.PlotData(chart1);

            end:
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
            if (!functions.RunCheckList(CheckNumber, textOffsetForAll, this.Controls, "number")) { goto end; }

            // Normē visus datu objektus, kuros ir dati
            foreach (DataObject c in fullDataObjects)
            {
                c.Offset(Convert.ToDouble(textOffsetForAll.Text));
                c.PlotData(chart1);
            }

            end:
            // Footer
            functions.ButtonOnOff(true, this.Controls);
        }
        #endregion

        private void buttonFit_Click(object sender, EventArgs e)
        {
            // Header
            var functions = new Functions();
            functions.ButtonOnOff(false, this.Controls);

            // Pārbauda vai visi parametru lauciņi ir aizpildīti
            List<double> arrayP;
            List<double> fitP;
            try { arrayP = new List<double>() { Convert.ToDouble(textBoxA1.Text), Convert.ToDouble(textBoxTr1.Text), Convert.ToDouble(textBoxTf1.Text), Convert.ToDouble(textBoxA2.Text), Convert.ToDouble(textBoxTr2.Text), Convert.ToDouble(textBoxTf2.Text), Convert.ToDouble(textBoxA3.Text), Convert.ToDouble(textBoxTr3.Text), Convert.ToDouble(textBoxTf3.Text) }; }
            catch (FormatException) { MessageBox.Show("Some parameter fields are empty"); goto end; }
            catch (Exception ex) { MessageBox.Show(ex.Message); goto end; }

            // Fito datus
            try { fitP = functions.AnalyticalFunctionCurveFit(responsfunction, arrayP, fittingdata); }
            catch (Exception outofbonds) { MessageBox.Show(outofbonds.Message); goto end; }

            // Uzzīmē datus un ieraksta rūtiņās vērtība
            for (int i = 0; i < fittingdata.XData.Length; i++)
            {
                analyticalfit.XData[i] = fittingdata.XData[i];
                analyticalfit.AbsoluteDifferenceAnalyticalFit[i] = responsfunction.Value(fitP, analyticalfit.XData[i]);
            }
            textBoxA1.Text = fitP[0].ToString();
            textBoxTf1.Text = fitP[1].ToString();
            textBoxTr1.Text = fitP[2].ToString();
            textBoxA1.Text = fitP[3].ToString();
            textBoxTf1.Text = fitP[4].ToString();

            // Uzzīmē datus
            analyticalfit.PlotData(chart1);

            end:
            // Footer
            functions.ButtonOnOff(true, this.Controls);

        }
    }
}
