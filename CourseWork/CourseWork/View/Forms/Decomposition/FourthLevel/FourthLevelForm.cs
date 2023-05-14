using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork.View.Forms.Decomposition.FourthLevel
{
    public partial class FourthLevelForm : Form
    {
        private List<string> marks;
        private Dictionary<string, List<double>> calculatedMarksValues;
        private DataGridView dataGridTable;

        public FourthLevelForm(DataGridView dataGridTable)
        {
            InitializeComponent();
            this.dataGridTable = dataGridTable;
        }

        private void FourthLevelForm_Load(object sender, EventArgs e)
        {

        }
    }
}
