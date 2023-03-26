using CourseWork.Domain.TechnogenicObject;
using CourseWork.Handlers.Application.ToolStrip.Project;
using CourseWork.Infra.Storage.Sqlite;
using CourseWork.Infra.Storage.Sqlite.TechnogenicObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class ApplicationForm : Form
    {
        // handlers
        private ProjectToolHandler projectToolHandler;

        // db object
        private Sqlite sqlite;
        
        // storage
        private ITechogenicObjectStorage technogenicObjectStorage;

        // domain service
        private ITechnogenicObjectService technogenicObjectService;

        public ApplicationForm(ProjectToolHandler projectToolHandler)
        {
            this.projectToolHandler = projectToolHandler;

            InitializeComponent();
        }

        private void ApplicationForm_Load(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }
        
        private void OpenProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                projectToolHandler.OpenProject();

                sqlite = new Sqlite(projectToolHandler.SqliteFilePath);

                technogenicObjectStorage = new TechnogenicObjectStorage(sqlite);
                technogenicObjectService = new TechnogenicObjectService(technogenicObjectStorage);

                technogenicObjectService.FillDataGridTable(dataGridViewTable);

                resizeDataGridTable();

                // technogenicObjectService.ShowPicture(projectToolHandler.PngFilePath)
                // technogenicObjectService.ShowObjectDescription(projectToolHandler.PngFilePath)
                // мб объединить в одну функцию InitializeProject(...)
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void deleteEpochButton_Click(object sender, EventArgs e)
        {
            List<int> selectedRowIndexes = new List<int>();

            foreach (DataGridViewRow r in dataGridViewTable.SelectedRows)
            {
                selectedRowIndexes.Add(r.Index);

                dataGridViewTable.Rows.Remove(r);
                dataGridViewTable.Rows.Add(r);
            }

            technogenicObjectService.DeleteEpoches(selectedRowIndexes);
        }

        private void resizeDataGridTable()
        {
            for (int column = 0; column < dataGridViewTable.ColumnCount; column++)
            {
                dataGridViewTable.Columns[column].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
    }
}
