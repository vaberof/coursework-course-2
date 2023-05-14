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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.FourthLevelCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.FourthLevelResponseFunctionChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.FourthLevelClearButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.FourthLevelResponseFunctionChart)).BeginInit();
            this.SuspendLayout();
            // 
            // FourthLevelCheckedListBox
            // 
            this.FourthLevelCheckedListBox.FormattingEnabled = true;
            this.FourthLevelCheckedListBox.Location = new System.Drawing.Point(12, 12);
            this.FourthLevelCheckedListBox.Name = "FourthLevelCheckedListBox";
            this.FourthLevelCheckedListBox.Size = new System.Drawing.Size(247, 667);
            this.FourthLevelCheckedListBox.TabIndex = 0;
            // 
            // FourthLevelResponseFunctionChart
            // 
            chartArea1.Name = "ChartArea1";
            this.FourthLevelResponseFunctionChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.FourthLevelResponseFunctionChart.Legends.Add(legend1);
            this.FourthLevelResponseFunctionChart.Location = new System.Drawing.Point(305, 12);
            this.FourthLevelResponseFunctionChart.Name = "FourthLevelResponseFunctionChart";
            this.FourthLevelResponseFunctionChart.Size = new System.Drawing.Size(1100, 668);
            this.FourthLevelResponseFunctionChart.TabIndex = 1;
            this.FourthLevelResponseFunctionChart.Text = "chart1";
            // 
            // FourthLevelClearButton
            // 
            this.FourthLevelClearButton.Location = new System.Drawing.Point(12, 696);
            this.FourthLevelClearButton.Name = "FourthLevelClearButton";
            this.FourthLevelClearButton.Size = new System.Drawing.Size(247, 61);
            this.FourthLevelClearButton.TabIndex = 2;
            this.FourthLevelClearButton.Text = "Очистить";
            this.FourthLevelClearButton.UseVisualStyleBackColor = true;
            // 
            // FourthLevelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1417, 769);
            this.Controls.Add(this.FourthLevelClearButton);
            this.Controls.Add(this.FourthLevelResponseFunctionChart);
            this.Controls.Add(this.FourthLevelCheckedListBox);
            this.Name = "FourthLevelForm";
            this.Text = "FourthLevelForm";
            this.Load += new System.EventHandler(this.FourthLevelForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FourthLevelResponseFunctionChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox FourthLevelCheckedListBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart FourthLevelResponseFunctionChart;
        private System.Windows.Forms.Button FourthLevelClearButton;
    }
}