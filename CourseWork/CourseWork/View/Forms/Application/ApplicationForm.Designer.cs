namespace CourseWork
{
    partial class ApplicationForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationForm));
            this.TopMenuToolStrip = new System.Windows.Forms.ToolStrip();
            this.ProjectToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.OpenProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ObjectToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.ObjectShowDescriptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DecompositionToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.FirstLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SecondLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FourthLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewTable = new System.Windows.Forms.DataGridView();
            this.AddEpochButton = new System.Windows.Forms.Button();
            this.DeleteEpochButton = new System.Windows.Forms.Button();
            this.ObjectPictureBox = new System.Windows.Forms.PictureBox();
            this.AlphaTextBox = new System.Windows.Forms.TextBox();
            this.EpsilonTextBox = new System.Windows.Forms.TextBox();
            this.EpsilonLabel = new System.Windows.Forms.Label();
            this.AlphaLabel = new System.Windows.Forms.Label();
            this.ObjectDesciptionLabel = new System.Windows.Forms.Label();
            this.TopMenuToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // TopMenuToolStrip
            // 
            this.TopMenuToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.TopMenuToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProjectToolStripDropDownButton,
            this.ObjectToolStripDropDownButton,
            this.DecompositionToolStripDropDownButton});
            this.TopMenuToolStrip.Location = new System.Drawing.Point(0, 0);
            this.TopMenuToolStrip.Name = "TopMenuToolStrip";
            this.TopMenuToolStrip.Size = new System.Drawing.Size(1362, 31);
            this.TopMenuToolStrip.TabIndex = 1;
            this.TopMenuToolStrip.Text = "TopMenuToolStrip";
            // 
            // ProjectToolStripDropDownButton
            // 
            this.ProjectToolStripDropDownButton.AutoToolTip = false;
            this.ProjectToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ProjectToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenProjectToolStripMenuItem,
            this.SaveProjectToolStripMenuItem,
            this.SaveAsProjectToolStripMenuItem});
            this.ProjectToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("ProjectToolStripDropDownButton.Image")));
            this.ProjectToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ProjectToolStripDropDownButton.Name = "ProjectToolStripDropDownButton";
            this.ProjectToolStripDropDownButton.Size = new System.Drawing.Size(73, 28);
            this.ProjectToolStripDropDownButton.Text = "Проект";
            // 
            // OpenProjectToolStripMenuItem
            // 
            this.OpenProjectToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.OpenProjectToolStripMenuItem.Name = "OpenProjectToolStripMenuItem";
            this.OpenProjectToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            this.OpenProjectToolStripMenuItem.Text = "Открыть";
            this.OpenProjectToolStripMenuItem.Click += new System.EventHandler(this.OpenProjectToolStripMenuItem_Click);
            // 
            // SaveProjectToolStripMenuItem
            // 
            this.SaveProjectToolStripMenuItem.Name = "SaveProjectToolStripMenuItem";
            this.SaveProjectToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            this.SaveProjectToolStripMenuItem.Text = "Сохранить";
            this.SaveProjectToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
            // 
            // SaveAsProjectToolStripMenuItem
            // 
            this.SaveAsProjectToolStripMenuItem.Name = "SaveAsProjectToolStripMenuItem";
            this.SaveAsProjectToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            this.SaveAsProjectToolStripMenuItem.Text = "Сохранить как";
            // 
            // ObjectToolStripDropDownButton
            // 
            this.ObjectToolStripDropDownButton.AutoToolTip = false;
            this.ObjectToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ObjectToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ObjectShowDescriptionToolStripMenuItem});
            this.ObjectToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("ObjectToolStripDropDownButton.Image")));
            this.ObjectToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ObjectToolStripDropDownButton.Name = "ObjectToolStripDropDownButton";
            this.ObjectToolStripDropDownButton.Size = new System.Drawing.Size(73, 24);
            this.ObjectToolStripDropDownButton.Text = "Объект";
            // 
            // ObjectShowDescriptionToolStripMenuItem
            // 
            this.ObjectShowDescriptionToolStripMenuItem.Name = "ObjectShowDescriptionToolStripMenuItem";
            this.ObjectShowDescriptionToolStripMenuItem.Size = new System.Drawing.Size(248, 26);
            this.ObjectShowDescriptionToolStripMenuItem.Text = "Посмотреть описание";
            this.ObjectShowDescriptionToolStripMenuItem.Click += new System.EventHandler(this.ObjectDescriptionToolStripMenuItem_Click);
            this.ObjectShowDescriptionToolStripMenuItem.MouseLeave += new System.EventHandler(this.ObjectShowDescriptionToolStripMenuItem_MouseLeave);
            this.ObjectShowDescriptionToolStripMenuItem.MouseHover += new System.EventHandler(this.ObjectShowDescriptionToolStripMenuItem_MouseHover);
            // 
            // DecompositionToolStripDropDownButton
            // 
            this.DecompositionToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DecompositionToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FirstLevelToolStripMenuItem,
            this.SecondLevelToolStripMenuItem,
            this.FourthLevelToolStripMenuItem});
            this.DecompositionToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("DecompositionToolStripDropDownButton.Image")));
            this.DecompositionToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DecompositionToolStripDropDownButton.Name = "DecompositionToolStripDropDownButton";
            this.DecompositionToolStripDropDownButton.Size = new System.Drawing.Size(128, 24);
            this.DecompositionToolStripDropDownButton.Text = "Декомпозиция";
            // 
            // FirstLevelToolStripMenuItem
            // 
            this.FirstLevelToolStripMenuItem.Name = "FirstLevelToolStripMenuItem";
            this.FirstLevelToolStripMenuItem.Size = new System.Drawing.Size(162, 26);
            this.FirstLevelToolStripMenuItem.Text = "1 уровень";
            this.FirstLevelToolStripMenuItem.Click += new System.EventHandler(this.FirstLevelToolStripMenuItem_Click);
            // 
            // SecondLevelToolStripMenuItem
            // 
            this.SecondLevelToolStripMenuItem.Name = "SecondLevelToolStripMenuItem";
            this.SecondLevelToolStripMenuItem.Size = new System.Drawing.Size(162, 26);
            this.SecondLevelToolStripMenuItem.Text = "2 уровень";
            this.SecondLevelToolStripMenuItem.Click += new System.EventHandler(this.SecondLevelToolStripMenuItem_Click);
            // 
            // FourthLevelToolStripMenuItem
            // 
            this.FourthLevelToolStripMenuItem.Name = "FourthLevelToolStripMenuItem";
            this.FourthLevelToolStripMenuItem.Size = new System.Drawing.Size(162, 26);
            this.FourthLevelToolStripMenuItem.Text = "4 уровень";
            this.FourthLevelToolStripMenuItem.Click += new System.EventHandler(this.FourthLevelToolStripMenuItem_Click);
            // 
            // dataGridViewTable
            // 
            this.dataGridViewTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTable.Location = new System.Drawing.Point(0, 439);
            this.dataGridViewTable.Name = "dataGridViewTable";
            this.dataGridViewTable.RowHeadersWidth = 51;
            this.dataGridViewTable.RowTemplate.Height = 24;
            this.dataGridViewTable.Size = new System.Drawing.Size(1362, 334);
            this.dataGridViewTable.TabIndex = 0;
            // 
            // AddEpochButton
            // 
            this.AddEpochButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddEpochButton.Location = new System.Drawing.Point(11, 166);
            this.AddEpochButton.Name = "AddEpochButton";
            this.AddEpochButton.Size = new System.Drawing.Size(133, 52);
            this.AddEpochButton.TabIndex = 2;
            this.AddEpochButton.Text = "Добавить эпоху";
            this.AddEpochButton.UseVisualStyleBackColor = true;
            this.AddEpochButton.Click += new System.EventHandler(this.addEpochButton_Click);
            // 
            // DeleteEpochButton
            // 
            this.DeleteEpochButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DeleteEpochButton.Location = new System.Drawing.Point(176, 166);
            this.DeleteEpochButton.Name = "DeleteEpochButton";
            this.DeleteEpochButton.Size = new System.Drawing.Size(133, 52);
            this.DeleteEpochButton.TabIndex = 3;
            this.DeleteEpochButton.Text = "Удалить эпоху";
            this.DeleteEpochButton.UseVisualStyleBackColor = true;
            this.DeleteEpochButton.Click += new System.EventHandler(this.deleteEpochButton_Click);
            // 
            // ObjectPictureBox
            // 
            this.ObjectPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectPictureBox.Location = new System.Drawing.Point(564, 55);
            this.ObjectPictureBox.Name = "ObjectPictureBox";
            this.ObjectPictureBox.Size = new System.Drawing.Size(786, 363);
            this.ObjectPictureBox.TabIndex = 4;
            this.ObjectPictureBox.TabStop = false;
            // 
            // AlphaTextBox
            // 
            this.AlphaTextBox.Location = new System.Drawing.Point(11, 90);
            this.AlphaTextBox.Name = "AlphaTextBox";
            this.AlphaTextBox.Size = new System.Drawing.Size(133, 22);
            this.AlphaTextBox.TabIndex = 5;
            this.AlphaTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AlphaTextBox_KeyPress);
            // 
            // EpsilonTextBox
            // 
            this.EpsilonTextBox.Location = new System.Drawing.Point(176, 90);
            this.EpsilonTextBox.Name = "EpsilonTextBox";
            this.EpsilonTextBox.Size = new System.Drawing.Size(133, 22);
            this.EpsilonTextBox.TabIndex = 6;
            this.EpsilonTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EpsilonTextBox_KeyPress);
            // 
            // EpsilonLabel
            // 
            this.EpsilonLabel.AutoSize = true;
            this.EpsilonLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EpsilonLabel.Location = new System.Drawing.Point(206, 62);
            this.EpsilonLabel.Name = "EpsilonLabel";
            this.EpsilonLabel.Size = new System.Drawing.Size(69, 18);
            this.EpsilonLabel.TabIndex = 7;
            this.EpsilonLabel.Text = "Эпсилон";
            // 
            // AlphaLabel
            // 
            this.AlphaLabel.AutoSize = true;
            this.AlphaLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AlphaLabel.Location = new System.Drawing.Point(47, 62);
            this.AlphaLabel.Name = "AlphaLabel";
            this.AlphaLabel.Size = new System.Drawing.Size(55, 18);
            this.AlphaLabel.TabIndex = 8;
            this.AlphaLabel.Text = "Альфа";
            // 
            // ObjectDesciptionLabel
            // 
            this.ObjectDesciptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectDesciptionLabel.AutoSize = true;
            this.ObjectDesciptionLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ObjectDesciptionLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ObjectDesciptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ObjectDesciptionLabel.Location = new System.Drawing.Point(415, 55);
            this.ObjectDesciptionLabel.Name = "ObjectDesciptionLabel";
            this.ObjectDesciptionLabel.Padding = new System.Windows.Forms.Padding(1, 1, 1, 4);
            this.ObjectDesciptionLabel.Size = new System.Drawing.Size(128, 25);
            this.ObjectDesciptionLabel.TabIndex = 9;
            this.ObjectDesciptionLabel.Text = "object description";
            this.ObjectDesciptionLabel.Visible = false;
            // 
            // ApplicationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 773);
            this.Controls.Add(this.ObjectDesciptionLabel);
            this.Controls.Add(this.AlphaLabel);
            this.Controls.Add(this.EpsilonLabel);
            this.Controls.Add(this.EpsilonTextBox);
            this.Controls.Add(this.AlphaTextBox);
            this.Controls.Add(this.ObjectPictureBox);
            this.Controls.Add(this.DeleteEpochButton);
            this.Controls.Add(this.AddEpochButton);
            this.Controls.Add(this.dataGridViewTable);
            this.Controls.Add(this.TopMenuToolStrip);
            this.Name = "ApplicationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Данные";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ApplicationForm_FormClosing);
            this.Load += new System.EventHandler(this.ApplicationForm_Load);
            this.TopMenuToolStrip.ResumeLayout(false);
            this.TopMenuToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip TopMenuToolStrip;
        private System.Windows.Forms.DataGridView dataGridViewTable;
        private System.Windows.Forms.ToolStripDropDownButton ProjectToolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem OpenProjectToolStripMenuItem;
        private System.Windows.Forms.Button AddEpochButton;
        private System.Windows.Forms.Button DeleteEpochButton;
        private System.Windows.Forms.PictureBox ObjectPictureBox;
        private System.Windows.Forms.ToolStripDropDownButton ObjectToolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem SaveProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAsProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ObjectShowDescriptionToolStripMenuItem;
        private System.Windows.Forms.TextBox AlphaTextBox;
        private System.Windows.Forms.TextBox EpsilonTextBox;
        private System.Windows.Forms.Label EpsilonLabel;
        private System.Windows.Forms.Label AlphaLabel;
        private System.Windows.Forms.Label ObjectDesciptionLabel;
        private System.Windows.Forms.ToolStripDropDownButton DecompositionToolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem FirstLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SecondLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FourthLevelToolStripMenuItem;
    }
}

