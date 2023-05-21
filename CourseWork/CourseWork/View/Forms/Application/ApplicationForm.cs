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
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
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

        double alpha;
        double epsilon;
        Image image;

        private int nextEpochValue;
        private int currentEpochCount; // Текущее количество эпох = количество заполненных строк в mainCoordinatesTable

        public ApplicationForm(ProjectToolHandler projectToolHandler, ICalculationService calculationsService)
        {
            this.chartService = new ChartService();
            this.projectToolHandler = projectToolHandler;
            this.decompositionService = new DecompositionService(calculationsService);
            this.alpha = 0.9; 
            this.image = null;
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

                // TODO: открывать папку с проектом
                // TODO: фикс заполнения значений на 4 уровне
                technogenicObject = projectToolHandler.OpenProject();                

                enableFormUserInterface();
                initServiceAndStorage();                
                
                technogenicObjectService.FillMainCoordinatesTable(MainCoordinatiesDataGridViewTable);

                if (isEmptyMainCoordinatesTable())
                {
                    MessageBox.Show("Невозможно продолжить работу программы: пустая таблица координат", "Ошибка");
                    return;
                }

                if (isNotEnoughTechnogenicObjectData())
                {
                    MessageBox.Show("Невозможно продолжить работу программы: были получены не все исходные данные из текстового файла", "Ошибка");
                    return;
                }

                double epsilon = technogenicObject.MeasurementAccuracy;

                nextEpochValue = Convert.ToInt32(
                MainCoordinatiesDataGridViewTable.Rows[MainCoordinatiesDataGridViewTable.RowCount - 2].Cells[0].Value.ToString()) + 1;

                technogenicObjectService.CreateTechnogenicObjectValuesTable(alpha, epsilon, projectToolHandler.PngFilePath, nextEpochValue);
                technogenicObjectService.GetTechnogenicObjectValues(ref alpha, ref epsilon, ref image, ref nextEpochValue);

                resizeDataGridTable();

                currentEpochCount = MainCoordinatiesDataGridViewTable.Rows.Count - 1;

                // set up elements on form
                showObjectPicture(projectToolHandler.PngFilePath);
                AlphaTextBox.Text = alpha.ToString();
                EpsilonTextBox.Text = epsilon.ToString();
                setObjectDescription();               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }              
        }

        private bool isEmptyMainCoordinatesTable()
        {
            return MainCoordinatiesDataGridViewTable.RowCount == 0;
        }
        
        private bool isNotEnoughTechnogenicObjectData()
        {
            return technogenicObject == null || technogenicObject.Type == "" ||
                technogenicObject.StructuralBlocksCount == 0 || technogenicObject.GeodeticMarksCount == 0 ||
                technogenicObject.MeasurementEpochsCount == 0;
        }

        private void initServiceAndStorage()
        {
            db = new Sqlite(projectToolHandler.SqliteFilePath);
            technogenicObjectStorage = new TechnogenicObjectStorage(db);
            technogenicObjectService = new TechnogenicObjectService(technogenicObjectStorage);
        }

        private void addEpochButton_Click(object sender, EventArgs e)
        {
            technogenicObjectService.AddEpoch(MainCoordinatiesDataGridViewTable, ref currentEpochCount);
            technogenicObjectService.FillMainCoordinatesTable(MainCoordinatiesDataGridViewTable);
            resizeDataGridTable();
        }

        private double cleanNumber(double num)
        {
            string strNum = num.ToString("R");
            //MessageBox.Show("cleaned num: " + strNum);
            return Convert.ToDouble(strNum);            
        }

        private void deleteEpochButton_Click(object sender, EventArgs e)
        {
            List<int> selectedRowIndexes = new List<int>();

            foreach (DataGridViewRow row in MainCoordinatiesDataGridViewTable.SelectedRows)
            {
                selectedRowIndexes.Add(row.Index);
            }

            technogenicObjectService.DeleteEpochs(MainCoordinatiesDataGridViewTable, selectedRowIndexes, ref currentEpochCount);                
            
            for (int i = 0; i < selectedRowIndexes.Count; i++)
            {
                MainCoordinatiesDataGridViewTable.Rows.RemoveAt(selectedRowIndexes[i]);
            }

            technogenicObjectService.FillMainCoordinatesTable(MainCoordinatiesDataGridViewTable);
            resizeDataGridTable();
        }

        private void resizeDataGridTable()
        {
            for (int column = 0; column < MainCoordinatiesDataGridViewTable.ColumnCount; column++)
            {
                MainCoordinatiesDataGridViewTable.Columns[column].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
            // check for alpha and epsilon values on main form
            // then save all

            if (isEmptyAlphaOrEpsilonTextBoxes())
            {
                printEmptyAlphaOrEpsilonTextBoxesMessage();
                return;
            }

            double cleanedAlpha = cleanNumber(Convert.ToDouble(AlphaTextBox.Text));
            double cleanedEpsilon = cleanNumber(Convert.ToDouble(EpsilonTextBox.Text));

            technogenicObjectService.UpdateAlphaAndEpsilon(cleanedAlpha, cleanedEpsilon);

            /*technogenicObjectStorage.CloseConnection();
            ObjectPictureBox.Image.Dispose();
            ObjectPictureBox.Image = null;

            projectToolHandler.SaveZipArchive();

            technogenicObjectStorage.OpenConnection();
            showObjectPicture(projectToolHandler.PngFilePath);*/

            // TODO: вынести в функцию
            technogenicObjectService.FillMainCoordinatesTable(MainCoordinatiesDataGridViewTable);
            resizeDataGridTable();

            //setObjectDescription();
        }        

        private void FirstLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isEmptyAlphaOrEpsilonTextBoxes())
            {
                printEmptyAlphaOrEpsilonTextBoxesMessage();
                return;
            }

            double cleanedAlpha = cleanNumber(Convert.ToDouble(AlphaTextBox.Text));
            double cleanedEpsilon = cleanNumber(Convert.ToDouble(EpsilonTextBox.Text));

            //tryParseAlphaAndEpsilonValues(ref alpha, ref epsilon);           

            FirstLevelForm firstLevelForm = new FirstLevelForm(
                cleanedEpsilon,
                cleanedAlpha,
                decompositionService,
                chartService,
                MainCoordinatiesDataGridViewTable,
                technogenicObjectService.GetDataTable());

            firstLevelForm.Show();
        }        

        private void SecondLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isEmptyAlphaOrEpsilonTextBoxes())
            {
                printEmptyAlphaOrEpsilonTextBoxesMessage();
                return;
            }

            double cleanedAlpha = cleanNumber(Convert.ToDouble(AlphaTextBox.Text));
            double cleanedEpsilon = cleanNumber(Convert.ToDouble(EpsilonTextBox.Text));

            //tryParseAlphaAndEpsilonValues(ref alpha, ref epsilon);

            SecondLevelForm secondLevelForm = new SecondLevelForm(
                decompositionService,
                chartService,
                technogenicObject.StructuralBlocksCount,
                technogenicObject.GeodeticMarksCount,
                cleanedEpsilon,
                cleanedAlpha,
                projectToolHandler.PngFilePath,
                MainCoordinatiesDataGridViewTable);

            secondLevelForm.Show();
        }

        private void FourthLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isEmptyAlphaOrEpsilonTextBoxes())
            {
                printEmptyAlphaOrEpsilonTextBoxesMessage();
                return;
            }

            double cleanedAlpha = cleanNumber(Convert.ToDouble(AlphaTextBox.Text));

            //tryParseAlphaAndEpsilonValues(ref alpha, ref epsilon);

            FourthLevelForm fourthLevelForm = new FourthLevelForm(
                decompositionService,
                chartService,
                technogenicObject.GeodeticMarksCount,
                cleanedAlpha,
                MainCoordinatiesDataGridViewTable);

            fourthLevelForm.Show();
        }
        private bool isEmptyAlphaOrEpsilonTextBoxes()
        {
            return AlphaTextBox.Text == "" || EpsilonTextBox.Text == "";
        }

        private void printEmptyAlphaOrEpsilonTextBoxesMessage()
        {
            MessageBox.Show("Пустые значения Альфа и/или Точность измерения", "Ошибка");
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
