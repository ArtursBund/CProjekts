
namespace CProjekts
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textFileName = new System.Windows.Forms.TextBox();
            this.buttonReadData = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buttonNormData = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textRefReg = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textOffset = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxA1 = new System.Windows.Forms.TextBox();
            this.textBoxTr1 = new System.Windows.Forms.TextBox();
            this.textBoxTf1 = new System.Windows.Forms.TextBox();
            this.textBoxA2 = new System.Windows.Forms.TextBox();
            this.textBoxTr2 = new System.Windows.Forms.TextBox();
            this.textBoxTf2 = new System.Windows.Forms.TextBox();
            this.textBoxA3 = new System.Windows.Forms.TextBox();
            this.textBoxTr3 = new System.Windows.Forms.TextBox();
            this.textBoxTf3 = new System.Windows.Forms.TextBox();
            this.buttonPlot = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label1.Location = new System.Drawing.Point(64, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Datu ielasīšana";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Faila nosaukums:";
            // 
            // textFileName
            // 
            this.textFileName.Location = new System.Drawing.Point(120, 45);
            this.textFileName.Name = "textFileName";
            this.textFileName.Size = new System.Drawing.Size(100, 20);
            this.textFileName.TabIndex = 2;
            // 
            // buttonReadData
            // 
            this.buttonReadData.Location = new System.Drawing.Point(26, 82);
            this.buttonReadData.Name = "buttonReadData";
            this.buttonReadData.Size = new System.Drawing.Size(75, 23);
            this.buttonReadData.TabIndex = 3;
            this.buttonReadData.Text = "Ielasīt";
            this.buttonReadData.UseVisualStyleBackColor = true;
            this.buttonReadData.Click += new System.EventHandler(this.buttonReadData_Click);
            // 
            // chart1
            // 
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(270, 19);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(877, 300);
            this.chart1.TabIndex = 4;
            this.chart1.Text = "chart1";
            // 
            // buttonNormData
            // 
            this.buttonNormData.Location = new System.Drawing.Point(26, 203);
            this.buttonNormData.Name = "buttonNormData";
            this.buttonNormData.Size = new System.Drawing.Size(75, 23);
            this.buttonNormData.TabIndex = 5;
            this.buttonNormData.Text = "Normēt datus";
            this.buttonNormData.UseVisualStyleBackColor = true;
            this.buttonNormData.Click += new System.EventHandler(this.buttonNormData_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "References reģions:";
            // 
            // textRefReg
            // 
            this.textRefReg.Location = new System.Drawing.Point(120, 151);
            this.textRefReg.Name = "textRefReg";
            this.textRefReg.Size = new System.Drawing.Size(100, 20);
            this.textRefReg.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(76, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Offset:";
            // 
            // textOffset
            // 
            this.textOffset.Location = new System.Drawing.Point(120, 174);
            this.textOffset.Name = "textOffset";
            this.textOffset.Size = new System.Drawing.Size(100, 20);
            this.textOffset.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label5.Location = new System.Drawing.Point(55, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 18);
            this.label5.TabIndex = 10;
            this.label5.Text = "Datu normēšana";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label6.Location = new System.Drawing.Point(55, 242);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 18);
            this.label6.TabIndex = 11;
            this.label6.Text = "Analītiskā funkcija";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 300);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "A=";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 330);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "t_r=";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 360);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(25, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "t_f=";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(72, 273);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "F1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(145, 273);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(19, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "F2";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(218, 273);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(19, 13);
            this.label12.TabIndex = 17;
            this.label12.Text = "F3";
            // 
            // textBoxA1
            // 
            this.textBoxA1.Location = new System.Drawing.Point(61, 296);
            this.textBoxA1.Name = "textBoxA1";
            this.textBoxA1.Size = new System.Drawing.Size(41, 20);
            this.textBoxA1.TabIndex = 18;
            // 
            // textBoxTr1
            // 
            this.textBoxTr1.Location = new System.Drawing.Point(61, 326);
            this.textBoxTr1.Name = "textBoxTr1";
            this.textBoxTr1.Size = new System.Drawing.Size(41, 20);
            this.textBoxTr1.TabIndex = 19;
            // 
            // textBoxTf1
            // 
            this.textBoxTf1.Location = new System.Drawing.Point(61, 356);
            this.textBoxTf1.Name = "textBoxTf1";
            this.textBoxTf1.Size = new System.Drawing.Size(41, 20);
            this.textBoxTf1.TabIndex = 20;
            // 
            // textBoxA2
            // 
            this.textBoxA2.Location = new System.Drawing.Point(134, 296);
            this.textBoxA2.Name = "textBoxA2";
            this.textBoxA2.Size = new System.Drawing.Size(41, 20);
            this.textBoxA2.TabIndex = 21;
            // 
            // textBoxTr2
            // 
            this.textBoxTr2.Location = new System.Drawing.Point(134, 326);
            this.textBoxTr2.Name = "textBoxTr2";
            this.textBoxTr2.Size = new System.Drawing.Size(41, 20);
            this.textBoxTr2.TabIndex = 22;
            // 
            // textBoxTf2
            // 
            this.textBoxTf2.Location = new System.Drawing.Point(134, 356);
            this.textBoxTf2.Name = "textBoxTf2";
            this.textBoxTf2.Size = new System.Drawing.Size(41, 20);
            this.textBoxTf2.TabIndex = 23;
            // 
            // textBoxA3
            // 
            this.textBoxA3.Location = new System.Drawing.Point(207, 296);
            this.textBoxA3.Name = "textBoxA3";
            this.textBoxA3.Size = new System.Drawing.Size(41, 20);
            this.textBoxA3.TabIndex = 24;
            // 
            // textBoxTr3
            // 
            this.textBoxTr3.Location = new System.Drawing.Point(207, 326);
            this.textBoxTr3.Name = "textBoxTr3";
            this.textBoxTr3.Size = new System.Drawing.Size(41, 20);
            this.textBoxTr3.TabIndex = 25;
            // 
            // textBoxTf3
            // 
            this.textBoxTf3.Location = new System.Drawing.Point(207, 356);
            this.textBoxTf3.Name = "textBoxTf3";
            this.textBoxTf3.Size = new System.Drawing.Size(41, 20);
            this.textBoxTf3.TabIndex = 26;
            // 
            // buttonPlot
            // 
            this.buttonPlot.Location = new System.Drawing.Point(26, 391);
            this.buttonPlot.Name = "buttonPlot";
            this.buttonPlot.Size = new System.Drawing.Size(75, 23);
            this.buttonPlot.TabIndex = 27;
            this.buttonPlot.Text = "Plot";
            this.buttonPlot.UseVisualStyleBackColor = true;
            this.buttonPlot.Click += new System.EventHandler(this.buttonPlot_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 450);
            this.Controls.Add(this.buttonPlot);
            this.Controls.Add(this.textBoxTf3);
            this.Controls.Add(this.textBoxTr3);
            this.Controls.Add(this.textBoxA3);
            this.Controls.Add(this.textBoxTf2);
            this.Controls.Add(this.textBoxTr2);
            this.Controls.Add(this.textBoxA2);
            this.Controls.Add(this.textBoxTf1);
            this.Controls.Add(this.textBoxTr1);
            this.Controls.Add(this.textBoxA1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textOffset);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textRefReg);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonNormData);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.buttonReadData);
            this.Controls.Add(this.textFileName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textFileName;
        private System.Windows.Forms.Button buttonReadData;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button buttonNormData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textRefReg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textOffset;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxA1;
        private System.Windows.Forms.TextBox textBoxTr1;
        private System.Windows.Forms.TextBox textBoxTf1;
        private System.Windows.Forms.TextBox textBoxA2;
        private System.Windows.Forms.TextBox textBoxTr2;
        private System.Windows.Forms.TextBox textBoxTf2;
        private System.Windows.Forms.TextBox textBoxA3;
        private System.Windows.Forms.TextBox textBoxTr3;
        private System.Windows.Forms.TextBox textBoxTf3;
        private System.Windows.Forms.Button buttonPlot;
    }
}

