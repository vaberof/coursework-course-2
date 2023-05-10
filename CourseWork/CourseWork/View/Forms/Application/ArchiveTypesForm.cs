using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CourseWork.View.Forms.Application
{
    public partial class ArchiveTypesForm : Form
    {
        public string ArchiveType { get; set; }
        public ArchiveTypesForm()
        {
            InitializeComponent();
        }

        private void ArchiveTypesForm_Load(object sender, EventArgs e)
        {

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (ZipRadioButton.Checked)
            {
                ArchiveType = ZipRadioButton.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            } else
            {
                ArchiveType = RarRadioButton.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

            
        }
    }
}
