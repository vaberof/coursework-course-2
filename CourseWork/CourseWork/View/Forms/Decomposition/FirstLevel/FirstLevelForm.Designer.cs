namespace CourseWork.View.Forms.Decomposition
{
    partial class FirstLevelForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.FirstLevelCoordinatesDataGridView = new System.Windows.Forms.DataGridView();
            this.FirstLevelCoordinatesGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FirstLevelEstimationDataGridView = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.PredictedUpperBoundMCheckBox = new System.Windows.Forms.CheckBox();
            this.PredictedMCheckBox = new System.Windows.Forms.CheckBox();
            this.PredictedLowerBoundMCheckBox = new System.Windows.Forms.CheckBox();
            this.MCheckBox = new System.Windows.Forms.CheckBox();
            this.UpperBoundMCheckBox = new System.Windows.Forms.CheckBox();
            this.LowerBoundMCheckBox = new System.Windows.Forms.CheckBox();
            this.FirstLevelMChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.PredictedUpperAlphaCheckBox = new System.Windows.Forms.CheckBox();
            this.PredictedAlphaCheckBox = new System.Windows.Forms.CheckBox();
            this.PredictedLowerBoundAlphaCheckBox = new System.Windows.Forms.CheckBox();
            this.AlphaCheckBox = new System.Windows.Forms.CheckBox();
            this.UpperBoundAlphaCheckBox = new System.Windows.Forms.CheckBox();
            this.LowerBoundAlphaCheckBox = new System.Windows.Forms.CheckBox();
            this.FirstLevelAlphaChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.FirstLevelCoordinatesDataGridView)).BeginInit();
            this.FirstLevelCoordinatesGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FirstLevelEstimationDataGridView)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FirstLevelMChart)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FirstLevelAlphaChart)).BeginInit();
            this.SuspendLayout();
            // 
            // FirstLevelCoordinatesDataGridView
            // 
            this.FirstLevelCoordinatesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FirstLevelCoordinatesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FirstLevelCoordinatesDataGridView.Location = new System.Drawing.Point(3, 18);
            this.FirstLevelCoordinatesDataGridView.Name = "FirstLevelCoordinatesDataGridView";
            this.FirstLevelCoordinatesDataGridView.RowHeadersWidth = 51;
            this.FirstLevelCoordinatesDataGridView.RowTemplate.Height = 24;
            this.FirstLevelCoordinatesDataGridView.Size = new System.Drawing.Size(479, 438);
            this.FirstLevelCoordinatesDataGridView.TabIndex = 0;
            this.FirstLevelCoordinatesDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.FirstLevelCoordinatesDataGridView_CellContentClick);
            // 
            // FirstLevelCoordinatesGroupBox
            // 
            this.FirstLevelCoordinatesGroupBox.Controls.Add(this.FirstLevelCoordinatesDataGridView);
            this.FirstLevelCoordinatesGroupBox.Location = new System.Drawing.Point(12, 12);
            this.FirstLevelCoordinatesGroupBox.Name = "FirstLevelCoordinatesGroupBox";
            this.FirstLevelCoordinatesGroupBox.Size = new System.Drawing.Size(485, 459);
            this.FirstLevelCoordinatesGroupBox.TabIndex = 1;
            this.FirstLevelCoordinatesGroupBox.TabStop = false;
            this.FirstLevelCoordinatesGroupBox.Text = "Фазовые координаты";
            this.FirstLevelCoordinatesGroupBox.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.FirstLevelEstimationDataGridView);
            this.groupBox1.Location = new System.Drawing.Point(12, 477);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(485, 280);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Оценка состояния объекта";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter_1);
            // 
            // FirstLevelEstimationDataGridView
            // 
            this.FirstLevelEstimationDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FirstLevelEstimationDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FirstLevelEstimationDataGridView.Location = new System.Drawing.Point(3, 18);
            this.FirstLevelEstimationDataGridView.Name = "FirstLevelEstimationDataGridView";
            this.FirstLevelEstimationDataGridView.RowHeadersWidth = 51;
            this.FirstLevelEstimationDataGridView.RowTemplate.Height = 24;
            this.FirstLevelEstimationDataGridView.Size = new System.Drawing.Size(479, 259);
            this.FirstLevelEstimationDataGridView.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.AccessibleName = "";
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(501, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(912, 745);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.FirstLevelMChart);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(904, 716);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "M(t)";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.PredictedUpperBoundMCheckBox);
            this.groupBox3.Controls.Add(this.PredictedMCheckBox);
            this.groupBox3.Controls.Add(this.PredictedLowerBoundMCheckBox);
            this.groupBox3.Controls.Add(this.MCheckBox);
            this.groupBox3.Controls.Add(this.UpperBoundMCheckBox);
            this.groupBox3.Controls.Add(this.LowerBoundMCheckBox);
            this.groupBox3.Location = new System.Drawing.Point(37, 483);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(421, 128);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Параметры графиков";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(260, 55);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 41);
            this.button2.TabIndex = 6;
            this.button2.Text = "Очистить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // PredictedUpperBoundMCheckBox
            // 
            this.PredictedUpperBoundMCheckBox.AutoSize = true;
            this.PredictedUpperBoundMCheckBox.Location = new System.Drawing.Point(96, 92);
            this.PredictedUpperBoundMCheckBox.Name = "PredictedUpperBoundMCheckBox";
            this.PredictedUpperBoundMCheckBox.Size = new System.Drawing.Size(117, 20);
            this.PredictedUpperBoundMCheckBox.TabIndex = 5;
            this.PredictedUpperBoundMCheckBox.Text = "Прогноз M(t)+";
            this.PredictedUpperBoundMCheckBox.UseVisualStyleBackColor = true;
            this.PredictedUpperBoundMCheckBox.CheckedChanged += new System.EventHandler(this.PredictedUpperBoundMCheckBox_CheckedChanged);
            // 
            // PredictedMCheckBox
            // 
            this.PredictedMCheckBox.AutoSize = true;
            this.PredictedMCheckBox.Location = new System.Drawing.Point(96, 66);
            this.PredictedMCheckBox.Name = "PredictedMCheckBox";
            this.PredictedMCheckBox.Size = new System.Drawing.Size(110, 20);
            this.PredictedMCheckBox.TabIndex = 4;
            this.PredictedMCheckBox.Text = "Прогноз M(t)";
            this.PredictedMCheckBox.UseVisualStyleBackColor = true;
            this.PredictedMCheckBox.CheckedChanged += new System.EventHandler(this.PredictedMCheckBox_CheckedChanged);
            // 
            // PredictedLowerBoundMCheckBox
            // 
            this.PredictedLowerBoundMCheckBox.AutoSize = true;
            this.PredictedLowerBoundMCheckBox.Location = new System.Drawing.Point(96, 40);
            this.PredictedLowerBoundMCheckBox.Name = "PredictedLowerBoundMCheckBox";
            this.PredictedLowerBoundMCheckBox.Size = new System.Drawing.Size(114, 20);
            this.PredictedLowerBoundMCheckBox.TabIndex = 3;
            this.PredictedLowerBoundMCheckBox.Text = "Прогноз M(t)-";
            this.PredictedLowerBoundMCheckBox.UseVisualStyleBackColor = true;
            this.PredictedLowerBoundMCheckBox.CheckedChanged += new System.EventHandler(this.PredictedLowerBoundMCheckBox_CheckedChanged);
            // 
            // MCheckBox
            // 
            this.MCheckBox.AutoSize = true;
            this.MCheckBox.Location = new System.Drawing.Point(26, 66);
            this.MCheckBox.Name = "MCheckBox";
            this.MCheckBox.Size = new System.Drawing.Size(51, 20);
            this.MCheckBox.TabIndex = 2;
            this.MCheckBox.Text = "M(t)";
            this.MCheckBox.UseVisualStyleBackColor = true;
            this.MCheckBox.CheckedChanged += new System.EventHandler(this.MCheckBox_CheckedChanged);
            // 
            // UpperBoundMCheckBox
            // 
            this.UpperBoundMCheckBox.AutoSize = true;
            this.UpperBoundMCheckBox.Location = new System.Drawing.Point(26, 92);
            this.UpperBoundMCheckBox.Name = "UpperBoundMCheckBox";
            this.UpperBoundMCheckBox.Size = new System.Drawing.Size(58, 20);
            this.UpperBoundMCheckBox.TabIndex = 1;
            this.UpperBoundMCheckBox.Text = "M(t)+";
            this.UpperBoundMCheckBox.UseVisualStyleBackColor = true;
            this.UpperBoundMCheckBox.CheckedChanged += new System.EventHandler(this.UpperBoundMCheckBox_CheckedChanged);
            // 
            // LowerBoundMCheckBox
            // 
            this.LowerBoundMCheckBox.AutoSize = true;
            this.LowerBoundMCheckBox.Location = new System.Drawing.Point(26, 40);
            this.LowerBoundMCheckBox.Name = "LowerBoundMCheckBox";
            this.LowerBoundMCheckBox.Size = new System.Drawing.Size(55, 20);
            this.LowerBoundMCheckBox.TabIndex = 0;
            this.LowerBoundMCheckBox.Text = "M(t)-";
            this.LowerBoundMCheckBox.UseVisualStyleBackColor = true;
            this.LowerBoundMCheckBox.CheckedChanged += new System.EventHandler(this.LowerBoundMCheckBox_CheckedChanged);
            // 
            // FirstLevelMChart
            // 
            this.FirstLevelMChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea6.Name = "ChartArea1";
            this.FirstLevelMChart.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.FirstLevelMChart.Legends.Add(legend6);
            this.FirstLevelMChart.Location = new System.Drawing.Point(6, 17);
            this.FirstLevelMChart.Name = "FirstLevelMChart";
            this.FirstLevelMChart.Size = new System.Drawing.Size(892, 446);
            this.FirstLevelMChart.TabIndex = 0;
            this.FirstLevelMChart.Text = "chart1";
            this.FirstLevelMChart.Click += new System.EventHandler(this.FirstLevelMChart_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.FirstLevelAlphaChart);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(904, 716);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "a(t)";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.PredictedUpperAlphaCheckBox);
            this.groupBox2.Controls.Add(this.PredictedAlphaCheckBox);
            this.groupBox2.Controls.Add(this.PredictedLowerBoundAlphaCheckBox);
            this.groupBox2.Controls.Add(this.AlphaCheckBox);
            this.groupBox2.Controls.Add(this.UpperBoundAlphaCheckBox);
            this.groupBox2.Controls.Add(this.LowerBoundAlphaCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(37, 483);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(421, 128);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Параметры графиков";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(260, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 41);
            this.button1.TabIndex = 6;
            this.button1.Text = "Очистить";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // PredictedUpperAlphaCheckBox
            // 
            this.PredictedUpperAlphaCheckBox.AutoSize = true;
            this.PredictedUpperAlphaCheckBox.Location = new System.Drawing.Point(96, 92);
            this.PredictedUpperAlphaCheckBox.Name = "PredictedUpperAlphaCheckBox";
            this.PredictedUpperAlphaCheckBox.Size = new System.Drawing.Size(114, 20);
            this.PredictedUpperAlphaCheckBox.TabIndex = 5;
            this.PredictedUpperAlphaCheckBox.Text = "Прогноз a(t)+";
            this.PredictedUpperAlphaCheckBox.UseVisualStyleBackColor = true;
            this.PredictedUpperAlphaCheckBox.CheckedChanged += new System.EventHandler(this.PredictedUpperAlphaCheckBox_CheckedChanged);
            // 
            // PredictedAlphaCheckBox
            // 
            this.PredictedAlphaCheckBox.AutoSize = true;
            this.PredictedAlphaCheckBox.Location = new System.Drawing.Point(96, 66);
            this.PredictedAlphaCheckBox.Name = "PredictedAlphaCheckBox";
            this.PredictedAlphaCheckBox.Size = new System.Drawing.Size(107, 20);
            this.PredictedAlphaCheckBox.TabIndex = 4;
            this.PredictedAlphaCheckBox.Text = "Прогноз a(t)";
            this.PredictedAlphaCheckBox.UseVisualStyleBackColor = true;
            this.PredictedAlphaCheckBox.CheckedChanged += new System.EventHandler(this.PredictedAlphaCheckBox_CheckedChanged);
            // 
            // PredictedLowerBoundAlphaCheckBox
            // 
            this.PredictedLowerBoundAlphaCheckBox.AutoSize = true;
            this.PredictedLowerBoundAlphaCheckBox.Location = new System.Drawing.Point(96, 40);
            this.PredictedLowerBoundAlphaCheckBox.Name = "PredictedLowerBoundAlphaCheckBox";
            this.PredictedLowerBoundAlphaCheckBox.Size = new System.Drawing.Size(111, 20);
            this.PredictedLowerBoundAlphaCheckBox.TabIndex = 3;
            this.PredictedLowerBoundAlphaCheckBox.Text = "Прогноз a(t)-";
            this.PredictedLowerBoundAlphaCheckBox.UseVisualStyleBackColor = true;
            this.PredictedLowerBoundAlphaCheckBox.CheckedChanged += new System.EventHandler(this.PredictedLowerBoundAlphaCheckBox_CheckedChanged);
            // 
            // AlphaCheckBox
            // 
            this.AlphaCheckBox.AutoSize = true;
            this.AlphaCheckBox.Location = new System.Drawing.Point(26, 66);
            this.AlphaCheckBox.Name = "AlphaCheckBox";
            this.AlphaCheckBox.Size = new System.Drawing.Size(48, 20);
            this.AlphaCheckBox.TabIndex = 2;
            this.AlphaCheckBox.Text = "a(t)";
            this.AlphaCheckBox.UseVisualStyleBackColor = true;
            this.AlphaCheckBox.CheckedChanged += new System.EventHandler(this.AlphaCheckBox_CheckedChanged);
            // 
            // UpperBoundAlphaCheckBox
            // 
            this.UpperBoundAlphaCheckBox.AutoSize = true;
            this.UpperBoundAlphaCheckBox.Location = new System.Drawing.Point(26, 92);
            this.UpperBoundAlphaCheckBox.Name = "UpperBoundAlphaCheckBox";
            this.UpperBoundAlphaCheckBox.Size = new System.Drawing.Size(55, 20);
            this.UpperBoundAlphaCheckBox.TabIndex = 1;
            this.UpperBoundAlphaCheckBox.Text = "a(t)+";
            this.UpperBoundAlphaCheckBox.UseVisualStyleBackColor = true;
            this.UpperBoundAlphaCheckBox.CheckedChanged += new System.EventHandler(this.UpperBoundAlphaCheckBox_CheckedChanged);
            // 
            // LowerBoundAlphaCheckBox
            // 
            this.LowerBoundAlphaCheckBox.AutoSize = true;
            this.LowerBoundAlphaCheckBox.Location = new System.Drawing.Point(26, 40);
            this.LowerBoundAlphaCheckBox.Name = "LowerBoundAlphaCheckBox";
            this.LowerBoundAlphaCheckBox.Size = new System.Drawing.Size(52, 20);
            this.LowerBoundAlphaCheckBox.TabIndex = 0;
            this.LowerBoundAlphaCheckBox.Text = "a(t)-";
            this.LowerBoundAlphaCheckBox.UseVisualStyleBackColor = true;
            this.LowerBoundAlphaCheckBox.CheckedChanged += new System.EventHandler(this.LowerBoundAlphaCheckBox_CheckedChanged);
            // 
            // FirstLevelAlphaChart
            // 
            this.FirstLevelAlphaChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea5.Name = "ChartArea1";
            this.FirstLevelAlphaChart.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.FirstLevelAlphaChart.Legends.Add(legend5);
            this.FirstLevelAlphaChart.Location = new System.Drawing.Point(6, 16);
            this.FirstLevelAlphaChart.Name = "FirstLevelAlphaChart";
            this.FirstLevelAlphaChart.Size = new System.Drawing.Size(892, 446);
            this.FirstLevelAlphaChart.TabIndex = 3;
            this.FirstLevelAlphaChart.Text = "chart2";
            // 
            // FirstLevelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1417, 769);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FirstLevelCoordinatesGroupBox);
            this.Name = "FirstLevelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Первый уровень";
            this.Load += new System.EventHandler(this.FirstLevelForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FirstLevelCoordinatesDataGridView)).EndInit();
            this.FirstLevelCoordinatesGroupBox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FirstLevelEstimationDataGridView)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FirstLevelMChart)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FirstLevelAlphaChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView FirstLevelCoordinatesDataGridView;
        private System.Windows.Forms.GroupBox FirstLevelCoordinatesGroupBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView FirstLevelEstimationDataGridView;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataVisualization.Charting.Chart FirstLevelMChart;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox PredictedUpperBoundMCheckBox;
        private System.Windows.Forms.CheckBox PredictedMCheckBox;
        private System.Windows.Forms.CheckBox PredictedLowerBoundMCheckBox;
        private System.Windows.Forms.CheckBox MCheckBox;
        private System.Windows.Forms.CheckBox UpperBoundMCheckBox;
        private System.Windows.Forms.CheckBox LowerBoundMCheckBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox PredictedUpperAlphaCheckBox;
        private System.Windows.Forms.CheckBox PredictedAlphaCheckBox;
        private System.Windows.Forms.CheckBox PredictedLowerBoundAlphaCheckBox;
        private System.Windows.Forms.CheckBox AlphaCheckBox;
        private System.Windows.Forms.CheckBox UpperBoundAlphaCheckBox;
        private System.Windows.Forms.CheckBox LowerBoundAlphaCheckBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart FirstLevelAlphaChart;
    }
}