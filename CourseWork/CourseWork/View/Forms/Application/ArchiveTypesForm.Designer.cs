namespace CourseWork.View.Forms.Application
{
    partial class ArchiveTypesForm
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
            this.RarRadioButton = new System.Windows.Forms.RadioButton();
            this.ZipRadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RarRadioButton
            // 
            this.RarRadioButton.AutoSize = true;
            this.RarRadioButton.Location = new System.Drawing.Point(43, 60);
            this.RarRadioButton.Name = "RarRadioButton";
            this.RarRadioButton.Size = new System.Drawing.Size(44, 20);
            this.RarRadioButton.TabIndex = 0;
            this.RarRadioButton.TabStop = true;
            this.RarRadioButton.Text = "rar";
            this.RarRadioButton.UseVisualStyleBackColor = true;
            // 
            // ZipRadioButton
            // 
            this.ZipRadioButton.AutoSize = true;
            this.ZipRadioButton.Location = new System.Drawing.Point(43, 95);
            this.ZipRadioButton.Name = "ZipRadioButton";
            this.ZipRadioButton.Size = new System.Drawing.Size(45, 20);
            this.ZipRadioButton.TabIndex = 1;
            this.ZipRadioButton.TabStop = true;
            this.ZipRadioButton.Text = "zip";
            this.ZipRadioButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(38, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Выберите тип архива";
            // 
            // SaveButton
            // 
            this.SaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveButton.Location = new System.Drawing.Point(82, 137);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(106, 28);
            this.SaveButton.TabIndex = 3;
            this.SaveButton.Text = "Сохранить";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ArchiveTypesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 177);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ZipRadioButton);
            this.Controls.Add(this.RarRadioButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "ArchiveTypesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ArchiveTypesForm";
            this.Load += new System.EventHandler(this.ArchiveTypesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton RarRadioButton;
        private System.Windows.Forms.RadioButton ZipRadioButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SaveButton;
    }
}