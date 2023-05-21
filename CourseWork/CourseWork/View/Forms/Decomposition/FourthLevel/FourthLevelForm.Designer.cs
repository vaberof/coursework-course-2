namespace CourseWork.View.Forms.Decomposition.FourthLevel
{
    partial class FourthLevelForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.MarksCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.ResponseFunctionChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ClearCheckedListBoxAndChartButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ResponseFunctionChart)).BeginInit();
            this.SuspendLayout();
            // 
            // MarksCheckedListBox
            // 
            this.MarksCheckedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MarksCheckedListBox.CheckOnClick = true;
            this.MarksCheckedListBox.FormattingEnabled = true;
            this.MarksCheckedListBox.Location = new System.Drawing.Point(12, 12);
            this.MarksCheckedListBox.Name = "MarksCheckedListBox";
            this.MarksCheckedListBox.Size = new System.Drawing.Size(306, 650);
            this.MarksCheckedListBox.TabIndex = 0;
            this.MarksCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.MarksCheckedListBox_ItemCheck);
            // 
            // ResponseFunctionChart
            // 
            this.ResponseFunctionChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ChartArea1";
            this.ResponseFunctionChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.ResponseFunctionChart.Legends.Add(legend2);
            this.ResponseFunctionChart.Location = new System.Drawing.Point(340, 12);
            this.ResponseFunctionChart.Name = "ResponseFunctionChart";
            this.ResponseFunctionChart.Size = new System.Drawing.Size(1065, 745);
            this.ResponseFunctionChart.TabIndex = 1;
            this.ResponseFunctionChart.Text = "chart1";
            // 
            // ClearCheckedListBoxAndChartButton
            // 
            this.ClearCheckedListBoxAndChartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ClearCheckedListBoxAndChartButton.Location = new System.Drawing.Point(181, 696);
            this.ClearCheckedListBoxAndChartButton.Name = "ClearCheckedListBoxAndChartButton";
            this.ClearCheckedListBoxAndChartButton.Size = new System.Drawing.Size(135, 60);
            this.ClearCheckedListBoxAndChartButton.TabIndex = 2;
            this.ClearCheckedListBoxAndChartButton.Text = "Очистить";
            this.ClearCheckedListBoxAndChartButton.UseVisualStyleBackColor = true;
            this.ClearCheckedListBoxAndChartButton.Click += new System.EventHandler(this.ClearCheckedListBoxAndChartButton_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(12, 696);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 60);
            this.button1.TabIndex = 3;
            this.button1.Text = "Отобразить все";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.DisplayAllCheckedListBoxAndChartButton_Click);
            // 
            // FourthLevelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1417, 769);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ClearCheckedListBoxAndChartButton);
            this.Controls.Add(this.ResponseFunctionChart);
            this.Controls.Add(this.MarksCheckedListBox);
            this.Name = "FourthLevelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Четвертый уровень";
            this.Load += new System.EventHandler(this.FourthLevelForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ResponseFunctionChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox MarksCheckedListBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart ResponseFunctionChart;
        private System.Windows.Forms.Button ClearCheckedListBoxAndChartButton;
        private System.Windows.Forms.Button button1;
    }
}