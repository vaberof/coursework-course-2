using CourseWork.Domain.TechnogenicObject;
using CourseWork.Handlers.Application.ToolStrip.Project;
using CourseWork.Infra.Storage.Sqlite;
using CourseWork.Infra.Storage.Sqlite.TechnogenicObject;
using CourseWork.Service.Calculation;
using CourseWork.Service.Chart;
using CourseWork.Service.Decomposition;
using CourseWork.View.Forms.Application;
using CourseWork.View.Forms.Decomposition;
using CourseWork.View.Forms.Decomposition.FourthLevel;
using CourseWork.View.Forms.Decomposition.SecondLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class ApplicationForm : Form
    {
        // handlers
        private ProjectToolHandler projectToolHandler;

        // db object
        private Sqlite db;
        
        // storage
        private ITechogenicObjectStorage technogenicObjectStorage;

        // domain object
        private TechnogenicObject technogenicObject;

        // domain service
        private ITechnogenicObjectService technogenicObjectService;

        // application service
        DecompositionService decompositionService;
        ChartService chartService;

        private int nextEpochValue;
        private int currentEpochCount; // Текущее количество эпох = количество заполненных строк в mainCoordinatesTable
        private bool isOpenedProject;
        public ApplicationForm(ProjectToolHandler projectToolHandler, ICalculationService calculationsService)
        {
            this.chartService = new ChartService();
            this.projectToolHandler = projectToolHandler;
            this.decompositionService = new DecompositionService(calculationsService);
            InitializeComponent();
        }
        
        private void ApplicationForm_Load(object sender, EventArgs e)
        {
            disableFormUserInterface();
        }
                
        private void OpenProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                /*if (technogenicObjectStorage != null)
                {
                    technogenicObjectStorage.CloseConnection();                    
                }

                if (ObjectPictureBox.Image != null)
                {
                    ObjectPictureBox.Image.Dispose();
                    ObjectPictureBox.Image = null;
                }*/

                technogenicObject = projectToolHandler.OpenProject();                
                isOpenedProject = true;

                enableFormUserInterface();

                initServiceAndStorage();                

                technogenicObjectStorage.CreateEpochCountColumn();
                technogenicObjectService.FillMainCoordinatesTable(dataGridViewTable);

                nextEpochValue = Convert.ToInt32(
                    dataGridViewTable.Rows[dataGridViewTable.RowCount - 2].Cells[0].Value.ToString()) + 1;

                technogenicObjectStorage.UpdateEpochCount(nextEpochValue);

                currentEpochCount = dataGridViewTable.Rows.Count - 1;

                showObjectPicture(projectToolHandler.PngFilePath);
                resizeDataGridTable();

                AlphaTextBox.Text = "0,9";
                EpsilonTextBox.Text = Convert.ToString(technogenicObject.MeasurementAccuracy);

                setObjectDescription();               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }              
        }

        private void initServiceAndStorage()
        {
            db = new Sqlite(projectToolHandler.SqliteFilePath);
            technogenicObjectStorage = new TechnogenicObjectStorage(db);
            technogenicObjectService = new TechnogenicObjectService(technogenicObjectStorage);
        }

        private void addEpochButton_Click(object sender, EventArgs e)
        {
            technogenicObjectService.AddEpoch(dataGridViewTable, ref currentEpochCount);
            technogenicObjectService.FillMainCoordinatesTable(dataGridViewTable);
            resizeDataGridTable();
        }

        private void deleteEpochButton_Click(object sender, EventArgs e)
        {

            List<int> selectedRowIndexes = new List<int>();

            foreach (DataGridViewRow row in dataGridViewTable.SelectedRows)
            {
                selectedRowIndexes.Add(row.Index);
            }

            technogenicObjectService.DeleteEpochs(dataGridViewTable, selectedRowIndexes, ref currentEpochCount);                
            
            for (int i = 0; i < selectedRowIndexes.Count; i++)
            {
                dataGridViewTable.Rows.RemoveAt(selectedRowIndexes[i]);
            }

            technogenicObjectService.FillMainCoordinatesTable(dataGridViewTable);
            resizeDataGridTable();
        }

        private void resizeDataGridTable()
        {
            for (int column = 0; column < dataGridViewTable.ColumnCount; column++)
            {
                dataGridViewTable.Columns[column].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void showObjectPicture(string pngFilePath)
        {
            ObjectPictureBox.Load(pngFilePath);
            ObjectPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void disableFormUserInterface()
        {
            ObjectToolStripDropDownButton.Enabled = false;
            DecompositionToolStripDropDownButton.Enabled = false;
            AlphaTextBox.Enabled = false;
            EpsilonTextBox.Enabled = false;
            AddEpochButton.Enabled = false;
            DeleteEpochButton.Enabled = false;
        }

        private void enableFormUserInterface()
        {
            ObjectToolStripDropDownButton.Enabled = true;
            DecompositionToolStripDropDownButton.Enabled = true;
            AlphaTextBox.Enabled = true;
            EpsilonTextBox.Enabled = true;
            AddEpochButton.Enabled = true;
            DeleteEpochButton.Enabled = true;
        }

        private void ObjectShowDescriptionToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            ObjectDesciptionLabel.Visible = true;
        }

        private void ObjectShowDescriptionToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            ObjectDesciptionLabel.Visible = false;
        }

        private void ObjectDescriptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ObjectDesciptionLabel.Text, "Описание объекта");
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // открывается форма с выбором типа архива
            using (var archiveTypesForm = new ArchiveTypesForm())
            {
                var result = archiveTypesForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string archiveType = archiveTypesForm.ArchiveType;

                    technogenicObjectStorage.CloseConnection();
                    ObjectPictureBox.Image.Dispose();
                    ObjectPictureBox.Image = null;

                    projectToolHandler.SaveArchive(archiveType);

                    technogenicObjectStorage.OpenConnection();

                    // TODO: вынести в функцию
                    technogenicObjectService.FillMainCoordinatesTable(dataGridViewTable);
                    showObjectPicture(projectToolHandler.PngFilePath);

                    resizeDataGridTable();

                    AlphaTextBox.Text = "0,9";
                    EpsilonTextBox.Text = Convert.ToString(technogenicObject.MeasurementAccuracy);

                    setObjectDescription();
                }
            }
        }

        private void FirstLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FirstLevelForm firstLevelForm = new FirstLevelForm(
                technogenicObject.MeasurementAccuracy,
                Convert.ToDouble(AlphaTextBox.Text),
                decompositionService,
                chartService,
                dataGridViewTable,
                technogenicObjectService.GetDataTable());

            firstLevelForm.Show();
        }

        private void SecondLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SecondLevelForm secondLevelForm = new SecondLevelForm(
                decompositionService,
                chartService,
                technogenicObject.StructuralBlocksCount,
                technogenicObject.GeodeticMarksCount,
                technogenicObject.MeasurementAccuracy,
                Convert.ToDouble(AlphaTextBox.Text),
                projectToolHandler.PngFilePath,
                dataGridViewTable);

            secondLevelForm.Show();
        }

        private void FourthLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FourthLevelForm fourthLevelForm = new FourthLevelForm(
                decompositionService,
                chartService,
                technogenicObject.GeodeticMarksCount,
                Convert.ToDouble(AlphaTextBox.Text),
                dataGridViewTable);

            fourthLevelForm.Show();
        }

        private void AlphaTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            if (e.KeyChar == ',' && (sender as TextBox).Text.Contains(','))
            {
                e.Handled = true;
            }
        }

        private void EpsilonTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            if (e.KeyChar == ',' && (sender as TextBox).Text.Contains(','))
            {
                e.Handled = true;
            }
        }

        // private void setupInitialObjectValues() {...}

        private void setObjectDescription()
        {
            string objectDescpiption = string.Format(
                        "Вариант задания: {0}\n" +
                        "Тип техногенного объекта: {1}\n" +
                        "Количество структурных блоков: {2}\n" +
                        "Количество геодезических марок, закрепленных в теле объекта: {3}\n" +
                        "Количество эпох измерений: {4}\n" +
                        "Точность измерений: {5}м",
                        technogenicObject.TaskOption,
                        technogenicObject.Type,
                        technogenicObject.StructuralBlocksCount,
                        technogenicObject.GeodeticMarksCount,
                        technogenicObject.MeasurementEpochsCount,
                        technogenicObject.MeasurementAccuracy);

            ObjectDesciptionLabel.Text = objectDescpiption;
        }
        

        private void ApplicationForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }        
    }
}
